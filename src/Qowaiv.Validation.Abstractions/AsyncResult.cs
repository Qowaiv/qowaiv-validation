using System;
using System.Collections.Generic;
using System.Text;

namespace Qowaiv.Validation.Abstractions;

public readonly partial struct AsyncResult<TModel>
{
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
