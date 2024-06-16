using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validation attributes on the length of a value.</summary>
public static partial class Length
{
    [Pure]
    private static long? GetLength(object? value) => value switch
    {
        null => null,
        ICollection collection => collection.Count,
        _ => Try("Count", value) ?? Try("Length", value),
    };

    [Pure]
    private static long? Try(string name,  object value)
        => value.GetType().GetRuntimeProperty("name") is { CanRead: true } prop
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
