#pragma warning disable QW0010 // Use System.DateOnly instead of Qowaiv.Date: We support netstandard 2.0.

namespace FluentValidation;

/// <summary>Fluent validation for <see cref="Clock"/>.</summary>
public static partial class ClockValidation
{
    /// <summary>Requires a date time to be in the future.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime> InFuture<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => date > utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
    }

    /// <summary>Requires a date time to be in the future (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime?> InFuture<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value > utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
    }

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
    public static IRuleBuilderOptions<TModel, Date> InFuture<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

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
    public static IRuleBuilderOptions<TModel, Date?> InFuture<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value > today())
            .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
    }

    /// <summary>Requires a date time not to be in the future.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime> NotInFuture<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => date <= utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
    }

    /// <summary>Requires a date time not to be in the future (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime?> NotInFuture<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value <= utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
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
    public static IRuleBuilderOptions<TModel, Date> NotInFuture<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

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
    public static IRuleBuilderOptions<TModel, Date?> NotInFuture<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value <= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
    }

    /// <summary>Requires a date time to be in the past.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime> InPast<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => date < utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.InPast);
    }

    /// <summary>Requires a date time to be in the past (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime?> InPast<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value < utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.InPast);
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
    public static IRuleBuilderOptions<TModel, Date> InPast<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

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
    public static IRuleBuilderOptions<TModel, Date?> InPast<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value < today())
            .WithMessage(_ => QowaivValidationFluentMessages.InPast);
    }

    /// <summary>Requires a date time not to be in the past.</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime> NotInPast<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => date >= utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
    }

    /// <summary>Requires a date time not to be in the past (if set).</summary>
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    /// <param name="ruleBuilder">
    /// The rule builder on which the validator should be defined.
    /// </param>
    /// <param name="utcNow">
    /// An optional function providing now, or if not specified, <see cref="Clock.UtcNow()"/>.
    /// </param>
    [FluentSyntax]
    public static IRuleBuilderOptions<TModel, DateTime?> NotInPast<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime>? utcNow = null)
    {
        Guard.NotNull(ruleBuilder);

        utcNow ??= Clock.UtcNow;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value >= utcNow())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
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
    public static IRuleBuilderOptions<TModel, Date> NotInPast<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

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
    public static IRuleBuilderOptions<TModel, Date?> NotInPast<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date>? today = null)
    {
        Guard.NotNull(ruleBuilder);

        today ??= Clock.Today;

        return ruleBuilder
            .Must(date => !date.HasValue || date.Value >= today())
            .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
    }
}
