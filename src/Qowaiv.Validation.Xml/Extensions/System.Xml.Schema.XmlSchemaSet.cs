namespace System.Xml.Schema;

/// <summary>Extensions on <see cref="XmlSchemaSet"/>.</summary>
public static class QowaivXmlSchemaSetExtensions
{
    /// <summary>Appends an <see cref="XmlSchema"/> to the <see cref="XmlSchemaSet"/>.</summary>
    [FluentSyntax]
    public static XmlSchemaSet Append(this XmlSchemaSet set, XmlSchema schema)
    {
        Guard.NotNull(set).Add(Guard.NotNull(schema));
        return set;
    }
}
