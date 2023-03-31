namespace Qowaiv.Validation.Abstractions;

/// <summary>Represents a result of a validation, executed command, etcetera.</summary>
public sealed class Result<TModel> : Result
{
    /// <summary>Initializes a new instance of the <see cref="Result{T}"/> class.</summary>
    internal Result(FixedMessages messages) : base(messages) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="Result{T}"/> class.</summary>
    /// <param name="value">
    /// The value related to the result.
    /// </param>
    /// <param name="messages">
    /// The messages related to the result.
    /// </param>
    internal Result(TModel? value, FixedMessages messages) : base(messages)
        => _value = IsValid ? value : default;

    /// <summary>Gets the value related to result.</summary>
    /// <remarks>
    /// Although the value can be null (<see cref="Result.Null{T}(IEnumerable{IValidationMessage})"/>
    /// in normal flows however, the value is not null when valid.
    /// </remarks>
    [Pure]
    public TModel Value => IsValid
        ? _value!
        : throw InvalidModelException.For<TModel>(Errors);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TModel? _value;

    /// <summary>Implicitly casts a model to the <see cref="Result"/>.</summary>
    [Pure]
    public static implicit operator Result<TModel>(TModel model) => For(model);

    /// <summary>Throws an <see cref="InvalidModelException"/> if the result is not valid.</summary>
    public void ThrowIfInvalid()
    {
        if (!IsValid)
        {
            throw InvalidModelException.For<TModel>(Errors);
        }
    }

    /// <summary>Casts the result of <typeparamref name="TModel"/> to a result of <typeparamref name="TOut"/>.</summary>
    /// <typeparam name="TOut">
    /// The new type of the value.
    /// </typeparam>
    /// <exception cref="InvalidCastException">
    /// if <typeparamref name="TOut"/> is not a subtype of <typeparamref name="TModel"/>.
    /// </exception>
    [Pure]
    public Result<TOut> Cast<TOut>()
    {
        return new(_value is null ? default : Cast(), (FixedMessages)Messages);

        TOut Cast()
            => _value is TOut cast
            ? cast
            : throw new InvalidCastException($"Unable to cast object of type 'Result<{typeof(TModel)}>' to type 'Result<{typeof(TOut)}>'.");
    }

    /// <summary>Gets the <see cref="Result{TModel}"/> as a <see cref="Task{TResult}"/>.</summary>
    [Pure]
    public Task<Result<TModel>> AsTask() => Task.FromResult(this);

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    [Impure]
    public Result<TOut> Act<TOut>(Func<TModel, Result<TOut>> action)
    {
        Guard.NotNull(action, nameof(action));

        if (IsValid)
        {
            var outcome = action(Value);
            var value = outcome.IsValid ? outcome.Value : default;
            return new(value, ((FixedMessages)Messages).AddRange(outcome.Messages));
        }
        else return WithMessages<TOut>(Messages);
    }

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    [Impure]
    public Result<TModel> Act(Func<TModel, Result> action)
    {
        Guard.NotNull(action, nameof(action));

        if (IsValid)
        {
            var outcome = action(Value);
            return For(Value, ((FixedMessages)Messages).AddRange(outcome.Messages).Add(new ActFailed(outcome.IsValid)));
        }
        else return WithMessages<TModel>(Messages);
    }

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="update">
    /// The update to apply on a successfully invoked action.
    /// </param>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public Result<TModel> Act<TOut>(Func<TModel, Result<TOut>> action, Action<TModel, TOut> update)
        => Act(action, (model, result) =>
        {
            Guard.NotNull(update, nameof(update));
            if (model is { }) { update.Invoke(model, result); }
            return model;
        });

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="update">
    /// The update to apply on a successfully invoked action.
    /// </param>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public Result<TModel> Act<TOut>(Func<TModel, Result<TOut>> action, Func<TModel, TOut, TModel> update)
    {
        Guard.NotNull(update, nameof(update));

        var resolved = Act(action);
        return resolved.IsValid
            ? new(update(_value!, resolved.Value), (FixedMessages)resolved.Messages)
            : new(_value, (FixedMessages)resolved.Messages);
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
    public async Task<Result<TOut>> ActAsync<TOut>(
        Func<TModel, Task<Result<TOut>>> action,
        bool continueOnCapturedContext = false)
    {
        _ = Guard.NotNull(action, nameof(action));

        if (IsValid)
        {
            var outcome = await action(Value).ConfigureAwait(continueOnCapturedContext);
            var value = outcome.IsValid ? outcome.Value : default;
            return new(value, ((FixedMessages)Messages).AddRange(outcome.Messages));
        }
        else return WithMessages<TOut>(Messages);
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
    public async Task<Result<TModel>> ActAsync(
        Func<TModel, Task<Result>> action,
        bool continueOnCapturedContext = false)
    {
        _ = Guard.NotNull(action, nameof(action));

        if (IsValid)
        {
            var outcome = await action(Value).ConfigureAwait(continueOnCapturedContext);
            return For(Value, ((FixedMessages)Messages).AddRange(outcome.Messages));
        }
        else return WithMessages<TModel>(Messages);
    }

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <param name="update">
    /// The update to apply on a successfully invoked action.
    /// </param>
    /// <typeparam name="TOut">
    /// The type of the new result value.
    /// </typeparam>
    /// <returns>
    /// The updated model with the merged messages.
    /// </returns>
    [Impure]
    public Task<Result<TModel>> ActAsync<TOut>(
        Func<TModel, Task<Result<TOut>>> action,
        Action<TModel, TOut> update)
        => ActAsync(action, (model, result) =>
        {
            Guard.NotNull(update, nameof(update));
            if (model is { }) { update.Invoke(model, result); }
            return model;
        });

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
    public async Task<Result<TModel>> ActAsync<TOut>(
        Func<TModel, Task<Result<TOut>>> action,
        Func<TModel, TOut, TModel> update,
        bool continueOnCapturedContext = false)
    {
        Guard.NotNull(update, nameof(update));

        var resolved = await ActAsync(action).ConfigureAwait(continueOnCapturedContext);
        return resolved.IsValid
            ? new(update(_value!, resolved.Value), (FixedMessages)resolved.Messages)
            : new(_value, (FixedMessages)resolved.Messages);
    }

    /// <summary>Explicitly casts the <see cref="Result"/> to the type of the related model.</summary>
    public static explicit operator TModel?(Result<TModel>? result) => result == null ? default : result.Value;

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="result">
    /// The result to act on.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    public static Result<TModel> operator |(Result<TModel> result, Func<TModel, Result<TModel>> action)
        => Guard.NotNull(result, nameof(result)).Act(action);

    /// <summary>Invokes the action when <see cref="Result{TModel}"/> is valid.</summary>
    /// <param name="result">
    /// The result to act on.
    /// </param>
    /// <param name="action">
    /// The action to invoke.
    /// </param>
    /// <returns>
    /// A result with the merged messages.
    /// </returns>
    public static Result<TModel> operator |(Result<TModel> result, Func<TModel, Result> action)
        => Guard.NotNull(result, nameof(result)).Act(action);

    /// <summary>Throws <see cref="NoValue"/> exception when valid with null value.</summary>
    [Pure]
    internal Result<TModel> NotNull()
       => IsValid && Value is null ? throw NoValue.For<TModel>() : this;
}
