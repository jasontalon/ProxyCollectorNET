namespace ProxyCollectorNET.Infrastructure;

public interface IProxyCollectorStrategy
{
    Task Work(CancellationToken cancellationToken);
    
}