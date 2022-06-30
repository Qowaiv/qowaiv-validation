using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Allowed_values_specs;

public class Is_valid_for
{
    [Test]
    public void Null()
        => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(null).Should().BeTrue();

    [Test]
    public void value_in_allowed_values()
        => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(Country.GB).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void value_not_in_allowed_values()
       => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(Country.TR).Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "De waarde van het veld Country is niet toegestaan.")]
    [TestCase("en", "The value of the Country field is not allowed.")]
    public void culture_depedent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "Country"));
    }
    internal class Model
    {
        [AllowedValues("DE", "FR", "GB")]
        public Country Country { get; set; } = Country.NL;
    }
}
