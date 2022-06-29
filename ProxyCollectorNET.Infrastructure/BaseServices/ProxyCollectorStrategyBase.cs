using Microsoft.Extensions.Logging;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Infrastructure.Database;

namespace ProxyCollectorNET.Infrastructure;

public class ProxyCollectorOptions
{
    public string ServiceName { get; set; }
    public string BaseUrl { get; set; }
}

public abstract class ProxyCollectorStrategyBase : IProxyCollectorStrategy
{
    protected readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;
    protected readonly ILogger _logger;

    public ProxyCollectorStrategyBase(AppDbContext dbContext, IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public abstract void Configure(Action<ProxyCollectorOptions> options);

    public async Task Work(CancellationToken cancellationToken)
    {
        var proxyAddresses = await Fetch(cancellationToken);

        foreach (var proxyAddress in proxyAddresses)
        {
        }
    }

    public abstract Task<List<ProxyAddress>> Fetch(CancellationToken cancellationToken = default);
}