using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>Decorates a property or field as optional.</summary>
    /// <remarks>
    /// Null object pattern implementation for a <see cref="RequiredAttribute"/>.
    /// See: https://en.wikipedia.org/wiki/Null_object_pattern.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class OptionalAttribute : RequiredAttribute
    {
        /// <summary>Gets a (singleton) <see cref="OptionalAttribute"/>.</summary>
        internal static readonly OptionalAttribute Optional = new();

        /// <summary>Returns true as an <see cref="OptionalAttribute"/> is always valid.</summary>
        [Pure]
        public override bool IsValid(object value) => true;
    }
}
