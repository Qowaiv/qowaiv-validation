using Qowaiv.Validation.Abstractions;
using System;
using System.Collections.Generic;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Compares two instances of <see cref="IValidationMessage"/>.</summary>
    [Obsolete("Use Qowaiv.Validation.Abstractions.ValidationMessageCompare.ByInterface instead.")]
    public class ValidationMessageComparer : IEqualityComparer<IValidationMessage>
    {
        /// <inheritdoc />
        public bool Equals(IValidationMessage x, IValidationMessage y)
            => ValidationMessageCompare.ByInterface.Equals(x, y);

        /// <inheritdoc />
        public int GetHashCode(IValidationMessage obj)
            => ValidationMessageCompare.ByInterface.GetHashCode(obj);
    }
}
