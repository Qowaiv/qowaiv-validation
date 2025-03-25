namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is a finite number.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Validates(typeof(float))]
[Validates(typeof(double))]
public sealed class IsFiniteAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="IsFiniteAttribute"/> class.</summary>
    public IsFiniteAttribute()
    {
        ErrorMessageResourceType = typeof(QowaivValidationMessages);
        ErrorMessageResourceName = nameof(QowaivValidationMessages.IsFiniteAttribute_ValidationError);
    }

    /// <inheritdoc />
    [Pure]
    public override bool IsValid(object? value) => value switch
    {
        null => true,
        float flt => flt.IsFinite(),
        double dbl => dbl.IsFinite(),
        _ => throw UnsupportedType.ForAttribute<IsFiniteAttribute>(value.GetType()),
    };
}
