using AutoMapper;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Infrastructure.Extensions;

namespace ProxyCollectorNET.Dto;

public class FreeProxyListMapperProfile : Profile
{
    public FreeProxyListMapperProfile()
    {
        CreateMap<FreeProxyListResponse, Address>()
            .ForMember(dest => dest.Organisation,
                opt => opt.Ignore())
            .ForMember(dest => dest.IpAddress,
                opt => opt.MapFrom(src => src.IpAddress))
            .ForMember(dest => dest.Port,
                opt => opt.MapFrom(src => src.Port))
            .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Protocols,
                opt => opt.MapFrom<FreeProxyListProtocolMapper>())
            .ForMember(dest => dest.Latency,
                opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt,
                opt => opt.Ignore());

        CreateMap<List<FreeProxyListResponse>, List<Address>>()
            .ConvertUsing<FreeProxyListResponseListConverter>();
    }
}

public class FreeProxyListProtocolMapper : IValueResolver<FreeProxyListResponse, Address, string[]?>
{
    public string[]? Resolve(FreeProxyListResponse source, Address destination, string[]? destMember,
        ResolutionContext context)
    {
        if (source.Https.IsNullOrEmpty()) return new[] {"HTTP"};

        return source.Https.Equals("yes") ? new[] {"HTTPS"} : new[] {"HTTP"};
    }
}

public class FreeProxyListResponse
{
    public string? IpAddress { get; set; }
    public string? Port { get; set; }
    public string? CountryCode { get; set; }
    public string? Country { get; set; }
    public string? Anonymity { get; set; }
    public string? Google { get; set; }
    public string? Https { get; set; }
    public string? LastChecked { get; set; }
}

public class FreeProxyListResponseListConverter : ITypeConverter<List<FreeProxyListResponse>, List<Address>>
{
    public List<Address> Convert(List<FreeProxyListResponse> source, List<Address> destination,
        ResolutionContext context)
    {
        return source.Select(p => context.Mapper.Map<Address>(p)).ToList();
    }
}