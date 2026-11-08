namespace FluentValidation;

/// <summary>Fluent validation for <see cref="Clock"/>.</summary>
public static partial class ClockValidation
{
    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    extension<TModel>(IRuleBuilder<TModel, DateOnly> ruleBuilder)
    {
        /// <summary>Requires a date to be in the future.</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly> InFuture(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => date > today())
                .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
        }

        /// <summary>Requires a date not to be in the future.</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly> NotInFuture(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => date <= today())
                .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
        }

        /// <summary>Requires a date to be in the past.</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly> InPast(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => date < today())
                .WithMessage(_ => QowaivValidationFluentMessages.InPast);
        }

        /// <summary>Requires a date not to be in the past.</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly> NotInPast(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => date >= today())
                .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
        }
    }

    /// <typeparam name="TModel">
    /// Type of the model being validated.
    /// </typeparam>
    extension<TModel>(IRuleBuilder<TModel, DateOnly?> ruleBuilder)
    {
        /// <summary>Requires a date to be in the future (if set).</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly?> InFuture(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value > today())
                .WithMessage(_ => QowaivValidationFluentMessages.InFuture);
        }

        /// <summary>Requires a date not to be in the future (if set).</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly?> NotInFuture(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value <= today())
                .WithMessage(_ => QowaivValidationFluentMessages.NotInFuture);
        }

        /// <summary>Requires a date to be in the past (if set).</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly?> InPast(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value < today())
                .WithMessage(_ => QowaivValidationFluentMessages.InPast);
        }

        /// <summary>Requires a date not to be in the past (if set).</summary>
        /// <param name="today">
        /// An optional function providing today, or if not specified, <see cref="Clock.Today()"/>.
        /// </param>
        [FluentSyntax]
        public IRuleBuilderOptions<TModel, DateOnly?> NotInPast(Func<DateOnly>? today = null)
        {
            Guard.NotNull(ruleBuilder);

            today ??= DateOnlyToday;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value >= today())
                .WithMessage(_ => QowaivValidationFluentMessages.NotInPast);
        }
    }

    [Pure]
    private static DateOnly DateOnlyToday() => Clock.Today();
}
