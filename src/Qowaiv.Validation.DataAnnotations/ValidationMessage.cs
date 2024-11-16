using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents a <see cref="ValidationResult"/> as a <see cref="IValidationMessage"/>.</summary>
[Inheritable]
[Serializable]
public class ValidationMessage : ValidationResult, IValidationMessage
{
    /// <summary>Initializes a new instance of the <see cref="ValidationMessage"/> class.</summary>
    public ValidationMessage() : this(ValidationSeverity.None, null, null) => Do.Nothing();

    internal ValidationMessage(ValidationSeverity severity, string? message, string[]? memberNames)
        : base(message, memberNames ?? [])
        => Severity = severity;

    /// <inheritdoc />
    public ValidationSeverity Severity { get; }

    /// <inheritdoc />
    public string? PropertyName => MemberNames.FirstOrDefault();

    /// <inheritdoc />
    public string? Message => ErrorMessage;

    /// <summary>Creates a None message.</summary>
    public static ValidationMessage None => new();

    /// <summary>Creates an error message.</summary>
    [Pure]
    public static ValidationMessage Error(string? message, params string[] memberNames) => new(ValidationSeverity.Error, message, memberNames);

    /// <summary>Creates a warning message.</summary>
    [Pure]
    public static ValidationMessage Warn(string? message, params string[] memberNames) => new(ValidationSeverity.Warning, message, memberNames);

    /// <summary>Creates an info message.</summary>
    [Pure]
    public static ValidationMessage Info(string? message, params string[] memberNames) => new(ValidationSeverity.Info, message, memberNames);

    /// <summary>Creates a validation message for a validation result.</summary>
    /// <param name="validationResult">
    /// The validation result to convert.
    /// </param>
    [Pure]
    public static ValidationMessage For(ValidationResult validationResult)
        => validationResult switch
        {
            null => None,
            ValidationMessage message => message,
            _ => validationResult == Success
                ? None
                : Error(validationResult.ErrorMessage, validationResult.MemberNames.ToArray()),
        };

    /// <summary>Creates a validation message.</summary>
    [Pure]
    public static IValidationMessage For(ValidationSeverity severity, string message, params string[] memberNames)
        => severity switch
        {
            ValidationSeverity.None => None,
            ValidationSeverity.Info => Info(message, memberNames),
            ValidationSeverity.Warning => Warn(message, memberNames),
            _ => Error(message, memberNames),
        };
}
