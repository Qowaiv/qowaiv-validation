namespace Qowaiv.Validation.Messages;

/// <summary>Message to communicate that a (external) service/system/process is unavailable.</summary>
[Serializable]
public class ServiceUnavailable : Exception, IValidationMessage
{
    /// <summary>Initializes a new instance of the <see cref="ServiceUnavailable"/> class.</summary>
    public ServiceUnavailable() : this(ValidationMessages.ServiceUnavailable) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="ServiceUnavailable"/> class.</summary>
    public ServiceUnavailable(string message) : base(message) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="ServiceUnavailable"/> class.</summary>
    public ServiceUnavailable(string message, Exception innerException) : base(message, innerException) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="ServiceUnavailable"/> class.</summary>
    protected ServiceUnavailable(SerializationInfo info, StreamingContext context) : base(info, context) => Do.Nothing();

    /// <inheritdoc />
    public ValidationSeverity Severity => ValidationSeverity.Error;

    /// <inheritdoc />
    public string PropertyName { get; set; }

    /// <summary>Creates <see cref="ServiceUnavailable"/> for the service with the specified name.</summary>
    /// <param name="name">
    /// The name of the service.
    /// </param>
    [Pure]
    public static ServiceUnavailable WithName(string name)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        return new ServiceUnavailable(string.Format(ValidationMessages.ServiceUnavailable_WithName, name))
        {
            PropertyName = name,
        };
    }
}
