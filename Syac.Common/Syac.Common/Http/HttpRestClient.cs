using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Common.Http;

public class HttpRestClient(HttpClient httpClient, IOptions<SyacHttpSettings> settings) : IHttpRestClient
{
    private readonly SyacHttpSettings _settings = settings.Value;

    /// <inheritdoc/>
    public async Task<TResponse> GetAsync<TResponse>(
        string? uri = null,
        string? configName = null,
        HttpResilienceOptions? manualOverride = null,
        CancellationToken cancellationToken = default)
    {
        var (finalUri, resilienceOptions) = ResolveConfig(uri, configName, manualOverride);
        var pipeline = BuildPipeline(resilienceOptions);
        return await pipeline.ExecuteAsync(async ct =>
        {
            var response = await httpClient.GetAsync(finalUri, ct);
            response.EnsureSuccessStatusCode();
            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default!;
            }

            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: ct)!;
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string? uri = null,
        TRequest request = default!,
        string? configName = null,
        HttpResilienceOptions? manualOverride = null,
        CancellationToken cancellationToken = default)
    {
       var (finalUri, resilienceOptions) = ResolveConfig(uri, configName, manualOverride);
        var pipeline = BuildPipeline(resilienceOptions);

        return await pipeline.ExecuteAsync(async ct =>
        {
            var response = await httpClient.PostAsJsonAsync(finalUri, request, cancellationToken: ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: ct)!;
        }, cancellationToken);
    }

    private (Uri finalUri, HttpResilienceOptions resilienceOptions) ResolveConfig(string? uriParam, string? configName, HttpResilienceOptions? manualOverride)
    {
        HttpServiceConfig? config = null;

        if (!string.IsNullOrEmpty(configName) && _settings.Services.TryGetValue(configName!, out var foundConfig))
        {
            config = foundConfig;
        }

        var options = manualOverride ?? config?.Resilience ?? HttpResilienceOptions.Default;

        string baseUrl = config?.BaseUrl ?? string.Empty;
        string path = uriParam ?? string.Empty;

        if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
        {
            return (new Uri(path), options);
        }

        if (!string.IsNullOrEmpty(baseUrl))
        {
            var baseUri = new Uri(baseUrl);
            var finalUri = string.IsNullOrEmpty(path) ? baseUri : new Uri(baseUri, path);
            return (finalUri, options);
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Either a valid URI or a configuration with a BaseUrl must be provided.");
        }

        return (new Uri(path, UriKind.RelativeOrAbsolute), options);
    }

    private ResiliencePipeline BuildPipeline(HttpResilienceOptions options) => new ResiliencePipelineBuilder()
        .AddTimeout(new TimeoutStrategyOptions { Timeout = options.TotalTimeout })
        .AddRetry(new RetryStrategyOptions
        {
            MaxRetryAttempts = options.MaxRetryAttempts,
            Delay = options.DelayBetweenRetries,
            BackoffType = DelayBackoffType.Exponential
        })
        .Build();

}
