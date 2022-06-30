using Qowaiv.Financial;
using Specs;
using System.ComponentModel.DataAnnotations;

namespace Data_annotations.Attributes.Mandatory_specs;

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

public class With_message
{
    [TestCase("nl-NL", "Het veld TestField is verplicht.")]
    [TestCase("en-GB", "The TestField field is required.")]
    public void culture_depedent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new AnnotatedModelValidator<Model>().Validate(new Model { ReferenceField = "ignore" })
            .Should().BeInvalid().WithMessage(ValidationMessage.Error(message, "TestField"));
    }
   
    [Test]
    [SetCulture("en-GB")]
    public void equal_to_required_message()
        => new AnnotatedModelValidator<Model>().Validate(new Model())
            .Should().BeInvalid().WithMessages(
                ValidationMessage.Error("The TestField field is required.", "TestField"),
                ValidationMessage.Error("The ReferenceField field is required.", "ReferenceField"));

    [Test]
    [SetCulture("en-GB")]
    public void supporting_custom_error_resource_type()
        => new WithCustomErrorMessageType().Should().BeInvalidFor(new AnnotatedModelValidator<WithCustomErrorMessageType>())
        .WithMessage(ValidationMessage.Error("This Iban is wrong.", "Iban"));

    [Test]
    [SetCulture("en-GB")]
    public void supporting_custom_display()
        => new WithCustomDisplay().Should().BeInvalidFor(new AnnotatedModelValidator<WithCustomDisplay>())
        .WithMessage(ValidationMessage.Error("The IBAN field is required.", "Iban"));

    internal class Model
    {
        [Mandatory]
        public string TestField { get; }

        [Required]
        public string ReferenceField { get; set; }
    }

    public class WithCustomErrorMessageType
    {
        [Mandatory(ErrorMessageResourceType = typeof(TestMessages), ErrorMessageResourceName = "TestError")]
        public InternationalBankAccountNumber Iban { get; set; }
    }

    public class WithCustomDisplay
    {
        [Mandatory]
        [Display(Name = "IBAN")]
        public InternationalBankAccountNumber Iban { get; set; }
    }
}
