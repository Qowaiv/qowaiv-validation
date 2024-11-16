namespace FluentValidation;

/// <summary>Fluent validation for <see cref="PostalCode"/>.</summary>
public static class PostalCodeValidation
{
    /// <summary>The postal code should be valid for the specified country.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="country">
    /// The county the postal code should be valid for.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, PostalCode> ValidFor<TModel>(this IRuleBuilder<TModel, PostalCode> ruleBuilder, Country country)
        => Guard.NotNull(ruleBuilder)
        .ValidFor(_ => country);

    /// <summary>The postal code should be valid for the specified country.</summary>
    /// <typeparam name="TModel">
    /// Type of object being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="country">
    /// The county the postal code should be valid for.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, PostalCode> ValidFor<TModel>(this IRuleBuilder<TModel, PostalCode> ruleBuilder, Func<TModel, Country> country)
        => Guard.NotNull(ruleBuilder)
            .Must((model, postalCode, context) => IsValidFor(postalCode, country(model), context))
            .WithMessage(_ => QowaivValidationFluentMessages.PostalCodeValidForCountry);

    [Impure]
    private static bool IsValidFor<TModel>(PostalCode postalCode, Country country, ValidationContext<TModel> context)
    {
        if (country.IsEmptyOrUnknown()
            || postalCode.IsEmptyOrUnknown()
            || postalCode.IsValid(country))
        {
            return true;
        }
        context.MessageFormatter
            .AppendArgument(nameof(Country), country.DisplayName)
            .AppendArgument("Value", postalCode);

        return false;
    }
}
