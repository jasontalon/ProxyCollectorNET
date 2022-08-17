using AutoMapper;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Dto;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Extensions;

namespace ProxyCollectorNET.Collectors;

public interface IGeonodeCollector : ICollector
{
    
}

public class GeonodeCollector : IGeonodeCollector
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public GeonodeCollector(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<(List<Address>? Addresses, Exception? Exception)> Fetch(CancellationToken cancellationToken)
    {
        var path = "api/proxy-list?limit=100&page=1";

        var (response, error) =
            await _httpClient.GetAsync<GeonodeResponse>(path, cancellationToken);

        if (error is not null) return (null, error);

        if (response is null) return (null, null);

        (var addresses, error) = TryCatch.Handle(() => _mapper.Map<GeonodeResponse, List<Address>>(response));

        if (error is not null) return (null, error);

        return (addresses, null);
    }

    public static void ConfigureClient(HttpClient client)
    {
        client.BaseAddress = new Uri("https://proxylist.geonode.com");
        client.Timeout = TimeSpan.FromSeconds(10);
    }
}