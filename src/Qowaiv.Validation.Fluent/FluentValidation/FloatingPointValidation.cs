namespace FluentValidation;

/// <summary>FLuent validation for <see cref="double"/> and <see cref="float"/>.</summary>
public static class FloatingPointValidation
{
    /// <summary>The floating point should be a finite number..</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, double> IsFinite<TModel>(this IRuleBuilder<TModel, double> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(IsFinite)
        .WithMessage(m => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number..</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, double?> IsFinite<TModel>(this IRuleBuilder<TModel, double?> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => !number.HasValue || IsFinite(number.Value))
        .WithMessage(m => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number..</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, float> IsFinite<TModel>(this IRuleBuilder<TModel, float> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(IsFinite)
        .WithMessage(m => QowaivValidationFluentMessages.IsFinite);

    /// <summary>The floating point should be a finite number..</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, float?> IsFinite<TModel>(this IRuleBuilder<TModel, float?> ruleBuilder)
        => Guard.NotNull(ruleBuilder, nameof(ruleBuilder))
        .Must(number => !number.HasValue || IsFinite(number.Value))
        .WithMessage(m => QowaivValidationFluentMessages.IsFinite);

    [Pure]
    private static bool IsFinite(double number)
#if NETSTANDARD
        => !double.IsNaN(number) && !double.IsInfinity(number);
#else
        => double.IsFinite(number);
#endif

    [Pure]
    private static bool IsFinite(float number)
#if NETSTANDARD
        => !float.IsNaN(number) && !float.IsInfinity(number);
#else
        => float.IsFinite(number);
#endif
}
