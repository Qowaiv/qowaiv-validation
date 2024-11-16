using Qowaiv.IO;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validation attributes on the size of a value.</summary>
public static partial class Size
{
    /// <summary>Specifies the allowed range of the size of property, field or parameter.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class InRangeAttribute(long minimum, long maximum)
        : ValidationAttribute(() => QowaivValidationMessages.Size_InRange_ValidationError)
    {
        /// <summary>Initializes a new instance of the <see cref="InRangeAttribute"/> class.</summary>
        public InRangeAttribute(string minimum, string maximum) : this(
            (long)StreamSize.Parse(minimum, CultureInfo.InvariantCulture),
            (long)StreamSize.Parse(maximum, CultureInfo.InvariantCulture)) { }

        /// <summary>The minimum length.</summary>
        public StreamSize Minimum { get; } = minimum;

        /// <summary>The maximum length.</summary>
        public StreamSize Maximum { get; } = maximum;

        /// <inheritdoc />
        [Pure]
        public override bool IsValid(object? value)
            => GetSize<InRangeAttribute>(value) is not StreamSize size
            || size == StreamSize.Zero // should be dealt with by mandatory/required.
            || (size >= Minimum && size <= Maximum);

        /// <inheritdoc />
        [Pure]
        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Minimum, Maximum);
    }
}
