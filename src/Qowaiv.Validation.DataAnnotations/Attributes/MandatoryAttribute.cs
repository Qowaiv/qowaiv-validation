using Qowaiv.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is mandatory (for value types the default value is not allowed).</summary>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[Validates(typeof(object))]
public sealed class MandatoryAttribute : RequiredAttribute
{
    /// <summary>Initializes a new instance of the <see cref="MandatoryAttribute"/> class.</summary>
    public MandatoryAttribute()
    {
        ErrorMessageResourceType = typeof(QowaivValidationMessages);
        ErrorMessageResourceName = nameof(QowaivValidationMessages.MandatoryAttribute_ValidationError);
    }

    /// <summary>Gets or sets a value that indicates whether an empty string is allowed.</summary>
    public bool AllowUnknownValue { get; set; }

    /// <inheritdoc />
    public override bool RequiresValidationContext => true;

    /// <inheritdoc />
    [Pure]
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        => IsValid(value, GetMemberType(Guard.NotNull(validationContext)))
        ? null
        : ValidationMessage.Error(FormatErrorMessage(validationContext.DisplayName), validationContext.MemberNames());

    /// <summary>Gets the type of the field/property.</summary>
    /// <remarks>
    /// Because the values of the member are boxed, this is (unfortunately)
    /// the only way to determine if the provided value is a nullable type,
    /// or not.
    /// The resolved type is cached, as a single attribute instance is bound
    /// to a single member.
    /// </remarks>
    [Pure]
    private Type? GetMemberType(ValidationContext context)
    {
        var member = context.MemberName;
        if (member == CachedMember && ReferenceEquals(context.ObjectType, CachedObjectType)) return CachedMemberType;

        CachedMember = member;
        CachedObjectType = context.ObjectType;
        CachedMemberType = member is { Length: > 0 }
            ? context.ObjectType.GetProperty(member)?.PropertyType ?? context.ObjectType.GetField(member)?.FieldType
            : null;

        return CachedMemberType;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? CachedMember;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Type? CachedObjectType;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Type? CachedMemberType;

    /// <summary>Returns true if the value is not null and value types are
    /// not equal to their default value, otherwise false.
    /// </summary>
    /// <remarks>
    /// The unknown value is expected to be static field or property of the type with the name "Unknown".
    /// </remarks>
    [Pure]
    public override bool IsValid(object? value) => IsValid(value, null);

    [Pure]
    private bool IsValid(object? value, Type? memberType)
    {
        if (value is null) return this.PostValidate(base.IsValid(value));

        var type = memberType ?? value.GetType();

        var result = !AllowUnknownValue && value.Equals(SvoValues.Unknown(QowaivType.GetNotNullableType(type)))
            ? false
            : type.IsValueType
            ? !value.Equals(SvoValues.Default(type))
            : base.IsValid(value);

        return this.PostValidate(result);
    }

}
