using ProxyCollectorNET.Domain;

namespace ProxyCollectorNET.Collectors;

public interface ICollector
{
    public Task<(List<Address>? Addresses, Exception? Exception)> FetchAsync(CancellationToken cancellationToken = default);
}