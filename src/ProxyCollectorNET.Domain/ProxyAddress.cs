namespace ProxyCollectorNET.Domain;

public class ProxyAddress
{
    public string IPAddress { get; set; }
    public string Port { get; set; }
    public string? Type { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? Organisation { get; set; }
}