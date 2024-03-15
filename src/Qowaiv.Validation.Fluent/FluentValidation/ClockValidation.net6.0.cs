#if NET6_0_OR_GREATER

namespace FluentValidation;

/// <summary>Fluent validation for <see cref="Clock"/>.</summary>
public static partial class ClockValidation
{
    /// <summary>Requires a date to be in the future.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly> InFuture<TModel>(this IRuleBuilder<TModel, DateOnly> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => date > today())
            .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
    }

    /// <summary>Requires a date to be in the future (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly?> InFuture<TModel>(this IRuleBuilder<TModel, DateOnly?> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value > today())
            .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
    }

    /// <summary>Requires a date not to be in the future.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly> NotInFuture<TModel>(this IRuleBuilder<TModel, DateOnly> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => date <= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
    }

    /// <summary>Requires a date not to be in the future (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly?> NotInFuture<TModel>(this IRuleBuilder<TModel, DateOnly?> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value <= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
    }

    /// <summary>Requires a date to be in the past.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly> InPast<TModel>(this IRuleBuilder<TModel, DateOnly> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => date < today())
            .WithMessage(_ => QowaivValidationFluentMessages.InPast);
    }

    /// <summary>Requires a date to be in the past (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly?> InPast<TModel>(this IRuleBuilder<TModel, DateOnly?> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value < today())
            .WithMessage(_ => QowaivValidationFluentMessages.InPast);
    }

    /// <summary>Requires a date not to be in the past.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly> NotInPast<TModel>(this IRuleBuilder<TModel, DateOnly> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => date >= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
    }

    /// <summary>Requires a date not to be in the past (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="today">
    /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateOnly?> NotInPast<TModel>(this IRuleBuilder<TModel, DateOnly?> ruleBuilder, Func<DateOnly>? today = null)
    {
        Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

        today ??= DateOnlyToday;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value >= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
    }

    [Pure]
    private static DateOnly DateOnlyToday() => Clock.Today();
}

#endif
