namespace ValidationMessage_specs;

public class Serializes
{
    [Test]
    public void Info_message()
    {
        var message = ValidationMessage.Info("Can be serialized", "ErrorMessage", "MemberNames");

        var actual = SerializeDeserialize.Binary(message);

        Assert.AreEqual(message.Severity, actual.Severity);
        Assert.AreEqual(message.ErrorMessage, actual.ErrorMessage);
        Assert.AreEqual(message.MemberNames, actual.MemberNames);
    }

    [Test]
    public void Warning_message()
    {
        var message = ValidationMessage.Warn("Can be serialized", "ErrorMessage", "MemberNames");

        var actual = SerializeDeserialize.Binary(message);

        Assert.AreEqual(message.Severity, actual.Severity);
        Assert.AreEqual(message.ErrorMessage, actual.ErrorMessage);
        Assert.AreEqual(message.MemberNames, actual.MemberNames);
    }

    [Test]
    public void Error_message()
    {
        var message = ValidationMessage.Error("Can be serialized", "ErrorMessage", "MemberNames");

        var actual = SerializeDeserialize.Binary(message);

        Assert.AreEqual(message.Severity, actual.Severity);
        Assert.AreEqual(message.ErrorMessage, actual.ErrorMessage);
        Assert.AreEqual(message.MemberNames, actual.MemberNames);
    }
}
