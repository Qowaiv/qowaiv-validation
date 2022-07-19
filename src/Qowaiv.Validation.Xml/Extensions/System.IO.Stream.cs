using System.Xml.Schema;
using System.Xml;

namespace System.IO;

internal static class QowaivValidationStreamExtensions
{
    [Pure]
    public static XmlSchema AsXmlSchema(this Stream stream)
    {
        using var reader = new XmlTextReader(stream);
        return XmlSchema.Read(reader, null)!;
    }
}
