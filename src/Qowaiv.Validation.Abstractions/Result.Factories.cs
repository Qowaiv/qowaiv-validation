namespace Qowaiv.Validation.Abstractions;

public partial class Result
{
    /// <summary>Represents an OK <see cref="Result"/>.</summary>
    public static readonly Result OK = new([]);

    /// <summary>Creates a <see cref="AsyncResult{T}"/> for the value.</summary>
    [Pure]
    public static AsyncResult<T> Async<T>(T value, params IEnumerable<IValidationMessage> messages)
        where T : notnull
            => For(value, messages).Async();

    /// <summary>Creates a valid null <see cref="Result{T}"/>.</summary>
    [Pure]
    public static Result<T> Null<T>(params IEnumerable<IValidationMessage> messages)
        => new(ValidationMessages.New(messages));

    /// <summary>Creates a <see cref="Result{T}"/> for the value.</summary>
    [Pure]
    public static Result<T> For<T>(T value, params IEnumerable<IValidationMessage> messages)
        where T : notnull
        => new Result<T>(value, ValidationMessages.New(messages)).NotNull();

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result WithMessages(params IEnumerable<IValidationMessage> messages)
        => new(ValidationMessages.New(messages));

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result<TModel> WithMessages<TModel>(params IEnumerable<IValidationMessage> messages)
         => new Result<TModel>(ValidationMessages.New(messages)).NotNull();
}
