namespace ProxyCollectorNET.Infrastructure;

public interface IProxyCollectorStrategy
{
    Task WorkAsync(CancellationToken cancellationToken);
}