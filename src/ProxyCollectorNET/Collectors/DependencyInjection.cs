using Microsoft.Extensions.DependencyInjection;
using HttpMessageHandler = ProxyCollectorNET.Infrastructure.HttpMessageHandler;

namespace ProxyCollectorNET.Collectors;

public static class DependencyInjection
{
    public static IServiceCollection AddCollectors(this IServiceCollection @this)
    {
        @this.AddHttpClient<IGeonodeCollector, GeonodeCollector>(GeonodeCollector.ConfigureClient)
            .AddHttpMessageHandler<HttpMessageHandler>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        return @this;
    }
}