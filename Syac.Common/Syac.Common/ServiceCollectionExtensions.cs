using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syac.Common.Cache;
using Syac.Common.Http;

namespace Syac.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSyacCommonHttp(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SyacHttpSettings>(config.GetSection("SyacHttpSettings"));
        services.AddHttpClient<IHttpRestClient, HttpRestClient>();
        return services;
    }

    public static IServiceCollection AddSyacCache(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("SyacCacheSettings");
        services.Configure<SyacCacheSettings>(section);
        var settings = section.Get<SyacCacheSettings>();

        if (settings is null || string.IsNullOrEmpty(settings.ConnectionString))
        {
            throw new InvalidOperationException("SyacCacheSettings.ConnectionString is not configured properly.");
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = settings.ConnectionString;
            options.InstanceName = settings.InstanceName;
        });

        services.AddSingleton<ISyacCache, SyacCache>();

        return services;
    }
}
