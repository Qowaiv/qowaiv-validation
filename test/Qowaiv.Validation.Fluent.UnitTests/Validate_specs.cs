namespace Validate_specs;

public class Validation_without_warnings
{
    [Test]
    public void Is_valid()
    {
        var model = new WarningModel();
        IValidator<WarningModel> validator = new WarningModelValidator();

        validator.Validate(model)
            .Should().BeValid().WithMessages(
                ValidationMessage.Warn("Test warning.", nameof(model.Message)),
                ValidationMessage.Info("Nice that you validated this.", nameof(model.Message)));
    }
}
