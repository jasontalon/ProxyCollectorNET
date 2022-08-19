using AutoMapper;
using Newtonsoft.Json;
using ProxyCollectorNET.Domain;

namespace ProxyCollectorNET.Dto;

public class ProxyscanMapperProfile : Profile
{
    public ProxyscanMapperProfile()
    {
        CreateMap<ProxyscanResponse, Address>()
            .ForMember(dest => dest.Organisation, opt => opt.MapFrom<ProxyscanResponseOrgResolver>())
            .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.Ip))
            .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
            .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.Location != null ? src.Location.Country : null))
            .ForMember(dest => dest.Protocols, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Latency, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time ?? 0)));
    }
}

public class ProxyscanResponse
{
    [JsonProperty("Ip", NullValueHandling = NullValueHandling.Ignore)]
    public string? Ip { get; set; }

    [JsonProperty("Port", NullValueHandling = NullValueHandling.Ignore)]
    public long? Port { get; set; }

    [JsonProperty("Ping", NullValueHandling = NullValueHandling.Ignore)]
    public long? Ping { get; set; }

    [JsonProperty("Time", NullValueHandling = NullValueHandling.Ignore)]
    public long? Time { get; set; }

    [JsonProperty("Location", NullValueHandling = NullValueHandling.Ignore)]
    public ProxyscanResponseLocation? Location { get; set; }

    [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Type { get; set; }

    [JsonProperty("Failed", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Failed { get; set; }

    [JsonProperty("Anonymity", NullValueHandling = NullValueHandling.Ignore)]
    public string? Anonymity { get; set; }

    [JsonProperty("WorkingCount", NullValueHandling = NullValueHandling.Ignore)]
    public long? WorkingCount { get; set; }

    [JsonProperty("Uptime", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Uptime { get; set; }

    [JsonProperty("RecheckCount", NullValueHandling = NullValueHandling.Ignore)]
    public long? RecheckCount { get; set; }
}

public class ProxyscanResponseOrgResolver : IValueResolver<ProxyscanResponse, Address, string?>
{
    public string? Resolve(ProxyscanResponse source, Address destination, string? destMember, ResolutionContext context)
        => source.Location?.Org ?? source.Location?.Isp;
}

public class ProxyscanResponseLocation : Profile
{
    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }

    [JsonProperty("continent")] public string? Continent { get; set; }

    [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
    public string? Country { get; set; }

    [JsonProperty("countryCode", NullValueHandling = NullValueHandling.Ignore)]
    public string? CountryCode { get; set; }

    [JsonProperty("ipName")] public string? IpName { get; set; }

    [JsonProperty("ipType")] public string? IpType { get; set; }

    [JsonProperty("isp")] public string? Isp { get; set; }

    [JsonProperty("lat")] public string? Lat { get; set; }

    [JsonProperty("lon")] public string? Lon { get; set; }

    [JsonProperty("org")] public string? Org { get; set; }

    [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
    public string? Query { get; set; }

    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string? Region { get; set; }

    [JsonProperty("status")] public string? Status { get; set; }
}