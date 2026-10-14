namespace Qowaiv.Validation.Abstractions;

/// <summary>Extensions on asynchronous <see cref="Result{TModel}"/>.</summary>
public static class ResultActExtensions
{
    extension<TModel>(Task<Result<TModel>> promise)
    {
        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TOut>> ActAsync<TOut>(
            Func<TModel, Result<TOut>> action,
            bool continueOnCapturedContext = false)
        {
            _ = Guard.NotNull(promise);
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);

            return result is null
                ? Result.WithMessages<TOut>()
                : result.Act(action);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TOut>> ActAsync<TOut>(
            Func<TModel, Task<Result<TOut>>> action,
            bool continueOnCapturedContext = false)
        {
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.Null<TOut>()
                : await result.ActAsync(action).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync(
            Func<TModel, Result> action,
            bool continueOnCapturedContext = false)
        {
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.Null<TModel>()
                : result.Act(action);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <returns>
        /// A result with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync(
            Func<TModel, Task<Result>> action,
            bool continueOnCapturedContext = false)
        {
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.Null<TModel>()
                : await result.ActAsync(action).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Result<TOut>> action,
            Action<TModel, TOut> update,
            bool continueOnCapturedContext = false)
        {
            _ = Guard.NotNull(promise);
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.WithMessages<TModel>()
                : result.Act(action, update);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Task<Result<TOut>>> action,
            Action<TModel, TOut> update,
            bool continueOnCapturedContext = false)
        {
            _ = Guard.NotNull(promise);
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.WithMessages<TModel>()
                : await result.ActAsync(action, update).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Result<TOut>> action,
            Func<TModel, TOut, TModel> update,
            bool continueOnCapturedContext = false)
        {
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.WithMessages<TModel>()
                : result.Act(action, update);
        }

        /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
        /// <param name="action">
        /// The action to invoke.
        /// </param>
        /// <param name="update">
        /// The update to apply on a successfully invoked action.
        /// </param>
        /// <param name="continueOnCapturedContext">
        /// true to attempt to marshal the continuation back to the original context captured;
        /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
        /// </param>
        /// <typeparam name="TOut">
        /// The type of the new result value.
        /// </typeparam>
        /// <returns>
        /// The updated model with the merged messages.
        /// </returns>
        [Impure]
        public async Task<Result<TModel>> ActAsync<TOut>(
            Func<TModel, Task<Result<TOut>>> action,
            Func<TModel, TOut, TModel> update,
            bool continueOnCapturedContext = false)
        {
            _ = Guard.NotNull(promise);
            Guard.NotNull(action);

            var result = await promise.ConfigureAwait(continueOnCapturedContext);
            return result is null
                ? Result.WithMessages<TModel>()
                : await result.ActAsync(action, update).ConfigureAwait(continueOnCapturedContext);
        }
    }
}
