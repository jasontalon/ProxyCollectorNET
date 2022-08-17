namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class ArrayExtensions
{
    public static bool IsNullOrEmpty<T>(this T[]? @this)
    {
        if (@this is null) return true;

        return !@this.Any();
    }
}