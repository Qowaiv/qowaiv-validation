﻿using Qowaiv.Financial;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that a field is a multiple of a factor.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
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
    public override bool RequiresValidationContext => true;

    /// <inheritdoc />
    [Pure]
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        => IsValid(value)
        ? ValidationResult.Success!
        : ValidationMessage.Error(FormatErrorMessage(validationContext.DisplayName), validationContext.MemberNames());

    /// <inheritdoc />
    [Pure]
    public override string FormatErrorMessage(string? name)
        => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Factor);

    /// <inheritdoc />
    [Pure]
    public override bool IsValid(object? value) => value switch
    {
        // should be handled by required.
        null => true,
        string str => string.IsNullOrEmpty(str),

        float flt => IsValid(flt),
        double dbl => IsValid(dbl),
        decimal dec => IsValid(dec),
        Amount amount => IsValid((decimal)amount),
        Money money => IsValid((decimal)money.Amount),
        Percentage percentage => IsValid(100 * (decimal)percentage),
        IConvertible convertible => IsValid(convertible),

        _ => false,
    };

    [Pure]
    private bool IsValid(decimal value) => value % Factor == 0;

    [Pure]
    private bool IsValid(float value)
       => IsFinite(value)
       && AsDecimal(value) is { } dec
       && IsValid(dec);

    [Pure]
    private bool IsValid(double value)
        => IsFinite(value)
        && AsDecimal(value) is { } dec
        && IsValid(dec);

    [Pure]
    private bool IsValid(IConvertible value)
        => IsValid(value.GetTypeCode()) 
        && IsValid(Convert.ToDecimal(value));

    [Pure]
    private bool IsValid(TypeCode type)
        => type >= TypeCode.SByte 
        && type <= TypeCode.UInt64;

    [Pure]
    private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);

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