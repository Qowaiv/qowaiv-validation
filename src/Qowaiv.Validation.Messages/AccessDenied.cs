namespace Qowaiv.Validation.Messages;

/// <summary>Message to communicate that access has been denied.</summary>
[Serializable]
public class AccessDenied : SecurityException, IValidationMessage
{
    /// <summary>Initializes a new instance of the <see cref="AccessDenied"/> class.</summary>
    public AccessDenied() : this(ValidationMessages.AccessDenied) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AccessDenied"/> class.</summary>
    public AccessDenied(string message) : base(message) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AccessDenied"/> class.</summary>
    public AccessDenied(string message, Exception inner) : base(message, inner) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AccessDenied"/> class.</summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="type">
    /// The type of the permission that caused the exception to be thrown.
    /// </param>
    public AccessDenied(string message, Type type) : base(message, type) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AccessDenied"/> class.</summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="type">
    /// The type of the permission that caused the exception to be thrown.
    /// </param>
    /// <param name="state">
    /// The state of the permission that caused the exception to be thrown.
    /// </param>
    public AccessDenied(string message, Type type, string state) : base(message, type, state) => Do.Nothing();

    /// <inheritdoc />
    public ValidationSeverity Severity => ValidationSeverity.Error;

    /// <inheritdoc />
    public string? PropertyName => null;
}
