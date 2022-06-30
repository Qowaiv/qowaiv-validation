using Qowaiv.Validation.DataAnnotations;
using ValidationSeverity = Qowaiv.Validation.Abstractions.ValidationSeverity;

namespace Data_annotations.ValidationMessage_specs;

public class Serializes
{
    [Test]
    public void Info_message()
    {
        var message = ValidationMessage.Info("Can be serialized", "ErrorMessage", "MemberNames");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(new
        {
            Severity = ValidationSeverity.Info,
            ErrorMessage = "Can be serialized",
            MemberNames = new[] { "ErrorMessage", "MemberNames" },
        });
    }

    [Test]
    public void Warning_message()
    {
        var message = ValidationMessage.Warn("Can be serialized", "ErrorMessage", "MemberNames");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(new
        {
            Severity = ValidationSeverity.Warning,
            ErrorMessage = "Can be serialized",
            MemberNames = new[] { "ErrorMessage", "MemberNames" },
        });
    }

    [Test]
    public void Error_message()
    {
        var message = ValidationMessage.Error("Can be serialized", "ErrorMessage", "MemberNames");
        SerializeDeserialize.Binary(message).Should().BeEquivalentTo(new
        {
            Severity = ValidationSeverity.Error,
            ErrorMessage = "Can be serialized",
            MemberNames = new[] { "ErrorMessage", "MemberNames" },
        });
    }
}
