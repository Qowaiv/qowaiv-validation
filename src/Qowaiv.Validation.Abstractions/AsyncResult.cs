namespace Qowaiv.Validation.Abstractions;

/// <summary>
/// Represents an asynchronous operation that produces a <see cref="Result"/> at some time in the future.
/// </summary>
public readonly struct AsyncResult
{
    internal AsyncResult(Task<Result> promise) => Promise = promise;

    /// <summary>Gets the underlying promise, guarding against the uninitialized (default) struct.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Task<Result> Promise => field
        ?? throw new InvalidOperationException($"The {nameof(AsyncResult)} is not initialized; use {nameof(Result)}.{nameof(Result.Async)}() or an implicit cast from {nameof(Task)} instead of default.");

    /// <inheritdoc cref="Task.IsCanceled" />
    public bool IsCanceled => Promise.IsCanceled;

    /// <inheritdoc cref="Task.IsCompleted" />
    public bool IsCompleted => Promise.IsCompleted;

    /// <<summary>Represents the async result as task.</summary>
    [Pure]
    public Task<Result> AsTask() => Promise;

    /// <summary>Configures an awaiter used to await this <see cref="AsyncResult" />.</summary>
    /// <param name="continueOnCapturedContext">
    /// true to attempt to marshal the continuation back to the original context captured;
    /// otherwise, false.
    /// </param>
    [Pure]
    public ConfiguredTaskAwaitable<Result> ConfigureAwait(bool continueOnCapturedContext)
        => Promise.ConfigureAwait(continueOnCapturedContext);

    /// <summary>Gets the awaiter.</summary>
    [Pure]
    public TaskAwaiter<Result> GetAwaiter() => Promise.GetAwaiter();

    /// <summary>Implicitly casts from Task to an AsyncResult.</summary>
    public static implicit operator AsyncResult(Task<Result> promise) => new(Guard.NotNull(promise));
}
