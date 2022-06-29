using Microsoft.Extensions.DependencyInjection;
using ProxyCollectorNET.Infrastructure;

namespace ProxyCollectorNET.Strategies;

public static class DependencyInjection
{
    public static IServiceCollection AddProxyCollectionStrategies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProxyCollectorStrategy, GeonodeProxyCollectorStrategy>();
        return serviceCollection;
    }
}