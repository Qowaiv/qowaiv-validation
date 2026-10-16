namespace Qowaiv.Validation.Abstractions;

/// <summary>
/// Represents an asynchronous operation that produces a <see cref="Result"/> at some time in the future.
/// </summary>
public readonly struct AsyncResult
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly Task<Result> Promise;

    internal AsyncResult(Task<Result> promise) => Promise = Guard.NotNull(promise);

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
    public ConfiguredTaskAwaitable<Result> ConfigureAwait(bool continueOnCapturedContext)
        => Promise.ConfigureAwait(continueOnCapturedContext);

    /// <summary>Gets the awaiter.</summary>
    [Pure]
    public AsyncResultAwaiter GetAwaiter() => new(Promise);

    /// <summary>Implictly casts from Task to an AsyncResult.</summary>
    public static implicit operator AsyncResult(Task<Result> promise) => new(promise);
}
