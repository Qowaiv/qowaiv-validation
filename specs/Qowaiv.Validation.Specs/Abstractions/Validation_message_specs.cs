using Qowaiv.Validation.Abstractions;

namespace Abstractions.Validation_message_specs;

public class None
{
    [Test]
    public void Has_an_empty_string_representation()
    {
        var message = ValidationMessage.None;
        Assert.AreEqual("", message.ToString());
    }
}

public class Serialization
{
    [Test]
    public void On_an_info_message_keeps_all_data()
    {
        var message = ValidationMessage.Info("Can be serialized", "Prop");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(message);
    }

    [Test]
    public void On_a_warning_message_keeps_all_data()
    {
        var message = ValidationMessage.Warn("Can be serialized", "Prop");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(message);
    }

    [Test]
    public void On_an_error_message_keeps_all_data()
    {
        var message = ValidationMessage.Error("Can be serialized", "Prop");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(message);
    }
}
