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

    internal AsyncResult(Task<Result<TModel>> promise) => this.Promise = Guard.NotNull(promise);

    /// <inheritdoc cref="Task.IsCanceled" />
    public bool IsCanceled => Promise.IsCanceled;

    /// <inheritdoc cref="Task.IsCompleted" />
    public bool IsCompleted => Promise.IsCompleted;

    /// <summary>Gets the awaiter.</summary>
    [Pure]
    public AsyncResultAwaiter<TModel> GetAwaiter() => new(Promise);
}
