using Qowaiv.Financial;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is a multiple of a factor.</summary>
[AttributeUsage(AttributeTarget.Member, AllowMultiple = false)]
[Validates(typeof(decimal))]
[Validates(typeof(Amount))]
[Validates(typeof(Money))]
[Validates(typeof(Percentage))]
[Validates(typeof(sbyte))]
[Validates(typeof(byte))]
[Validates(typeof(short))]
[Validates(typeof(ushort))]
[Validates(typeof(int))]
[Validates(typeof(uint))]
[Validates(typeof(long))]
[Validates(typeof(ulong))]
public sealed class MultipleOfAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    public MultipleOfAttribute(string factor)
        : this(decimal.Parse(factor, CultureInfo.InvariantCulture)) { }

    /// <summary>Initializes a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    /// <remarks>
    /// Supports up to 10e-15. For smaller factors use <see cref="MultipleOfAttribute(string)" />.
    /// </remarks>
    public MultipleOfAttribute(double factor)
        : this(AsDecimal(factor) ?? throw new ArgumentOutOfRangeException(nameof(factor), "Can not be represented by a decimal.")) { }

    /// <summary>Initializes a new instance of the <see cref="MultipleOfAttribute"/> class.</summary>
    private MultipleOfAttribute(decimal factor) : base(() => QowaivValidationMessages.MultipleOfAttribute_ValidationError)
        => Factor = factor <= 0
            ? throw new ArgumentOutOfRangeException(nameof(factor), "The factor can not be zero or less.")
            : factor;

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
        null /*...........*/ => true,
        decimal v /*......*/ => IsValid(v),
        Amount v /*.......*/ => IsValid((decimal)v),
        Money v /*........*/ => IsValid((decimal)v.Amount),
        Percentage v /*...*/ => IsValid(100 * (decimal)v),
        IConvertible v when Supported(v.GetTypeCode()) => IsValid(Convert.ToDecimal(v)),
        _ /*..............*/ => throw UnsupportedType.ForAttribute<MultipleOfAttribute>(value.GetType()),
    };

    [Pure]
    private bool IsValid(decimal value) => value % Factor == 0;

    [Pure]
    private static bool Supported(TypeCode type)
        => type is >= TypeCode.SByte and <= TypeCode.UInt64;

    /// <summary>Converts a <see cref="double"/> to a <see cref="decimal"/>.</summary>
    /// <remarks>
    /// Round to 15 decimals to strip the binary representation noise,
    /// and normalize to remove the trailing zeros so the factor is
    /// stable across runtimes (and formats predictably).
    /// </remarks>
    [Pure]
    private static decimal? AsDecimal(double value)
        => value is >= Decimal_MinValue_as_d and <= Decimal_MaxValue_as_d
        ? (((decimal)value).Round(15) / 1.00_000_000_000_000_000_000_000_000_0m)
        : null;

    private const double Decimal_MinValue_as_d = (double)decimal.MinValue;
    private const double Decimal_MaxValue_as_d = (double)decimal.MaxValue;
}
