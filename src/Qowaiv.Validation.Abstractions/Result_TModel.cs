using Qowaiv.Validation.Abstractions.Internals;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Represents a result of a validation, executed command, etcetera.</summary>
    public sealed class Result<TModel> : Result
    {
        /// <summary>Initializes a new instance of the <see cref="Result{T}"/> class.</summary>
        /// <param name="data">
        /// The data related to the result.
        /// </param>
        /// <param name="messages">
        /// The messages related to the result.
        /// </param>
        internal Result(TModel data, FixedMessages messages) : base(messages)
            => _value = IsValid ? data : default;

        /// <summary>Gets the value related to result.</summary>
        public TModel Value => IsValid
            ? _value
            : throw InvalidModelException.For<TModel>(Errors);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TModel _value;

        /// <summary>Implicitly casts a model to the <see cref="Result"/>.</summary>
        public static implicit operator Result<TModel>(TModel model) => For(model);

        /// <summary>Throws an <see cref="InvalidModelException"/> if the result is not valid.</summary>
        public void ThrowIfInvalid()
        {
            if (!IsValid)
            {
                throw InvalidModelException.For<TModel>(Errors);
            }
        }

        /// <summary>Gets the <see cref="Result{TModel}"/> as a <see cref="Task{TResult}"/>.</summary>
        public Task<Result<TModel>> AsTask() => Task.FromResult(this);

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public Result<TOut> Act<TOut>(Func<TModel, Result<TOut>> action)
        {
            Guard.NotNull(action, nameof(action));

            if (IsNullValueOrInvalid(this))
            {
                return WithMessages<TOut>(Messages);
            }

            var messages = (FixedMessages)Messages;
            var outcome = action(Value);
            return For(outcome.IsValid
                ? outcome.Value
                : default,
                messages.AddRange(outcome.Messages));
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public Result<TModel> Act(Func<TModel, Result> action)
        {
            Guard.NotNull(action, nameof(action));

            if (IsNullValueOrInvalid(this))
            {
                return WithMessages<TModel>(Messages);
            }
            else
            {
                var messages = (FixedMessages)Messages;
                var outcome = action(Value);
                return For(Value, messages.AddRange(outcome.Messages));
            }
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public Result<TModel> Act<TOut>(Func<TModel, Result<TOut>> action, Action<TModel, TOut> update)
            => Act(action, (model, result) =>
            {
                Guard.NotNull(update, nameof(update));
                if (model is { }) { update.Invoke(model, result); }
                return model;
            });

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public Result<TModel> Act<TOut>(Func<TModel, Result<TOut>> action, Func<TModel, TOut, TModel> update)
        {
            Guard.NotNull(update, nameof(update));

            var resolved = Act(action);
            return resolved.IsValid
                ? For(update(_value, resolved.Value), resolved.Messages)
                : For(_value, resolved.Messages);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public async Task<Result<TOut>> ActAsync<TOut>(Func<TModel, Task<Result<TOut>>> action)
        {
            _ = Guard.NotNull(action, nameof(action));

            if (IsNullValueOrInvalid(this))
            {
                return WithMessages<TOut>(Messages);
            }

            var messages = (FixedMessages)Messages;
            var outcome = await action(Value).ContinueOnAnyContext();

            return For(outcome.IsValid
                ? outcome.Value
                : default,
                messages.AddRange(outcome.Messages));
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public async Task<Result<TModel>> ActAsync(Func<TModel, Task<Result>> action)
        {
            _ = Guard.NotNull(action, nameof(action));

            if (IsNullValueOrInvalid(this))
            {
                return WithMessages<TModel>(Messages);
            }

            var messages = (FixedMessages)Messages;
            var outcome = await action(Value).ContinueOnAnyContext();
            return For(Value, messages.AddRange(outcome.Messages));
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Task<Result<TOut>>> action, 
            Action<TModel, TOut> update)
            => ActAsync(action, (model, result) =>
            {
                Guard.NotNull(update, nameof(update));
                if (model is { }) { update.Invoke(model, result); }
                return model;
            });

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public async Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Task<Result<TOut>>> action,
            Func<TModel, TOut, TModel> update)
        {
            Guard.NotNull(update, nameof(update));

            var resolved = await ActAsync(action).ContinueOnAnyContext();
            return resolved.IsValid
                ? For(update(_value, resolved.Value), resolved.Messages)
                : For(_value, resolved.Messages);
        }

        /// <summary>Explicitly casts the <see cref="Result"/> to the type of the related model.</summary>
        public static explicit operator TModel(Result<TModel> result) => result == null ? default : result.Value;

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="result">
        /// The result to act on.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static Result<TModel> operator |(Result<TModel> result, Func<TModel, Result<TModel>> action)
            => Guard.NotNull(result, nameof(result)).Act(action);

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="result">
        /// The result to act on.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static Result<TModel> operator |(Result<TModel> result, Func<TModel, Result> action)
            => Guard.NotNull(result, nameof(result)).Act(action);

        internal static bool IsNullValueOrInvalid<T>(Result<T> result)
           => !result.IsValid || ReferenceEquals(null, result._value);
    }
}
