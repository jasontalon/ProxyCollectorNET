using Microsoft.Extensions.Logging;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Database;

namespace ProxyCollectorNET.Strategies;

public class GeonodeProxyCollectorStrategy : ProxyCollectorStrategyBase
{

    public override void Configure(ProxyCollectorOptions options)
    {
        options.BaseUrl = "https://proxylist.geonode.com";
        options.ServiceName = "Geonode";
    }

    public override async Task<List<ProxyAddress>> FetchAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public GeonodeProxyCollectorStrategy(AppDbContext dbContext, IHttpClientFactory httpClientFactory, ILogger<GeonodeProxyCollectorStrategy> logger) : base(dbContext, httpClientFactory, logger)
    {
        //https://proxylist.geonode.com/api/proxy-list?limit=100&page=1&sort_by=lastChecked&sort_type=desc&protocols=https
    }
}