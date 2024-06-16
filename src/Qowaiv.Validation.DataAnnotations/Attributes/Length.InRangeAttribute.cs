namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validation attributes on the length of a value.</summary>
public static partial class Length
{
    /// <summary>Specifies the allowed range of the length of property, field or parameter.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class InRangeAttribute(int minimum, int maximum)
        : ValidationAttribute(() => QowaivValidationMessages.Length_InRange_ValidationError)
    {
        /// <summary>The minimum length.</summary>
        public long Minimum { get; } = minimum;

        /// <summary>The maximum length.</summary>
        public long Maximum { get; } = maximum;

        /// <inheritdoc />
        [Pure]
        public override bool IsValid(object? value)
            => GetLength(value) is not long length
            || (length >= Minimum && length <= Maximum);

        /// <inheritdoc />
        [Pure]
        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Minimum, Maximum);
    }
}
