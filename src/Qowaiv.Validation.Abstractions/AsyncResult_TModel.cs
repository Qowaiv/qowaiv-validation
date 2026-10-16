namespace Qowaiv.Validation.Abstractions;

/// <summary>
/// Represents an asynchronous operation that produces a <see cref="Result{TModel}"/> at some time in the future.
/// </summary>
/// <typeparam name="TModel">
/// The model of the result.
/// </typeparam>
public readonly partial struct AsyncResult<TModel>
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly Task<Result<TModel>> Promise;

    internal AsyncResult(Task<Result<TModel>> promise) => Promise = Guard.NotNull(promise);

    /// <inheritdoc cref="Task.IsCanceled" />
    public bool IsCanceled => Promise.IsCanceled;

    /// <inheritdoc cref="Task.IsCompleted" />
    public bool IsCompleted => Promise.IsCompleted;

    /// <summary>Configures an awaiter used to await this <see cref="AsyncResult" />.</summary>
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false.
    /// </param>
    [Pure]
    public ConfiguredTaskAwaitable<Result<TModel>> ConfigureAwait(bool continueOnCapturedContext)
        => Promise.ConfigureAwait(continueOnCapturedContext);

    /// <summary>Gets the awaiter.</summary>
    [Pure]
    public TaskAwaiter<Result<TModel>> GetAwaiter() => Promise.GetAwaiter();

    /// <summary>Implicitly casts from Task to an AsyncResult.</summary>
    public static implicit operator AsyncResult<TModel>(Task<Result<TModel>> promise) => new(promise);

    /// <summary>Invokes the action when <see cref="AsyncResult{TModel}"/> is valid.</summary>
    /// <param name="result">
    /// The result to act on.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    public static AsyncResult<TModel> operator |(AsyncResult<TModel> result, Func<TModel, Result<TModel>> action)
        => result.ActAsync(action);

    /// <summary>Invokes the action when <see cref="AsyncResult{TModel}"/> is valid.</summary>
    /// <param name="result">
    /// The result to act on.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    public static AsyncResult<TModel> operator |(AsyncResult<TModel> result, Func<TModel, AsyncResult<TModel>> action)
        => result.ActAsync(action);

    /// <summary>Invokes the action when <see cref="AsyncResult{TModel}"/> is valid.</summary>
    /// <param name="result">
    /// The result to act on.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    public static AsyncResult<TModel> operator |(AsyncResult<TModel> result, Func<TModel, Result> action)
        => result.ActAsync(action);
}
