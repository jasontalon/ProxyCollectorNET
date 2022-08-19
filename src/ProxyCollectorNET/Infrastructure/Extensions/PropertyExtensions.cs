namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class PropertyExtension
{
    public static void Set(this object obj, string propertyName, object? value)
        => obj.GetType().GetProperty(propertyName)?.SetValue(obj, value, null);

    public static void Set<TVal>(this object obj, Dictionary<string, TVal> properties)
    {
        foreach (var property in properties)
            obj.Set(property.Key, property.Value ?? default);
    }
}