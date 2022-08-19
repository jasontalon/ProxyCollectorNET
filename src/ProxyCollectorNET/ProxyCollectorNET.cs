using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProxyCollectorNET.Collectors;
using HttpMessageHandler = ProxyCollectorNET.Infrastructure.HttpMessageHandler;

namespace ProxyCollectorNET;

public static class ProxyCollectorNET
{
    private static IServiceCollection AddProxyCollector(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddAutoMapper(typeof(ProxyCollectorNET))
            .AddScoped<HttpMessageHandler>()
            .AddCollectors();

        return serviceCollection;
    }

    private static void AddProxyCollector(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        => serviceCollection.AddProxyCollector(hostBuilderContext.Configuration);

    public static IHost BuildHost()
    {
        var Configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(Configuration))
            .ConfigureServices(AddProxyCollector);

        return host.Build();
    }
}