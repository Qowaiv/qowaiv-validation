using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Reflection;

internal static class ObjectWrapper
{
    /// <summary>Tries to resolve the value of the length property.</summary>
    [Pure]
    public static long? TryLength(this object obj) => obj switch
    {
        string str => str.Length,
        Array array => array.Length,
        _ => Try("Length", obj),
    };

    /// <summary>Tries to resolve the value of the count property.</summary>
    [Pure]
    public static long? TryCount(this object obj) => obj switch
    {
        ICollection collection => collection.Count,
        _ => Try("Count", obj) ?? (obj as IEnumerable)?.Cast<object>().Count(),
    };

    [Pure]
    private static long? Try(string name, object value)
        => value.GetType().GetRuntimeProperty(name) is { CanRead: true } prop
            ? GetValue(value, prop)
            : null;

    [Pure]
    private static long? GetValue(object value, PropertyInfo property) => property.PropertyType switch
    {
        var type when type == typeof(int) => (int?)property.GetValue(value),
        var type when type == typeof(long) => (long?)property.GetValue(value),
        _ => null,
    };
}
