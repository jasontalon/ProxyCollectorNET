using AutoMapper;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Dto;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Extensions;

namespace ProxyCollectorNET.Collectors;

public interface IProxyScanCollector : ICollector
{
}

public class ProxyScanCollector : IProxyScanCollector
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public ProxyScanCollector(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<(List<Address>? Addresses, Exception? Exception)> FetchAsync(
        CancellationToken cancellationToken = default)
    {
        var path = "api/proxy?format=json&limit=100&last_check=3600";

        var (response, exception) = await _httpClient.GetAsync<List<ProxyscanResponse>>(path, cancellationToken);

        if (exception is not null) return (null, exception);

        (var addresses, exception) = TryCatch.Handle(() => _mapper.Map<List<Address>>(response));

        if (exception is not null) return (null, exception);

        return (addresses, null);
    }

    public static void ConfigureHttpClient(HttpClient client)
    {
        client.BaseAddress = new Uri("https://www.proxyscan.io");
        client.Timeout = TimeSpan.FromSeconds(10);
    }
}