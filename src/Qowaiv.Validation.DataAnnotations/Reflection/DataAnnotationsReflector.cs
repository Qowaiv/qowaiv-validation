using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Reflection;

internal static class DataAnnotationsReflector
{
    /// <summary>Returns true if type implements <see cref="IValidatableObject"/>,
    /// or if the type has been decorated with any <see cref="ValidationAttribute"/>'s,
    /// or any of its properties.
        /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Pure]
    public static bool IsValidatableObject(this Type type)
    {
        return typeof(IValidatableObject).IsAssignableFrom(type)
            || type.ValidationAttributes().Any()
            || type.GetProperties().Any(IsValidatable);

        static bool IsValidatable(PropertyInfo property)
            => typeof(IValidatableObject).IsAssignableFrom(property.PropertyType)
            || property.PropertyType.ValidationAttributes().Any()
            || property.ValidationAttributes().Any();
    }

    /// <summary>Returns true if the type is an <see cref="IEnumerable"/>.</summary>
    ///  <remarks>
    ///  Excludes <see cref="string"/> and <see cref="byte"/>[].
    ///  </remarks>
    [Pure]
    public static bool IsEnumerable(this Type type)
       => type != typeof(string)
        && type != typeof(byte[])
        && type.GetEnumerableType() is { };

    [Pure]
    private static Type? GetEnumerableType(this Type type)
        => type
        .GetInterfaces()
        .FirstOrDefault(iface =>
            iface.IsGenericType &&
            iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))?
        .GetGenericArguments()[0];

    /// <summary>Gets the decorated <see cref="System.ComponentModel.DataAnnotations.RequiredAttribute"/> for the property.</summary>
    [Pure]
    public static RequiredAttribute? RequiredAttribute(this PropertyInfo property)
        => property.GetCustomAttributes<RequiredAttribute>(inherit: true)
        .FirstOrDefault(attr => attr is not OptionalAttribute);

    /// <summary>Gets the decorated <see cref="ValidationAttribute"/>'s for the property.</summary>
    [Pure]
    public static IEnumerable<ValidationAttribute> ValidationAttributes(this PropertyInfo property)
        => property.GetCustomAttributes<ValidationAttribute>(inherit: true);

    /// <summary>Gets the decorated <see cref="ValidationAttribute"/>'s for the property.</summary>
    [Pure]
    public static IEnumerable<ValidationAttribute> ValidationAttributes(this Type type)
        => type.GetCustomAttributes<ValidationAttribute>(inherit: true);
}
