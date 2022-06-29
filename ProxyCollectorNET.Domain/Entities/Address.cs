namespace ProxyCollectorNET.Domain.Entities;

public class Address
{
    public string IPAddress { get; set; }
    public string Port { get; set; }
    public string? Type { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? Organisation { get; set; }
    public string? Source { get; set; }
    public DateTimeOffset? CreatedAtUtc { get; set; }
}