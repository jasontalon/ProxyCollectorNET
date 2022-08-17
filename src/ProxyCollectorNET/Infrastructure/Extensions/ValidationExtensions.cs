namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class ValidationExtensions
{
    public static bool Validate<T>(this T @this, params Func<T, bool>[] predicates)
    {
        return predicates.All(p => p(@this));
    }

    public static string? Validate<T>(this T @this, params Func<T, string>[] predicates)
    {
        foreach (var predicate in predicates)
        {
            var message = predicate(@this);
            if (message.IsNotNullOrEmpty()) return message;
        }

        return null;
    }
}