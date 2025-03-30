using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Reflection;

internal abstract class Member
{
    /// <inheritdoc cref="MemberInfo.Name" />
    public abstract string Name { get; }

    /// <summary>Gets the type of the member.</summary>
    public abstract Type MemberType { get; }

    /// <summary>Indicates if the member can be read.</summary>
    public abstract bool CanRead { get; }

    /// <inheritdoc cref="MemberInfo.GetCustomAttributes(bool)" />
    [Pure]
    public abstract object[] GetCustomAttributes();

    /// <summary>Gets the value of the member.</summary>
    [Pure]
    public abstract object? GetValue(object obj);

    /// <summary>Indicates that the members is indexed.</summary>
    [Pure]
    public abstract bool IsNotIndexed { get; }

    /// <summary>
    /// Get the <see cref="RequiredMemberAttribute"/> attribute instance for reference types
    /// that are not nullable and defined as required properties.
    /// </summary>
    [Pure]
    public RequiredAttribute? GetRequiredMemberAttribute()
    {
        if (MemberType.IsNonNullableValueType()) return null;

        var attributes = GetCustomAttributes().Select(a => a.GetType());

        return attributes.Any(a => a.FullName == "System.Runtime.CompilerServices.RequiredMemberAttribute")
            && !attributes.Any(a => a.FullName == "System.Runtime.CompilerServices.NullableAttribute")
            ? RequiredMember
            : null;
    }

    private static readonly RequiredAttribute RequiredMember = new();

    /// <summary>Gets a field member.</summary>
    [Pure]
    public static Member New(FieldInfo info) => new Field(info);

    /// <summary>Gets a property member.</summary>
    [Pure]
    public static Member New(PropertyInfo prop) => new Property(prop);

    private sealed class Field(FieldInfo info) : Member
    {
        private readonly FieldInfo Info = info;

        public override string Name => Info.Name;

        public override Type MemberType => Info.FieldType;

        public override bool CanRead => true;

        [Pure]
        public override object[] GetCustomAttributes() => Info.GetCustomAttributes(inherit: true);

        [Pure]
        public override object? GetValue(object obj) => Info.GetValue(obj);

        [Pure]
        public override bool IsNotIndexed => true;
    }

    private sealed class Property(PropertyInfo prop) : Member
    {
        private readonly PropertyInfo Info = prop;

        public override string Name => Info.Name;

        public override Type MemberType => Info.PropertyType;

        public override bool CanRead => Info.CanRead;

        [Pure]
        public override object[] GetCustomAttributes() => Info.GetCustomAttributes(inherit: true);

        [Pure]
        public override object? GetValue(object obj) => Info.GetValue(obj);

        [Pure]
        public override bool IsNotIndexed => Info.GetIndexParameters() is not { Length: > 0 };
    }
}
