using Microsoft.Extensions.Logging;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Database;

namespace ProxyCollectorNET.Strategies;

public class ProxyScanProxyCollectorStrategy : ProxyCollectorStrategyBase
{
    public ProxyScanProxyCollectorStrategy(AppDbContext dbContext, IHttpClientFactory httpClientFactory,
        ILogger<ProxyScanProxyCollectorStrategy> logger) : base(dbContext, httpClientFactory, logger)
    {
    }
    
    public override void Configure(ProxyCollectorOptions options)
    {
        options.BaseUrl = "https://www.proxyscan.io";
        options.BaseUrl = "Proxy Scan";
    }

    public override async Task<List<ProxyAddress>> FetchAsync(CancellationToken cancellationToken = default)
    {
        //https://www.proxyscan.io/Home/FilterResult
        throw new NotImplementedException();
    }
}