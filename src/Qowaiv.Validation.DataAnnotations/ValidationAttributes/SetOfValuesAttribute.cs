using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>Base <see cref="ValidationAttribute"/> for allowing or forbidding a set of values.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class SetOfValuesAttribute : ValidationAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="SetOfValuesAttribute"/> class.</summary>
        protected SetOfValuesAttribute(string value1, string value2)
            : this(new[] { value1, value2 }) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="SetOfValuesAttribute"/> class.</summary>
        /// <param name="values">
        /// String representations of the values.
        /// </param>
        protected SetOfValuesAttribute(params string[] values)
            : base(() => QowaivValidationMessages.AllowedValuesAttribute_ValidationError)
        {
            Values = Guard.NotNull(values, nameof(values));
        }

        /// <summary>The result to return when the value of <see cref="IsValid(object)"/>
        /// equals one of the values of the <see cref="SetOfValuesAttribute"/>.
        /// </summary>
        protected abstract bool OnEqual { get; }

        /// <summary>Gets the values.</summary>
        public string[] Values { get; }

        /// <summary>Returns true if the value occurs to be forbidden, otherwise false.</summary>
        public sealed override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var converter = TypeDescriptor.GetConverter(value.GetType());
            return OnEqual == Values.Any(val => value.Equals(converter.ConvertFromString(val)));
        }
    }
}
