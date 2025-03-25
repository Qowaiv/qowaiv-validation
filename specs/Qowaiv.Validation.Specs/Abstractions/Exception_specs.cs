using Qowaiv.Validation.Abstractions;

namespace Abstractions.Exception_specs;

public class InvalidModel
{
    [Test]
    public void Null_messages_is_fine()
    {
        var exception = InvalidModelException.For<int>(null!);
        exception.Message.Should().Be("The System.Int32 model can not be operated on as it is invalid.");
    }

    [Test]
    public void No_list_without_errors_specified()
    {
        var exception = InvalidModelException.For<int>([ValidationMessage.None]);
        exception.Message.Should().Be("The System.Int32 model can not be operated on as it is invalid.");
    }
    
    [Test]
    public void Contains_the_error_messages_in_Message()
    {
        var exception = InvalidModelException.For<int>(
        [
            ValidationMessage.None,
            ValidationMessage.Error("Not a prime", "_value"),
            ValidationMessage.Error("Multi-line\nmessage.", "multi-line"),
            ValidationMessage.Error("No property specified."),
            ValidationMessage.Error("  ", "no-message"),
            ValidationMessage.Warn("Small", "_value"),
            ValidationMessage.Info("Not my favorite", "_value"),
            ValidationMessage.Error("Not a multiple of 17", "_value"),
        ]);

        exception.Message.Should().Be(
           @"The System.Int32 model can not be operated on as it is invalid:
* Not a prime (_value)
* Multi-line
  message. (multi-line)
* No property specified.
* Validation Error. (no-message)
* Not a multiple of 17 (_value)
");
    }
}
