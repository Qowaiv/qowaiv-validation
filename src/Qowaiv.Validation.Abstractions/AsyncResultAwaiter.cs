namespace Qowaiv.Validation.Abstractions;

/// <summary>Provides an awaiter for awaiting a <see cref="AsyncResult"/>.</summary>
public readonly struct AsyncResultAwaiter : ICriticalNotifyCompletion
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly TaskAwaiter<Result> Awaiter;

    internal AsyncResultAwaiter(Task<Result> promise)
        => Awaiter = Guard.NotNull(promise).GetAwaiter();

    /// <inheritdoc cref="TaskAwaiter.IsCompleted" />
    public bool IsCompleted => Awaiter.IsCompleted;

    /// <inheritdoc cref="TaskAwaiter{TResult}.GetResult" />
    [Pure]
    [StackTraceHidden]
    public Result GetResult() => Awaiter.GetResult();

    /// <inheritdoc />
    public void OnCompleted(Action continuation)
        => Awaiter.OnCompleted(continuation);

    /// <inheritdoc />
    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        => ((ICriticalNotifyCompletion)Awaiter).UnsafeOnCompleted(continuation);
}
