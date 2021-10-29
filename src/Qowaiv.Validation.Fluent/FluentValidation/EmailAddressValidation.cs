using Qowaiv;
using Qowaiv.Validation.Abstractions.Diagnostics.Contracts;
using Qowaiv.Validation.Fluent;

namespace FluentValidation
{
    /// <summary>Fluent validation for <see cref="EmailAddress"/>.</summary>
    public static class EmailAddressValidation
    {
        /// <summary>Disallow IP-based email addresses.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        [FluentSyntax]
        public static IRuleBuilderOptions<TModel, EmailAddress> NotIPBased<TModel>(this IRuleBuilder<TModel, EmailAddress> ruleBuilder)
        {
            return Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
                .Must(emailAddress => !emailAddress.IsIPBased)
                .WithMessage(m => QowaivValidationFluentMessages.NoIPBasedEmailAddress);
        }
    }
}
