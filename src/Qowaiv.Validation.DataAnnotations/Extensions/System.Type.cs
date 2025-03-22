using System.Reflection;

namespace System;

internal static class TypeExtensions
{
    [Pure]
    public static Type? GetEnumerableType(this Type type) => type
        .GetInterfaces()
        .Find(iface =>
            iface.IsGenericType &&
            iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))?
        .GetGenericArguments()[0];

    /// <summary>True if the type implements <see cref="IValidatableObject"/>.</summary>
    [Pure]
    public static bool ImplementsIValidatableObject(this Type type) => typeof(IValidatableObject).IsAssignableFrom(type);

    /// <summary>Returns true for all value types except <see cref="Nullable{T}" />,</summary>
    [Pure]
    public static bool IsNonNullableValueType(this Type type)
        => type.IsValueType
        && (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>));

    /// <summary>Gets the decorated <see cref="ValidationAttribute"/>'s for the property.</summary>
    [Pure]
    public static IEnumerable<ValidationAttribute> ValidationAttributes(this Type type)
        => type.GetCustomAttributes<ValidationAttribute>(inherit: true);
}
