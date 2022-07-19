namespace System.Xml.Linq;

/// <summary>Extensions on <see cref="XObject"/>.</summary>
public static class QowaivXObjectExtensions
{
    /// <summary>Validates the document using a <see cref="Stream"/> representing an <see cref="XmlSchema"/>.</summary>
    [Pure]
    public static Result<XDocument> Validate(this XDocument document, Stream stream)
        => document.Validate(stream.AsXmlSchema());

    /// <summary>Validates the document using a <see cref="Stream"/> representing an <see cref="XmlSchema"/>.</summary>
    [Pure]
    public static Result<XDocument> Validate(this XDocument document, XmlSchema schema)
        => document.Validate(new XmlSchemaSet().Append(schema));

    /// <summary>Validates the document using an <see cref="XmlSchemaSet"/>.</summary>
    [Pure]
    public static Result<XDocument> Validate(this XDocument document, XmlSchemaSet schemas)
    {
        Guard.NotNull(document, nameof(document));
        Guard.NotNull(schemas, nameof(schemas));

        var handler = new XmlValidationHandler();
        document.Validate(schemas, handler.Validate);
        return Result.For(document, handler.Messages);
    }

    /// <summary>Get the absolute XPath of the attribute.</summary>
    /// <remarks>
    /// e.g.: /people/person[6]/name[1]/last[1]/@prefx
    /// </remarks>
    [Pure]
    internal static string AbsoluteXPath(this XAttribute attribute)
    {
        Guard.NotNull(attribute, nameof(attribute));
        return $"{attribute.Parent?.AbsoluteXPath()}/@{attribute.Name.LocalName}";
    }

    /// <summary>Get the absolute XPath of the element.</summary>
    /// <remarks>
    /// e.g.: /people/person[6]/name[1]/last[1]
    /// </remarks>
    [Pure]
    internal static string AbsoluteXPath(this XElement element)
    {
        Guard.NotNull(element, nameof(element));
        return string.Concat(element
            .Ancestors().Select(RelativePath).Reverse()
            .Append(element.RelativePath()));
    }

    [Pure]
    private static string RelativePath(this XElement element)
    {
        int index = element.IndexPosition();
        string name = element.Name.LocalName;

        return index == -1
            ? $"/{name}"
            : $"/{name}[{index}]";
    }

    /// <summary>
    /// Get the index of the given XElement relative to its
    /// siblings with identical names. If the given element is
    /// the root, -1 is returned.
    /// </summary>
    /// <param name="element">
    /// The element to get the index of.
    /// </param>
    [Pure]
    private static int IndexPosition(this XElement element)
    {
        if (element.Parent is { })
        {
            var index = 0;

            foreach (var sibling in element.Parent.Elements(element.Name))
            {
                index++;
                if (sibling == element) return index;
            }
            throw new InvalidOperationException("element has been removed from its parent.");
        }
        else return -1;
    }
}
