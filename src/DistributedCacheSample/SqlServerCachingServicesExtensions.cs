using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;

public static class SqlServerCachingServicesExtensions
{
    //
    // Summary:
    //     Adds Microsoft SQL Server distributed caching services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
    //
    // Parameters:
    //   services:
    //     The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services
    //     to.
    //
    //   setupAction:
    //     An System.Action`1 to configure the provided Microsoft.Extensions.Caching.SqlServer.SqlServerCacheOptions.
    //
    // Returns:
    //     The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional
    //     calls can be chained.
    public static IServiceCollection AddSqlServerCache(this IServiceCollection services, Action<SqlServerCacheOptions> setupAction)
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
        AddSqlServerCacheServices(services);
        services.Configure(setupAction);
        return services;
    }

    internal static void AddSqlServerCacheServices(IServiceCollection services)
    {
        services.AddSingleton<SqlServerCache>();
    }
}
