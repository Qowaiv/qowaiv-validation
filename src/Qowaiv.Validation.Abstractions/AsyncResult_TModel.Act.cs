namespace Qowaiv.Validation.Abstractions;

public readonly partial struct AsyncResult<TModel>
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
    public AsyncResult<TOut> ActAsync<TOut>(
        Func<TModel, Result<TOut>> action,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        var promise = Promise;

        async Task<Result<TOut>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? result.Act(action)
            : Result.WithMessages<TOut>();

        return new(Execute());
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
    public AsyncResult<TOut> ActAsync<TOut>(
        Func<TModel, AsyncResult<TOut>> action,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        var promise = Promise;

        async Task<Result<TOut>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? await result.ActAsync(action, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext)
            : Result.Null<TOut>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync(
        Func<TModel, Result> action,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? result.Act(action)
            : Result.Null<TModel>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync(
        Func<TModel, AsyncResult> action,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? await result.ActAsync(action, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext)
            : Result.Null<TModel>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync<TOut>(
        Func<TModel, Result<TOut>> action,
        Action<TModel, TOut> update,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        Guard.NotNull(update);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
             ? result.Act(action, update)
             : Result.WithMessages<TModel>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync<TOut>(
        Func<TModel, AsyncResult<TOut>> action,
        Action<TModel, TOut> update,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        Guard.NotNull(update);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
             ? await result.ActAsync(action, update, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext)
            : Result.WithMessages<TModel>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync<TOut>(
        Func<TModel, Result<TOut>> action,
        Func<TModel, TOut, TModel> update,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        Guard.NotNull(update);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? result.Act(action, update)
            : Result.WithMessages<TModel>();

        return new(Execute());
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
    public AsyncResult<TModel> ActAsync<TOut>(
        Func<TModel, AsyncResult<TOut>> action,
        Func<TModel, TOut, TModel> update,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(action);
        Guard.NotNull(update);
        var promise = Promise;

        async Task<Result<TModel>> Execute()
            => await promise.ConfigureAwait(continueOnCapturedContext) is { } result
            ? await result.ActAsync(action, update, continueOnCapturedContext).ConfigureAwait(continueOnCapturedContext)
            : Result.WithMessages<TModel>();

        return new(Execute());
    }
}
