namespace FluentValidation;

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
    [FluentSyntax]
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
    /// If true, unknown values are seen.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, TProperty> Required<TModel, TProperty>(this IRuleBuilder<TModel, TProperty> ruleBuilder, bool allowUnknown)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        return allowUnknown
            ? ruleBuilder
                .NotEmpty().WithMessage(QowaivValidationFluentMessages.Required)
            : ruleBuilder
                .NotEmptyOrUnknown().WithMessage(QowaivValidationFluentMessages.Required);
    }
}
