namespace Qowaiv.Validation.Xml;

/// <summary>An <see cref="ValidationEventHandler"/> that collects all issues as <see cref="IValidationMessage"/>'s.</summary>
[WillBeSealed]
public class XmlValidationHandler
{
    /// <summary>The collection of validation messages.</summary>
    public IReadOnlyCollection<IValidationMessage> Messages { get; } = new List<IValidationMessage>();

    /// <summary>The <see cref="ValidationEventHandler"/> delegate.</summary>
    public ValidationEventHandler Validate => (sender, e) => OnIssue(sender, e);

    private void OnIssue(object? sender, ValidationEventArgs e)
        => ((List<IValidationMessage>)Messages)
        .Add(ValidationMessage.For(e.Severity.ToValidationSeverity(), e.Message, PropertyName(sender)));

    [Pure]
    private static string? PropertyName(object? sender) => sender switch
    {
        XElement x => x.AbsoluteXPath(),
        XAttribute x => x.AbsoluteXPath(),
        _ => null,
    };
}
