﻿using Qowaiv.Validation.Abstractions;
using System.Collections.Generic;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Compares two instances of <see cref="IValidationMessage"/>.</summary>
    public class ValidationMessageComparer : IEqualityComparer<IValidationMessage>
    {
        /// <inheritdoc />
        public bool Equals(IValidationMessage x, IValidationMessage y)
        {
            if (x is null || y is null)
            {
                return ReferenceEquals(x, y);
            }
            return x.Message == y.Message
                && x.Severity == y.Severity
                && x.PropertyName == y.PropertyName;
        }

        /// <inheritdoc />
        public int GetHashCode(IValidationMessage obj)
        {
            if (obj is null)
            {
                return 0;
            }
            return (obj.Message ?? string.Empty).GetHashCode()
                ^ (obj.PropertyName ?? string.Empty).GetHashCode()
                ^ (int)obj.Severity;
        }
    }
}
