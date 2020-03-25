using Qowaiv;
using Qowaiv.Validation.Fluent;

namespace FluentValidation
{
    /// <summary>Fluent validation for Single Value Objects being (not) unknown.</summary>
    public static class UnknownValidation
    {
        /// <summary>Defines a 'not unknown' validator on the current rule builder.
        /// Validation will fail if the property has the 'unknown' value for the type.
        /// </summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// /// <typeparam name="TProperty">
        /// Type of the property being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        public static IRuleBuilderOptions<TModel, TProperty> NotUnknown<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder)
        {
            return Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
                .Must(prop => prop is null || !Equals(Unknown.Value(typeof(TProperty)), prop))
                .WithMessage(m => QowaivValidationFluentMessages.NotUnknown);
        }

        /// <summary>Defines a 'not empty' and a 'not unkmown' validator on the current rule builder.</summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        public static IRuleBuilderOptions<T, TProperty> NotEmptyOrUnknown<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
                .NotEmpty()
                .NotUnknown();
        }
    }
}
