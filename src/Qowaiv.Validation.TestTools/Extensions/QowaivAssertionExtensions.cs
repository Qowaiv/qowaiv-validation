using FluentAssertions.Primitives;
using FluentAssertions.Qowaiv.Validation;
using Qowaiv.Validation.Abstractions;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace FluentAssertions
{
    /// <summary>Contains extension methods for custom assertions in unit tests.</summary>
    [DebuggerNonUserCode]
    public static class QowaivAssertionExtensions
    {
        /// <summary>
        /// Returns an <see cref="ResultAssertions"/> object that can be used to assert the
        /// current <see cref="Result"/>.
        /// </summary>
        [Pure]
        public static ResultAssertions Should(this Result result) => new(result);

        /// <summary>
        /// Returns an <see cref="ResultAssertions{T}"/> object that can be used to assert the
        /// current <see cref="Result{TModel}"/>.
        /// </summary>
        [Pure]
        public static ResultAssertions<TModel> Should<TModel>(this Result<TModel> result) => new(result);
    }
}
