namespace System.Reflection;

internal static class PropertyInfoExtensions
{
    /// <summary>
    /// Get the <see cref="RequiredMember"/> attribute instance for reference types
    /// that are not nullable and defined as required properties.
    /// </summary>
    [Pure]
    public static RequiredAttribute? RequiredMemberAttribute(this PropertyInfo property)
    {
        if (property.PropertyType.IsNonNullableValueType()) return null;

        var attributes = property.GetCustomAttributes(inherit: true).Select(a => a.GetType());

        return attributes.Any(a => a.FullName == "System.Runtime.CompilerServices.RequiredMemberAttribute")
            && !attributes.Any(a => a.FullName == "System.Runtime.CompilerServices.NullableAttribute")
            ? RequiredMember
            : null;
    }

    private static readonly RequiredAttribute RequiredMember = new();
}
