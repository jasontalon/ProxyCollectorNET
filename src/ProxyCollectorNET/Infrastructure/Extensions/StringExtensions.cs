namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? @this) => string.IsNullOrEmpty(@this);
    public static bool IsNotNullOrEmpty(this string? @this) => !@this.IsNullOrEmpty();

    public static string? Coalesce(this string? @this, params string[] successors)
    {
        if (@this.IsNotNullOrEmpty()) return @this;

        if (successors.IsNullOrEmpty()) return null;

        if (successors.Length > 1)
            return successors.FirstOrDefault().Coalesce(successors[Range.StartAt(1)]);

        return successors.FirstOrDefault();
    }
}