namespace Qowaiv.Validation.Abstractions;

public readonly struct AsyncResultAwaiter<TModel> : ICriticalNotifyCompletion
{
    private readonly TaskAwaiter<Result<TModel>> Awaiter;

    internal AsyncResultAwaiter(Task<Result<TModel>> promise)
        => Awaiter = Guard.NotNull(promise).GetAwaiter();

    /// <inheritdoc cref="TaskAwaiter{TResult}.IsCompleted" />
    public bool IsCompleted => Awaiter.IsCompleted;

    /// <inheritdoc cref="TaskAwaiter{TResult}.GetResult" />
    [Pure]
#if !NETSTANDARD2_0
    [StackTraceHidden]
#endif
    public Result<TModel> GetResult() => Awaiter.GetResult();

    /// <inheritdoc />
    public void OnCompleted(Action continuation)
        => Awaiter.OnCompleted(continuation);

    /// <inheritdoc />
    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        => ((ICriticalNotifyCompletion)Awaiter).UnsafeOnCompleted(continuation);
}
