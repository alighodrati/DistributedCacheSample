using Microsoft.Extensions.Caching.StackExchangeRedis;

public static class StackExchangeRedisCacheServiceCollectionExtensions
{
    //
    // Summary:
    //     Adds Redis distributed caching services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
    //
    // Parameters:
    //   services:
    //     The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services
    //     to.
    //
    //   setupAction:
    //     An System.Action`1 to configure the provided Microsoft.Extensions.Caching.StackExchangeRedis.RedisCacheOptions.
    //
    // Returns:
    //     The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional
    //     calls can be chained.
    public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheOptions> setupAction)
    {
        if (services == null)
        {
            throw new ArgumentNullException("services");
        }

        if (setupAction == null)
        {
            throw new ArgumentNullException("setupAction");
        }

        services.AddOptions();
        services.Configure(setupAction);
        services.AddSingleton<RedisCache>();
        return services;
    }
}
