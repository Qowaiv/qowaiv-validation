using System;
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

            var result = await promise.ContinueOnAnyContext();

            return result is null
                ? Result.WithMessages<TOut>()
                : result.Act(action);
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

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.For<TOut>(default)
                : await result.ActAsync(action).ContinueOnAnyContext();
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

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.For<TModel>(default)
                : result.Act(action);
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

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.For<TModel>(default)
                : await result.ActAsync(action).ContinueOnAnyContext();
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
            this Task<Result<TModel>> promise,
            Func<TModel, Result<TOut>> action,
            Action<TModel, TOut> update)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.WithMessages<TModel>()
                : result.Act(action, update);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
            this Task<Result<TModel>> promise,
            Func<TModel, Task<Result<TOut>>> action,
            Action<TModel, TOut> update)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.WithMessages<TModel>()
                : await result.ActAsync(action, update).ContinueOnAnyContext();
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
            this Task<Result<TModel>> promise,
            Func<TModel, Result<TOut>> action,
            Func<TModel, TOut, TModel> update)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.WithMessages<TModel>()
                : result.Act(action, update);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="promise">
        /// The promise of the result.
        /// </param>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of input result.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
            this Task<Result<TModel>> promise,
            Func<TModel, Task<Result<TOut>>> action,
            Func<TModel, TOut, TModel> update)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ContinueOnAnyContext();
            return result is null
                ? Result.WithMessages<TModel>()
                : await result.ActAsync(action, update).ContinueOnAnyContext();
        }
    }
}
