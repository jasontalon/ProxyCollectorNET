using System.Text;
using AutoMapper;
using HtmlAgilityPack;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Dto;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Extensions;

namespace ProxyCollectorNET.Collectors;

public interface IFreeProxyListCollector : ICollector
{
}

public class FreeProxyListCollector : IFreeProxyListCollector
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public FreeProxyListCollector(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<(List<Address>? Addresses, Exception? Exception)> FetchAsync(
        CancellationToken cancellationToken = default)
    {
        var (html, error) =
            await TryCatch.HandleAsync(async () => await _httpClient.GetStringAsync(string.Empty, cancellationToken));

        if (error is not null) return (null, error);

        var document = new HtmlDocument();

        (var body, error) = TryCatch.Handle(() =>
        {
            document.LoadHtml(html);
            return document.DocumentNode.SelectNodes(
                "//table[contains(@class, 'table table-striped table-bordered')]/tbody");
        });

        if (error is not null) return (default, error);

        if (body is null) return default;

        List<FreeProxyListResponse> list = new();

        var properties = new[]
        {
            "IpAddress",
            "Port",
            "CountryCode",
            "Country",
            "Anonymity",
            "Google",
            "Https",
            "LastChecked"
        };

        foreach (var row in body.Nodes())
        {
            FreeProxyListResponse proxy = new();

            var dict = row.ChildNodes.Select((item, index) =>
                    new KeyValuePair<string, string>(properties[index], item.InnerText))
                .ToDictionary(x => x.Key, x => x.Value);

            proxy.Set(dict);

            list.Add(proxy);
        }

        (var addresses, error) = TryCatch.Handle(() => _mapper.Map<List<Address>>(list));

        if (error is not null) return (default, error);

        return (addresses, null);
    }

    public static void ConfigureHttpClient(HttpClient client)
    {
        client.BaseAddress = new Uri("https://free-proxy-list.net");
        client.Timeout = TimeSpan.FromSeconds(10);
    }
}