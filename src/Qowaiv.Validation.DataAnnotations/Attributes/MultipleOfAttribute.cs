using Qowaiv.Financial;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is a multiple of a factor.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Validates(typeof(float))]
[Validates(typeof(double))]
[Validates(typeof(decimal))]
[Validates(typeof(Amount))]
[Validates(typeof(Money))]
[Validates(typeof(Percentage))]
[Validates(typeof(IConvertible))]
public sealed class MultipleOfAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    public MultipleOfAttribute(double factor)
        : this(AsDecimal(factor) ?? throw new ArgumentOutOfRangeException(nameof(factor), "Can not be represented by a decimal.")) { }

    /// <summary>Initializes a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    private MultipleOfAttribute(decimal factor)
    {
        Factor = factor;
        ErrorMessageResourceType = typeof(QowaivValidationMessages);
        ErrorMessageResourceName = nameof(QowaivValidationMessages.MultipleOfAttribute_ValidationError);
    }

    /// <summary>The factor of which the value has to be a multiple of.</summary>
    public decimal Factor { get; }

    /// <inheritdoc />
    [Pure]
    public override string FormatErrorMessage(string? name)
        => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Factor);

    /// <inheritdoc />
    [Pure]
    public override bool IsValid(object? value) => value switch
    {
        null => true,
        float flt => IsValid(flt),
        double dbl => IsValid(dbl),
        decimal dec => IsValid(dec),
        Amount amount => IsValid((decimal)amount),
        Money money => IsValid((decimal)money.Amount),
        Percentage percentage => IsValid(100 * (decimal)percentage),
        IConvertible convertible => IsValid(convertible),
        _ => throw UnsupportedType.ForAttribute<MultipleOfAttribute>(value.GetType()),
    };

    [Pure]
    private bool IsValid(decimal value) => value % Factor == 0;

    [Pure]
    private bool IsValid(float value)
       => value.IsFinite()
       && AsDecimal(value) is { } dec
       && IsValid(dec);

    [Pure]
    private bool IsValid(double value)
        => value.IsFinite()
        && AsDecimal(value) is { } dec
        && IsValid(dec);

    [Pure]
    private bool IsValid(IConvertible value)
        => IsValid(value.GetTypeCode())
        && IsValid(Convert.ToDecimal(value));

    [Pure]
    private static bool IsValid(TypeCode type)
        => type >= TypeCode.SByte
        && type <= TypeCode.UInt64;

    [Pure]
    private static decimal? AsDecimal(float value)
        => value >= Decimal_MinValue
        && value <= Decimal_MaxValue
            ? (decimal)value
            : null;

    [Pure]
    private static decimal? AsDecimal(double value)
        => value >= Decimal_MinValue
        && value <= Decimal_MaxValue
            ? (decimal)value
            : null;

    private const double Decimal_MinValue = (double)decimal.MinValue;
    private const double Decimal_MaxValue = (double)decimal.MaxValue;
}
