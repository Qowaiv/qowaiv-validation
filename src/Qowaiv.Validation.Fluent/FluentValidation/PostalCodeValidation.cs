
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
    public static PostalCodeRuleBuilderOptions<TModel> ValidFor<TModel>(this IRuleBuilder<TModel, PostalCode> ruleBuilder, Country country)
        => new(ruleBuilder, _ => country);

    /// <summary>The postal code should be valid for the specified country.</summary>
    /// <typeparam name="TModel">
    /// Type of model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="country">
    /// The county the postal code should be valid for.
    /// </param>
    [FluentSyntax]
    public static PostalCodeRuleBuilderOptions<TModel> ValidFor<TModel>(this IRuleBuilder<TModel, PostalCode> ruleBuilder, Func<TModel, Country> country)
        => new(ruleBuilder, country);

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

public sealed class PostalCodeRuleBuilderOptions<TModel> : IRuleBuilderOptions<TModel, PostalCode>
{
    private readonly IRuleBuilderOptions<TModel, PostalCode> RuleBuilderOptions;
    private readonly Func<TModel, Country> Country;

    internal PostalCodeRuleBuilderOptions(IRuleBuilder<TModel, PostalCode> ruleBuilderOptions, Func<TModel, Country> country)
    {
        Country = Guard.NotNull(country);
        RuleBuilderOptions = Guard.NotNull(ruleBuilderOptions)
            .Must((model, postalCode, context) => IsValidFor(postalCode, country(model), context))
            .WithMessage(_ => QowaivValidationFluentMessages.PostalCodeValidForCountry);
    }

    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> Required() => Required(false);

    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> Required(bool allowUnknown)
        => RuleBuilderOptions.Required(allowUnknown).When(m => PostalCodeCountryInfo.GetInstance(Country(m)).HasPostalCode);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> DependentRules(Action action) 
        => RuleBuilderOptions.DependentRules(action);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> SetAsyncValidator(IAsyncPropertyValidator<TModel, PostalCode> validator)
        => RuleBuilderOptions.SetAsyncValidator(validator);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> SetValidator(IPropertyValidator<TModel, PostalCode> validator)
        => RuleBuilderOptions.SetValidator(validator);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> SetValidator(IValidator<PostalCode> validator, params string[] ruleSets)
        => RuleBuilderOptions.SetValidator(validator, ruleSets);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> SetValidator<TValidator>(Func<TModel, TValidator> validatorProvider, params string[] ruleSets) where TValidator : IValidator<PostalCode>
        => RuleBuilderOptions.SetValidator(validatorProvider, ruleSets);

    /// <inheritdoc />
    [Pure]
    public IRuleBuilderOptions<TModel, PostalCode> SetValidator<TValidator>(Func<TModel, PostalCode, TValidator> validatorProvider, params string[] ruleSets) where TValidator : IValidator<PostalCode>
        => RuleBuilderOptions.SetValidator(validatorProvider, ruleSets);


    [Impure]
    private static bool IsValidFor(PostalCode postalCode, Country country, ValidationContext<TModel> context)
    {
        if (country.IsEmptyOrUnknown()
            || postalCode.IsEmptyOrUnknown()
            || postalCode.IsValid(country))
        {
            return true;
        }
        context.MessageFormatter
            .AppendArgument(nameof(Country), country.DisplayName)
            .AppendArgument("ComparisonValue", postalCode);

        return false;
    }
}
