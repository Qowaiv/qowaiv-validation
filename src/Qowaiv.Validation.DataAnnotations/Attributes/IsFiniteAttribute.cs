namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is a finite number.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
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
    public override bool IsValid(object? value)
        => value is null
        || value switch
        {
            float flt => flt.IsFinite(),
            double dbl => dbl.IsFinite(),
            _ => false,
        };
}
