using AutoMapper;
using Newtonsoft.Json;
using ProxyCollectorNET.Domain;

namespace ProxyCollectorNET.Dto;

public class GeonodeResponse : Profile
{
    public GeonodeResponse()
    {
        CreateMap<GeonodeResponse, List<Address>>()
            .ConvertUsing<GeonodeResponseToAddressConverter>();
    }

    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public GeoNodeResponseSummary? Summary { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public List<GeonodeResponseDatum>? Data { get; set; }

    [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
    public long? Total { get; set; }

    [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
    public string? Page { get; set; }

    [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
    public string? Limit { get; set; }
}

public class GeonodeResponseDatum
{
    [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
    public string? Ip { get; set; }

    [JsonProperty("anonymityLevel", NullValueHandling = NullValueHandling.Ignore)]
    public string? AnonymityLevel { get; set; }

    [JsonProperty("asn", NullValueHandling = NullValueHandling.Ignore)]
    public string? Asn { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }

    [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
    public string? Country { get; set; }

    [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonProperty("google", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Google { get; set; }

    [JsonProperty("isp", NullValueHandling = NullValueHandling.Ignore)]
    public string? Isp { get; set; }

    [JsonProperty("lastChecked", NullValueHandling = NullValueHandling.Ignore)]
    public long? LastChecked { get; set; }

    [JsonProperty("latency")] public decimal? Latency { get; set; }

    [JsonProperty("org", NullValueHandling = NullValueHandling.Ignore)]
    public string? Org { get; set; }

    [JsonProperty("port", NullValueHandling = NullValueHandling.Ignore)]
    public string? Port { get; set; }

    [JsonProperty("protocols", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Protocols { get; set; }

    [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
    public long? Speed { get; set; }

    [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? UpdatedAt { get; set; }    

    [JsonProperty("upTime", NullValueHandling = NullValueHandling.Ignore)]
    public long? UpTime { get; set; }

    [JsonProperty("upTimeSuccessCount", NullValueHandling = NullValueHandling.Ignore)]
    public long? UpTimeSuccessCount { get; set; }

    [JsonProperty("upTimeTryCount", NullValueHandling = NullValueHandling.Ignore)]
    public long? UpTimeTryCount { get; set; }
}

public class GeoNodeResponseSummary : Profile
{
    public GeoNodeResponseSummary()
    {
        CreateMap<GeonodeResponseDatum, Address>()
            .ForMember(dest => dest.Organisation, opt => opt.MapFrom(src => src.Org))
            .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.Ip))
            .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Protocols, opt => opt.MapFrom(src => src.Protocols))
            .ForMember(dest => dest.Latency, opt => opt.MapFrom(src => src.Latency))
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.LastChecked ?? 0)));
    }

    [JsonProperty("lastUpdated", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonProperty("proxiesOnline", NullValueHandling = NullValueHandling.Ignore)]
    public long? ProxiesOnline { get; set; }

    [JsonProperty("countriesOnline", NullValueHandling = NullValueHandling.Ignore)]
    public long? CountriesOnline { get; set; }
}

public class GeonodeResponseToAddressConverter : ITypeConverter<GeonodeResponse, List<Address>?>
{
    private readonly IMapper _mapper;

    public GeonodeResponseToAddressConverter(IMapper mapper) => _mapper = mapper;

    public List<Address>? Convert(GeonodeResponse source, List<Address>? destination, ResolutionContext context)
        => source.Data?.Select(data => _mapper.Map<Address>(data)).ToList();
}