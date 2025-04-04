namespace Qowaiv.Validation.Abstractions;

/// <summary>Represents an exception that is shown once tried to access the invalid model of a <see cref="Result{T}"/>.</summary>
[Serializable]
public class InvalidModelException : InvalidOperationException
{
    /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
    public InvalidModelException() { }

    /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
    public InvalidModelException(string? message)
        : base(message) { }

    /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
    public InvalidModelException(string? message, Exception? innerException)
        : base(message, innerException) { }

    /// <summary>Initializes a new instance of the <see cref="InvalidModelException"/> class.</summary>
    public InvalidModelException(string? message, Exception? innerException, IEnumerable<IValidationMessage> messages)
        : this(message, innerException)
        => Errors = new ReadOnlyCollection<IValidationMessage>([.. Filter(messages)]);

    /// <summary>The related validation error(s).</summary>
    public IReadOnlyList<IValidationMessage> Errors { get; } = [];

    /// <summary>Creates an <see cref="InvalidModelException"/> for the model.</summary>
    [Pure]
    public static InvalidModelException For<T>(IEnumerable<IValidationMessage> messages)
    {
        var sb = new StringBuilder().AppendFormat(QowaivValidationMessages.InvalidModelException, typeof(T));
        var filtered = Filter(messages);

        if (filtered.Any())
        {
            sb.Remove(sb.Length - 1, 1).AppendLine(":");
            foreach (var message in filtered)
            {
                Append(sb, message);
            }
        }
        return new InvalidModelException(sb.ToString(), null, messages);
    }

    [Pure]
    private static IEnumerable<IValidationMessage> Filter(IEnumerable<IValidationMessage> messages)
        => (messages ?? [])
        .Where(e => e.Severity >= ValidationSeverity.Error);

    private static void Append(StringBuilder builder, IValidationMessage message)
    {
        var text = string.IsNullOrWhiteSpace(message.Message)
            ? "Validation Error."
            : message.Message!.Trim();
        var lines = text.Split(NewLine, StringSplitOptions.None);
        builder
            .Append("* ")
            .Append(string.Join(Environment.NewLine + "  ", lines))
            .AppendLine(PropertySuffix(message));
    }

    [Pure]
    private static string PropertySuffix(IValidationMessage message)
        => string.IsNullOrWhiteSpace(message.PropertyName) ? string.Empty : $" ({message.PropertyName})";

    private static readonly string[] NewLine = ["\r\n", "\n"];
}
