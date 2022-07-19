using Qowaiv.Validation.Abstractions.Diagnostics.Contracts;
using System.IO;

namespace System.Xml.Schema;

/// <summary>Extensions on <see cref="XmlSchemaSet"/>.</summary>
public static class QowaivXmlSchemaSetExtensions
{
    /// <summary>Appends an <see cref="XmlSchema"/> to the <see cref="XmlSchemaSet"/>.</summary>
    [Impure]
    public static XmlSchemaSet Append(this XmlSchemaSet set, XmlSchema schema)
    {
        Guard.NotNull(set, nameof(set));
        Guard.NotNull(schema, nameof(schema));
        set.Add(schema);
        return set;
    }
}
