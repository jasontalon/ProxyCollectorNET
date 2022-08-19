using Microsoft.Extensions.DependencyInjection;
using HttpMessageHandler = ProxyCollectorNET.Infrastructure.HttpMessageHandler;

namespace ProxyCollectorNET.Collectors;

public static class DependencyInjection
{
    public static IServiceCollection AddCollectors(this IServiceCollection @this)
    {
        @this.AddHttpClient<IGeonodeCollector, GeonodeCollector>(GeonodeCollector.ConfigureHttpClient)
            .AddHttpMessageHandler<HttpMessageHandler>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        @this.AddHttpClient<IProxyScanCollector, ProxyScanCollector>(ProxyScanCollector.ConfigureHttpClient)
            .AddHttpMessageHandler<HttpMessageHandler>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        @this.AddHttpClient<IFreeProxyListCollector, FreeProxyListCollector>(FreeProxyListCollector.ConfigureHttpClient)
            .AddHttpMessageHandler<HttpMessageHandler>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));


        return @this;
    }
}