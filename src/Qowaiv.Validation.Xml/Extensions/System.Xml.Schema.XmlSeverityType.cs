namespace System.Xml.Schema;

/// <summary>Extensions on <see cref="XmlSeverityType"/>.</summary>
public static class QowaivSeverityExtensions
{
    /// <summary>Converts <see cref="XmlSeverityType"/> to <see cref="ValidationSeverity"/>.</summary>
    [Pure]
    public static ValidationSeverity ToValidationSeverity(this XmlSeverityType severity)
        => severity == XmlSeverityType.Warning
        ? ValidationSeverity.Warning
        : ValidationSeverity.Error;
}
