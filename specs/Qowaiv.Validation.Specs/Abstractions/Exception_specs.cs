using Qowaiv.Validation.Abstractions;
using static Specs.Abstractions.Arrange;

namespace Abstractions.Exception_specs;

public class InvalidModel
{
    [Test]
    public void Null_messages_is_fine()
    {
        var exception = InvalidModelException.For<int>(null!);
        Assert.That(exception.Message, Is.EqualTo("The System.Int32 model can not be operated on as it is invalid."));
    }

    [Test]
    public void No_list_without_errors_specified()
    {
        var exception = InvalidModelException.For<int>(new IValidationMessage[]
        {
            ValidationMessage.None,
        });
        Assert.That(exception.Message, Is.EqualTo("The System.Int32 model can not be operated on as it is invalid."));
    }
    
    [Test]
    public void Contains_the_error_messages_in_Message()
    {
        var exception = InvalidModelException.For<int>(new IValidationMessage[]
        {
            ValidationMessage.None,
            ValidationMessage.Error("Not a prime", "_value"),
            ValidationMessage.Error("Multi-line\nmessage.", "multi-line"),
            ValidationMessage.Error("No property specified."),
            ValidationMessage.Error("  ", "no-message"),
            ValidationMessage.Warn("Small", "_value"),
            ValidationMessage.Info("Not my favorite", "_value"),
            ValidationMessage.Error("Not a multiple of 17", "_value"),
        });

        Assert.That(exception.Message, Is.EqualTo(
            @"The System.Int32 model can not be operated on as it is invalid:
* Not a prime (_value)
* Multi-line
  message. (multi-line)
* No property specified.
* Validation Error. (no-message)
* Not a multiple of 17 (_value)
"));
    }

    [Test]
    [Obsolete("Binary serialization is considered harmful.")]
    public void Serializes_the_error_messages()
    {
        var exception = InvalidModelException.For<int>(TestMessages);
        var actual = SerializeDeserialize.Binary(exception);
        actual.Should().BeEquivalentTo(new[] { Error1, Error2 });
    }
}
