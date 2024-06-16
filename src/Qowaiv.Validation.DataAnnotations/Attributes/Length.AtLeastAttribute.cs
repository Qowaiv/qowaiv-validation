namespace Qowaiv.Validation.DataAnnotations;

public static partial class Length
{
    /// <summary>Specifies the minimum the length of property, field or parameter.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class AtLeast(long minimum)
        : ValidationAttribute(() => QowaivValidationMessages.Length_AtLeast_ValdationError)
    {
        /// <summary>The minimum length.</summary>
        public long Minimum { get; } = minimum;

        /// <inheritdoc />
        [Pure]
        public override bool IsValid(object? value)
            => GetLength(value) is not long length
            || length >= Minimum;

        /// <inheritdoc />
        [Pure]
        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Minimum);
    }
}
