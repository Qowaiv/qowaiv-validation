using System.ComponentModel.DataAnnotations;
using System.Linq;

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
    public void EmailAddress_Unknown_when_uknown_values_are_allowed()
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
    [TestCase("en-UK", "The TestField field is required.")]
    public void is_Culture_dependent(CultureInfo culture, string message)
    {
        using (culture.Scoped())
        {
            new AnnotatedModelValidator<Model>().Validate(new Model())
                .Should().BeInvalid().WithMessage(ValidationMessage.Error(message, "TestField"));
        }
    }

    internal class Model
    {
        [Mandatory]
        public string TestField { get; set; }
    }
}
