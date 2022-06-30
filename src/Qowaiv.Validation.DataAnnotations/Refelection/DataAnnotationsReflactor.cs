using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Refelection;

internal static class DataAnnotationsReflactor
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

    /// <summary>Gets the decorated <see cref="DisplayAttribute"/> for the property.</summary>
    [Pure]
    public static DisplayAttribute DisplayAttribute(this PropertyInfo property)
        => property.GetCustomAttribute<DisplayAttribute>()
        ?? new DisplayAttribute { Name = property.Name };
}
