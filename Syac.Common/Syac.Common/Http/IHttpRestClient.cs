using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Common.Http;

/// <summary>
/// Defines an interface for an HTTP REST client that provides methods for sending asynchronous HTTP requests and
/// handling responses with built-in resilience and configuration options.
/// </summary>
/// <remarks>This interface supports common HTTP operations such as GET and POST, with the ability to deserialize
/// responses into specified types. It includes support for configurable resilience options to handle transient faults
/// and retries, as well as the ability to override default configurations on a per-request basis.</remarks>
public interface IHttpRestClient
{
    /// <summary>
    /// Sends an asynchronous GET request to the specified URI and returns the response deserialized into the specified
    /// type.
    /// </summary>
    /// <typeparam name="TResponse">The type to which the response will be deserialized.</typeparam>
    /// <param name="uri">The URI of the resource to retrieve. If <see langword="null"/>, a default URI from the configuration may be
    /// used.</param>
    /// <param name="configName">The name of the configuration to use for the request. If <see langword="null"/>, the default configuration is
    /// applied.</param>
    /// <param name="manualOverride">Optional resilience options to override the default configuration for this request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response of type
    /// <typeparamref name="TResponse"/>.</returns>
    Task<TResponse> GetAsync<TResponse>(
        string? uri=null, 
        string? configName=null, 
        HttpResilienceOptions? manualOverride = null, 
        CancellationToken cancellationToken= default);
    
    /// <summary>
    /// Sends an HTTP POST request to the specified URI with the provided request content and returns the deserialized
    /// response.
    /// </summary>
    /// <remarks>The method uses HTTP resilience options to handle transient faults and retries, which can be
    /// customized via the <paramref name="manualOverride"/> parameter. Ensure that the <typeparamref name="TRequest"/>
    /// and <typeparamref name="TResponse"/> types are serializable and deserializable, respectively.</remarks>
    /// <typeparam name="TRequest">The type of the request content to be sent in the body of the POST request.</typeparam>
    /// <typeparam name="TResponse">The type of the response content expected from the server.</typeparam>
    /// <param name="uri">The URI to which the POST request is sent. If <see langword="null"/>, a default URI may be used based on the
    /// configuration.</param>
    /// <param name="request">The content to be sent in the body of the POST request. Cannot be <see langword="null"/> if the request requires
    /// a body.</param>
    /// <param name="configName">The name of the configuration to use for the request. If <see langword="null"/>, the default configuration is
    /// applied.</param>
    /// <param name="manualOverride">Optional settings to override the default HTTP resilience options for this request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response of type
    /// <typeparamref name="TResponse"/>.</returns>
    Task<TResponse> PostAsync<TRequest, TResponse>(
        string? uri = null, 
        TRequest request = default!, 
        string? configName = null, 
        HttpResilienceOptions? manualOverride = null, 
        CancellationToken cancellationToken = default);
}
