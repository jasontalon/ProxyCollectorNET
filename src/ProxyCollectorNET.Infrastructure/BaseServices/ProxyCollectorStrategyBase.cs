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
    protected readonly ProxyCollectorOptions _options;

    public ProxyCollectorStrategyBase(AppDbContext dbContext, IHttpClientFactory httpClientFactory,
        ILogger<IProxyCollectorStrategy> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _options = new ProxyCollectorOptions();
    }

    public abstract void Configure(ProxyCollectorOptions options);

    public async Task WorkAsync(CancellationToken cancellationToken)
    {
        Configure(_options);

        if (string.IsNullOrEmpty(_options.BaseUrl))
            throw new ArgumentNullException(nameof(_options.BaseUrl));

        if (string.IsNullOrEmpty(_options.ServiceName))
            throw new ArgumentNullException(nameof(_options.ServiceName));
        
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        
        await FetchAsync(cancellationToken);
    }

    public abstract Task<List<ProxyAddress>> FetchAsync(CancellationToken cancellationToken = default);
}