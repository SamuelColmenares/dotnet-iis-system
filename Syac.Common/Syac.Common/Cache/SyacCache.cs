using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Syac.Common.Cache;

public class SyacCache(IDistributedCache cache, IOptions<SyacCacheSettings> settingsOpt) : ISyacCache
{
    private readonly SyacCacheSettings settings = settingsOpt.Value;

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var data = await cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(data))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);

        if (cachedValue is not null && !EqualityComparer<T>.Default.Equals(cachedValue, default(T)))
        {
            return cachedValue;
        }

        var newValue = await factory();

        if (newValue is not null && !EqualityComparer<T>.Default.Equals(newValue, default(T)))
        {
            await SetAsync(key, newValue, expiration, cancellationToken);
        }

        return newValue;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value);

        if (settings.DefaultExpirationInMinutes.HasValue)
        {
            expiration ??= TimeSpan.FromMinutes(settings.DefaultExpirationInMinutes.Value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            await cache.SetStringAsync(key, json, options, cancellationToken);
        }
        else
        {
            await cache.SetStringAsync(key, json, cancellationToken);
        }


    }
}
