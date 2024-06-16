namespace Qowaiv.Validation.DataAnnotations;

public static partial class Length
{
    /// <summary>Specifies the maximum the length of property, field or parameter.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class AtMost(long maximum)
        : ValidationAttribute(() => QowaivValidationMessages.Length_AtMost_ValidationError)
    {
        /// <summary>The maximum length.</summary>
        public long Maximum { get; } = maximum;

        /// <inheritdoc />
        [Pure]
        public override bool IsValid(object? value)
            => GetLength(value) is not long length
            || length <= Maximum;

        /// <inheritdoc />
        [Pure]
        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Maximum);
    }
}
