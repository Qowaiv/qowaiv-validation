namespace Qowaiv.Validation.Messages;

/// <summary>Message to communicate if a entity could not be found.</summary>
[Serializable]
public class EntityNotFound : InvalidOperationException, IValidationMessage
{
    /// <summary>Initializes a new instance of the <see cref="EntityNotFound"/> class.</summary>
    public EntityNotFound() : this(ValidationMessages.EntityNotFound) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="EntityNotFound"/> class.</summary>
    public EntityNotFound(string message) : base(message) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="EntityNotFound"/> class.</summary>
    public EntityNotFound(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

    /// <inheritdoc />
    public ValidationSeverity Severity => ValidationSeverity.Error;

    /// <inheritdoc />
    public string? PropertyName => null;

    /// <summary>Creates an <see cref="EntityNotFound"/> for specific ID.</summary>
    [Pure]
    public static EntityNotFound ForId(object id)
        => new(string.Format(ValidationMessages.EntityNotFound_ForId, id));
}
