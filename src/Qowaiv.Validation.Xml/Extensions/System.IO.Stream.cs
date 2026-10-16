namespace System.IO;

internal static class QowaivValidationStreamExtensions
{
    extension(Stream stream)
    {
        [Pure]
        public XmlSchema AsXmlSchema()
        {
            using var reader = new XmlTextReader(stream);
            return XmlSchema.Read(reader, null)!;
        }
    }
}
