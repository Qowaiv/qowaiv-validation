﻿using Qowaiv.IO;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Validation attributes on the size of a value.</summary>
public static partial class Size
{
    /// <summary>Specifies the minimum the size of property, field or parameter.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class AtLeastAttribute(long minimum)
        : ValidationAttribute(() => QowaivValidationMessages.Size_AtLeast_ValdationError)
    {
        /// <summary>Initializes a new instance of the <see cref="AtLeastAttribute"/> class.</summary>
        public AtLeastAttribute(string minimum)
            : this((long)StreamSize.Parse(minimum, CultureInfo.InvariantCulture)) { }

        /// <summary>The minimum size.</summary>
        public StreamSize Minimum { get; } = minimum;

        /// <inheritdoc />
        [Pure]
        public override bool IsValid(object? value)
            => GetSize<AtLeastAttribute>(value) is not StreamSize size
            || size == StreamSize.Zero
            || size >= Minimum;

        /// <inheritdoc />
        [Pure]
        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Minimum);
    }
}
