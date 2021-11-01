using Qowaiv.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Compares two instances of <see cref="IValidationMessage"/>.</summary>
    [Obsolete("Use Qowaiv.Validation.Abstractions.ValidationMessageCompare.ByInterface instead.")]
    public class ValidationMessageComparer : IEqualityComparer<IValidationMessage>
    {
        /// <inheritdoc />
        [Pure]
        public bool Equals(IValidationMessage x, IValidationMessage y)
            => ValidationMessageCompare.ByInterface.Equals(x, y);

        /// <inheritdoc />
        [Pure]
        public int GetHashCode(IValidationMessage obj)
            => ValidationMessageCompare.ByInterface.GetHashCode(obj);
    }
}
