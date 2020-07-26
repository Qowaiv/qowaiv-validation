using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions
{
    /// <summary>Extensions on asynchronous <see cref="Result{TModel}"/>.</summary>
    public static class ResultActExtensions
    {
        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static async Task<Result<TOut>> ActAsync<TModel, TOut>(this Task<Result<TModel>> promise, Func<TModel, Result<TOut>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);

            if (result is null)
            {
                return Result.WithMessages<TOut>();
            }
            if (!result.IsValid || ReferenceEquals(result.Value, null))
            {
                return Result.WithMessages<TOut>(result.Messages);
            }
            var messages = result.Messages.ToList();
            var outcome = action(result.Value);
            messages.AddRange(outcome.Messages);
            return Result.For(outcome.IsValid ? outcome.Value : default, messages);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static async Task<Result<TOut>> ActAsync<TModel, TOut>(this Task<Result<TModel>> promise, Func<TModel, Task<Result<TOut>>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);

            if (result is null)
            {
                return Result.WithMessages<TOut>();
            }
            if (!result.IsValid || ReferenceEquals(result.Value, null))
            {
                return Result.WithMessages<TOut>(result.Messages);
            }

            var messages = result.Messages.ToList();
            var outcome = await action(result.Value).ConfigureAwait(false);
            messages.AddRange(outcome.Messages);
            return Result.For(outcome.IsValid ? outcome.Value : default, messages);
        }


        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel>(this Task<Result<TModel>> promise, Func<TModel, Result> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);

            if (result is null || ReferenceEquals(null, result.Value))
            {
                return Result.For<TModel>(default);
            }
            else if (!result.IsValid)
            {
                return result;
            }
            else
            {
                var messages = result.Messages.ToList();

                var act = action(result.Value);
                messages.AddRange(act.Messages);
                return Result.For(result.Value, messages);
            }
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel>(this Task<Result<TModel>> promise, Func<TModel, Task<Result>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);
            
            if (result is null || ReferenceEquals(null, result.Value))
            {
                return Result.For<TModel>(default);
            }
            else if (!result.IsValid)
            {
                return result;
            }
            else
            {
                var messages = result.Messages.ToList();
                var act = await action(result.Value).ConfigureAwait(false);
                messages.AddRange(act.Messages);
                return Result.For(result.Value, messages);
            }
        }
    }
}
