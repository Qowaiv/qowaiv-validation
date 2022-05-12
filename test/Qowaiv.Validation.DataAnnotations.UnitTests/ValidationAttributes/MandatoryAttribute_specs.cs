using System.ComponentModel.DataAnnotations;

namespace Attributes.MandatoryAttribute_specs;

public class Is_valid_for
{
    [Test]
    public void NewGuid()
        => new MandatoryAttribute().IsValid(Guid.NewGuid()).Should().BeTrue();

    [Test]
    public void Known_EmailAddress()
        => new MandatoryAttribute().IsValid(EmailAddress.Parse("test@exact.com")).Should().BeTrue();


    [Test]
    public void EmailAddress_Unknown_when_unknown_values_are_allowed()
        => new MandatoryAttribute { AllowUnknownValue = true }.IsValid(EmailAddress.Unknown).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void Guid_Empty()
        => new MandatoryAttribute().IsValid(Guid.Empty).Should().BeFalse();

    [Test]
    public void EmailAddress_Empty()
        => new MandatoryAttribute().IsValid(EmailAddress.Empty).Should().BeFalse();

    [Test]
    public void EmailAddress_Unknown()
      => new MandatoryAttribute().IsValid(EmailAddress.Unknown).Should().BeFalse();
}

public class Validation_message
{
    [TestCase("nl-NL", "Het veld TestField is verplicht.")]
    [TestCase("en-GB", "The TestField field is required.")]
    public void is_Culture_dependent(CultureInfo culture, string message)
    {
        using (culture.Scoped())
        {
            new AnnotatedModelValidator<Model>().Validate(new Model { ReferenceField = "ignore" })
                .Should().BeInvalid().WithMessage(ValidationMessage.Error(message, "TestField"));
        }
    }

    [Test]
    public void English_message_equals_required_message()
    {
        using (new CultureInfo("en-GB").Scoped()) 
        { 
            new AnnotatedModelValidator<Model>().Validate(new Model())
                .Should().BeInvalid().WithMessages(
                    ValidationMessage.Error("The TestField field is required.", "TestField"),
                    ValidationMessage.Error("The ReferenceField field is required.", "ReferenceField"));
        }
    }

    internal class Model
    {
        [Mandatory]
        public string TestField { get; set; }

        [Required]
        public string ReferenceField { get; set; }
    }
}
