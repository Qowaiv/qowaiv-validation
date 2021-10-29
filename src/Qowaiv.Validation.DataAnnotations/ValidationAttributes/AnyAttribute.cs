using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>Specifies that a field should at least have one item in its collection.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AnyAttribute : RequiredAttribute
    {
        /// <summary>Returns true if the value is not null and the collection
        /// has any item, otherwise false.
        /// </summary>
        [Pure]
        public override bool IsValid(object value)
            => value is IEnumerable enumerable
            ? enumerable.GetEnumerator().MoveNext()
            : base.IsValid(value);
    }
}
