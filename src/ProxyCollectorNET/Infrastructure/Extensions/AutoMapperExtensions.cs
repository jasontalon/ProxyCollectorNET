using AutoMapper;

namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class AutoMapperExtensions
{
    public static (TDest? Destination, string Error) TryMap<TSource, TDest>(this IMapper @this, TSource source)
    {
        try
        {
            var dest = @this.Map<TDest>(source);

            return (dest, null)!;
        }
        catch (Exception ex)
        {
            return (default, ex.ToString())!;
        }
    }
}