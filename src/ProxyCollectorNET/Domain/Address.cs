namespace ProxyCollectorNET.Domain;

public class Address
{
    public string? Organisation { get; set; }
    public string? IpAddress { get; set; }
    public string? Port { get; set; }
    public string? Country { get; set; }
    public string[]? Protocols { get; set; }
    public int? Latency { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
}