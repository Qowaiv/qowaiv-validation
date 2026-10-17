namespace Qowaiv.Validation.Abstractions;

/// <summary>Represents a result of a validation, executed command, etcetera.</summary>
[Inheritable]
public partial class Result
{
    /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
    /// <param name="messages">
    /// The messages related to the result.
    /// </param>
    internal Result(IValidationMessages messages)
        => Messages = messages;

    /// <summary>Gets the messages related to the result.</summary>
    [Pure]
    public IValidationMessages Messages { get; }

    /// <summary>Return true if there are no error messages, otherwise false.</summary>
    [Pure]
    public bool IsValid => !Errors.Any();

    /// <summary>Throws an <see cref="InvalidModel"/> if the result is not valid.</summary>
    public void ThrowIfInvalid()
    {
        if (!IsValid)
        {
            throw InvalidOperation();
        }
    }

    /// <summary>Wraps the <see cref="Result{TModel}"/> in an <see cref="AsyncResult"/>.</summary>
    [Pure]
    public AsyncResult Async() => new(Task.FromResult(this));

    /// <summary>Generated an <see cref="InvalidModel" /> based on the errors.</summary>
    [Pure]
    protected internal virtual InvalidModel InvalidOperation()
        => InvalidModel.For(Errors);

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Error"/>.</summary>
    [Pure]
    public IEnumerable<IValidationMessage> Errors => Messages.GetErrors();

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Warning"/>.</summary>
    [Pure]
    public IEnumerable<IValidationMessage> Warnings => Messages.GetWarnings();

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Info"/>.</summary>
    [Pure]
    public IEnumerable<IValidationMessage> Infos => Messages.GetInfos();
}
