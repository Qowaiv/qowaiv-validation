using Qowaiv.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is mandatory (for value types the default value is not allowed).</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
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
    /// </remarks>
    [Pure]
    private static Type? GetMemberType(ValidationContext context)
    {
        if (string.IsNullOrEmpty(context.MemberName)) return null;
        else return context.ObjectType.GetProperty(context.MemberName)?.PropertyType
            ?? context.ObjectType.GetField(context.MemberName)?.FieldType;
    }

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
        if (value is { })
        {
            var type = memberType ?? value.GetType();
            var underlyingType = QowaivType.GetNotNullableType(type);

            if (!AllowUnknownValue && value.Equals(Unknown.Value(underlyingType)))
            {
                return false;
            }
            else if (type.IsValueType)
            {
                return !value.Equals(Activator.CreateInstance(type));
            }
        }
        return base.IsValid(value);
    }
}
