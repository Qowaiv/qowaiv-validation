namespace Qowaiv.Validation.Abstractions;

/// <summary>Provides an awaiter for awaiting a <see cref="AsyncResult{TResult}"/>.</summary>
public readonly struct AsyncResultAwaiter<TModel> : ICriticalNotifyCompletion
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly TaskAwaiter<Result<TModel>> Awaiter;

    internal AsyncResultAwaiter(Task<Result<TModel>> promise)
        => Awaiter = Guard.NotNull(promise).GetAwaiter();

    /// <inheritdoc cref="TaskAwaiter{TResult}.IsCompleted" />
    public bool IsCompleted => Awaiter.IsCompleted;

    /// <inheritdoc cref="TaskAwaiter{TResult}.GetResult" />
    [Pure]
    [StackTraceHidden]
    public Result<TModel> GetResult() => Awaiter.GetResult();

    /// <inheritdoc />
    public void OnCompleted(Action continuation)
        => Awaiter.OnCompleted(continuation);

    /// <inheritdoc />
    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        => ((ICriticalNotifyCompletion)Awaiter).UnsafeOnCompleted(continuation);
}
