using FluentValidation.Validators;
using Qowaiv;
using Qowaiv.Validation.Fluent;
using System;
using System.Linq;

namespace FluentValidation
{
    /// <summary>Fluent validation for Single Value Objects being required.</summary>
    public static class RequiredValidation
    {
        /// <summary>The property is required.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// /// <typeparam name="TProperty">
        /// Type of the property being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        public static IRuleBuilderOptions<TModel, TProperty> Required<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder)
            => ruleBuilder.Required(allowUnknown: false);

        /// <summary>The property is required.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// /// <typeparam name="TProperty">
        /// Type of the property being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="allowUnknown">
        /// If true, unknown values are seen 
        /// </param>
        public static IRuleBuilderOptions<TModel, TProperty> Required<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder, bool allowUnknown)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            return allowUnknown
                ? ruleBuilder
                    .NotEmpty().WithMessage(QowaivValidationFluentMessages.Required)
                : ruleBuilder
                    .NotEmptyOrUnknown().WithMessage(QowaivValidationFluentMessages.Required)
                ;
        }

        public static IRuleBuilderOptions<TModel, TProperty> RequiredWhen<TModel, TProperty>(
            this IRuleBuilder<TModel, TProperty> ruleBuilder,
            Func<TModel, bool> condition)
            => ruleBuilder.RequiredWhen(condition, false);

        public static IRuleBuilderOptions<TModel, TProperty> RequiredWhen<TModel, TProperty>(
            this IRuleBuilder<TModel, TProperty> ruleBuilder, 
            Func<TModel, bool> condition,
            bool allowUnknown)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));
            Guard.NotNull(condition, nameof(condition));

            var predicate = new PredicateValidator((m, val, propertyValidatorContext) => condition((TModel)m));
            var validator = new ConditnalValidator(predicate,
                allowUnknown
                ? new NotEmptyOrUnknownValidator(default(TProperty))
                : new NotEmptyValidator(default(TProperty)));

            return ruleBuilder.SetValidator(validator).WithMessage(QowaivValidationFluentMessages.Required);
        }
    }

    internal class ConditnalValidator : PropertyValidator
    {
        public ConditnalValidator(PredicateValidator when, PropertyValidator required)
            : base((string)null)
        {
            When = when;
            Required = required;
        }

        protected override bool IsValid(PropertyValidatorContext context)
            => When.Validate(context).Any() || !Required.Validate(context).Any();

        private PredicateValidator When { get; }
        private PropertyValidator Required { get; }
    }
}
