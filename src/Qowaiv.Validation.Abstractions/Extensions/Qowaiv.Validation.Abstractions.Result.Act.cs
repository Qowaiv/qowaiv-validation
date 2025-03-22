
namespace Qowaiv.Validation.Abstractions;
#pragma warning restore IDE0130 // Namespace does not match folder structure

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
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
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
    [Impure]
    public static async Task<Result<TOut>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
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
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
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
    [Impure]
    public static async Task<Result<TOut>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
        Func<TModel, Task<Result<TOut>>> action,
        bool continueOnCapturedContext = false)
    {
        _ = Guard.NotNull(promise);
        Guard.NotNull(action);

        var result = await promise.ConfigureAwait(continueOnCapturedContext);
        return result is null
            ? Result.Null<TOut>()
            : await result.ActAsync(action).ConfigureAwait(continueOnCapturedContext);
    }

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
    /// </param>
    /// <typeparam name="TModel">
    /// The type of input result.
    /// </typeparam>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel>(
        this Task<Result<TModel>> promise,
        Func<TModel, Result> action,
        bool continueOnCapturedContext = false)
    {
        _ = Guard.NotNull(promise);
        Guard.NotNull(action);

        var result = await promise.ConfigureAwait(continueOnCapturedContext);
        return result is null
            ? Result.Null<TModel>()
            : result.Act(action);
    }

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
    /// </param>
    /// <typeparam name="TModel">
    /// The type of input result.
    /// </typeparam>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel>(
        this Task<Result<TModel>> promise,
        Func<TModel, Task<Result>> action,
        bool continueOnCapturedContext = false)
    {
        _ = Guard.NotNull(promise);
        Guard.NotNull(action);

        var result = await promise.ConfigureAwait(continueOnCapturedContext);
        return result is null
            ? Result.Null<TModel>()
            : await result.ActAsync(action).ConfigureAwait(continueOnCapturedContext);
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
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false. See <see cref="Task.ConfigureAwait(bool)"/>.
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
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
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
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
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
    /// <typeparam name="TModel">
    /// The type of input result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
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
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
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
    /// <typeparam name="TModel">
    /// The type of input result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
        Func<TModel, Result<TOut>> action,
        Func<TModel, TOut, TModel> update,
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
    /// <param name="promise">
    /// The promise of the result.
    /// </param>
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
    /// <typeparam name="TModel">
    /// The type of input result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public static async Task<Result<TModel>> ActAsync<TModel, TOut>(
        this Task<Result<TModel>> promise,
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
