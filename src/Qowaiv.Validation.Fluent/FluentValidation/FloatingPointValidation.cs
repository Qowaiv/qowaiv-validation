namespace FluentValidation;

/// <summary>FLuent validation for <see cref="double"/> and <see cref="float"/>.</summary>
public static class FloatingPointValidation
{
    /// <summary>The floating point should be a finite number.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, double> IsFinite<TModel>(this IRuleBuilder<TModel, double> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => number.IsFinite())
        .WithMessage(_ => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, double?> IsFinite<TModel>(this IRuleBuilder<TModel, double?> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => !number.HasValue || number.Value.IsFinite())
        .WithMessage(_ => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, float> IsFinite<TModel>(this IRuleBuilder<TModel, float> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => number.IsFinite())
        .WithMessage(_ => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, float?> IsFinite<TModel>(this IRuleBuilder<TModel, float?> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => !number.HasValue || number.Value.IsFinite())
        .WithMessage(_ => QowaivValidationFluentMessages.IsFinite);
}
