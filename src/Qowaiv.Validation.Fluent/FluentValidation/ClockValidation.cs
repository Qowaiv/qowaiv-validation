using Qowaiv;
using Qowaiv.Validation.Fluent;
using System;

namespace FluentValidation
{
    /// <summary>Fluent validation for <see cref="Clock"/>.</summary>
    public static class ClockValidation
    {
        /// <summary>Requires a date time to be in the future.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime> InFuture<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => date > now())
                .WithMessage(m => QowaivValidationFluentMessages.InFuture);
        }

        /// <summary>Requires a date time to be in the future (if set).</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime?> InFuture<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value > now())
                .WithMessage(m => QowaivValidationFluentMessages.InFuture);
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
        public static IRuleBuilderOptions<TModel, Date> InFuture<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => date > today())
                .WithMessage(m => QowaivValidationFluentMessages.InFuture);
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
        public static IRuleBuilderOptions<TModel, Date?> InFuture<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value > today())
                .WithMessage(m => QowaivValidationFluentMessages.InFuture);
        }


        /// <summary>Requires a date time not to be in the future.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime> NotInFuture<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => date <= now())
                .WithMessage(m => QowaivValidationFluentMessages.NotInFuture);
        }

        /// <summary>Requires a date time not to be in the future (if set).</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime?> NotInFuture<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value <= now())
                .WithMessage(m => QowaivValidationFluentMessages.NotInFuture);
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
        public static IRuleBuilderOptions<TModel, Date> NotInFuture<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => date <= today())
                .WithMessage(m => QowaivValidationFluentMessages.NotInFuture);
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
        public static IRuleBuilderOptions<TModel, Date?> NotInFuture<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value <= today())
                .WithMessage(m => QowaivValidationFluentMessages.NotInFuture);
        }


        /// <summary>Requires a date time to be in the past.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime> InPast<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => date < now())
                .WithMessage(m => QowaivValidationFluentMessages.InPast);
        }

        /// <summary>Requires a date time to be in the past (if set).</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime?> InPast<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value < now())
                .WithMessage(m => QowaivValidationFluentMessages.InPast);
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
        public static IRuleBuilderOptions<TModel, Date> InPast<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => date < today())
                .WithMessage(m => QowaivValidationFluentMessages.InPast);
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
        public static IRuleBuilderOptions<TModel, Date?> InPast<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value < today())
                .WithMessage(m => QowaivValidationFluentMessages.InPast);
        }


        /// <summary>Requires a date time not to be in the past.</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime> NotInPast<TModel>(this IRuleBuilder<TModel, DateTime> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => date >= now())
                .WithMessage(m => QowaivValidationFluentMessages.NotInPast);
        }

        /// <summary>Requires a date time not to be in the past (if set).</summary>
        /// <typeparam name="TModel">
        /// Type of the model being validated.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// The rule builder on which the validator should be defined.
        /// </param>
        /// <param name="now">
        /// An optional function providing now, or if not specified, <see cref="Clock.Now()"/>.
        /// </param>
        public static IRuleBuilderOptions<TModel, DateTime?> NotInPast<TModel>(this IRuleBuilder<TModel, DateTime?> ruleBuilder, Func<DateTime> now = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            now ??= () => Clock.Now();

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value >= now())
                .WithMessage(m => QowaivValidationFluentMessages.NotInPast);
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
        public static IRuleBuilderOptions<TModel, Date> NotInPast<TModel>(this IRuleBuilder<TModel, Date> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => date >= today())
                .WithMessage(m => QowaivValidationFluentMessages.NotInPast);
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
        public static IRuleBuilderOptions<TModel, Date?> NotInPast<TModel>(this IRuleBuilder<TModel, Date?> ruleBuilder, Func<Date> today = null)
        {
            Guard.NotNull(ruleBuilder, nameof(ruleBuilder));

            today ??= Clock.Today;

            return ruleBuilder
                .Must(date => !date.HasValue || date.Value >= today())
                .WithMessage(m => QowaivValidationFluentMessages.NotInPast);
        }
    }
}
