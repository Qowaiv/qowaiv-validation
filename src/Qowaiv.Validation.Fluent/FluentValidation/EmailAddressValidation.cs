namespace FluentValidation;

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
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(emailAddress => !emailAddress.IsIPBased)
        .WithMessage(_ => QowaivValidationFluentMessages.NoIPBasedEmailAddress);
}
