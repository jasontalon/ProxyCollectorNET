using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProxyCollectorNET.Infrastructure.Database;
using ProxyCollectorNET.Strategies;

namespace ProxyCollectorNET;

public static class ProxyCollectorNET
{
    public static IServiceCollection AddProxyCollector(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddInfrastructure(configuration)
            .AddProxyCollectionStrategies();
        return serviceCollection;
    }

    public static IHost BuildHost()
    {
        var Configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(Configuration))
            .ConfigureServices((_, services) => services.AddProxyCollector(Configuration));

        return host.Build();
    }
}