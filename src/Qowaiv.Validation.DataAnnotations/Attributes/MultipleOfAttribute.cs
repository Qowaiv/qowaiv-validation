using Qowaiv.Financial;

namespace Qowaiv.Validation.DataAnnotations.Attributes;

/// <summary>Specifies that a field is a multiple of a factor.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MultipleOfAttribute : ValidationAttribute
{
    /// <summary>Creates a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    public MultipleOfAttribute(double factor)
    {
        Factor = (decimal)factor;
    }

    /// <summary>The factor of which the value has to be a multiple of.</summary>
    public decimal Factor { get;  }

    /// <inheritdoc />
    [Pure]
    public override bool IsValid(object? value) => value switch
    {
        double dbl => IsValid(dbl),
        decimal dec => IsValid(dec),
        Amount amount => IsValid((decimal)amount),
        Money money => IsValid((decimal)money.Amount),
        Percentage perentage => IsValid(100 * (decimal)perentage),
        _ => true,
    };

    [Pure]
    private bool IsValid(decimal value) 
        => value.RoundToMultiple(Factor) == value;

    [Pure]
    private bool IsValid(double value)
        => !double.IsNaN(value)
        && !double.IsInfinity(value)
        && value >= decimal_MinValue
        && value <= decimal_MaxValue
        && IsValid((decimal)value);
    
    private const double decimal_MinValue = (double)decimal.MinValue;
    private const double decimal_MaxValue = (double)decimal.MaxValue;
}
