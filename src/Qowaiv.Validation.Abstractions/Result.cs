namespace Qowaiv.Validation.Abstractions;

/// <summary>Represents a result of a validation, executed command, etcetera.</summary>
[Inheritable]
public class Result
{
    /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
    /// <param name="messages">
    /// The messages related to the result.
    /// </param>
    internal Result(FixedMessages messages)
        => Messages = Guard.NotNull(messages, nameof(messages));

    /// <summary>Gets the messages related to the result.</summary>
    [Pure]
    public IReadOnlyList<IValidationMessage> Messages { get; }

    /// <summary>Return true if there are no error messages, otherwise false.</summary>
    [Pure]
    public bool IsValid => !Errors.Any();

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Error"/>.</summary>

    [Pure]
    public IEnumerable<IValidationMessage> Errors => Messages.GetErrors();

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Warning"/>.</summary>
    [Pure]
    public IEnumerable<IValidationMessage> Warnings => Messages.GetWarnings();

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Info"/>.</summary>
    [Pure]
    public IEnumerable<IValidationMessage> Infos => Messages.GetInfos();

    /// <summary>Applies <see cref="Debugger.Break"/> when not valid and with <see cref="Debugger.IsAttached"/>.</summary>
    internal void BreakIfInvalid()
    {
        if (!IsValid && DebuggerWrapper.IsAttached())
        {
            DebuggerWrapper.Break();
        }
    }

    /// <summary>Represents an OK <see cref="Result"/>.</summary>
    public static readonly Result OK = new(FixedMessages.Empty);

    /// <summary>Creates a valid null <see cref="Result{T}"/>.</summary>
    [Pure]
    public static Result<T> Null<T>(IEnumerable<IValidationMessage> messages)
        => new(FixedMessages.New(messages));

    /// <summary>Creates a valid null <see cref="Result{T}"/>.</summary>
    [Pure]
    public static Result<T> Null<T>(params IValidationMessage[] messages)
        => new(FixedMessages.New(messages));

    /// <summary>Creates a <see cref="Result{T}"/> for the value.</summary>
    [Pure]
    public static Result<T> For<T>(T value, IEnumerable<IValidationMessage> messages)
        => new Result<T>(value, FixedMessages.New(messages)).NotNull();

    /// <summary>Creates a <see cref="Result{T}"/> for the value.</summary>
    [Pure]
    public static Result<T> For<T>(T value, params IValidationMessage[] messages)
        => new Result<T>(value, FixedMessages.New(messages)).NotNull();

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result WithMessages(IEnumerable<IValidationMessage> messages)
        => new(FixedMessages.New(messages));

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result WithMessages(params IValidationMessage[] messages)
        => new(FixedMessages.New(messages));

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result<T> WithMessages<T>(IEnumerable<IValidationMessage> messages)
       => new Result<T>(default, FixedMessages.New(messages)).NotNull();

    /// <summary>Creates a result with messages.</summary>
    [Pure]
    public static Result<T> WithMessages<T>(params IValidationMessage[] messages)
        => new Result<T>(default, FixedMessages.New(messages)).NotNull();
}
