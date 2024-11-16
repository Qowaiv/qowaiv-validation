using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.Required_specs;

public class Known
{
    [Test]
    public void Is_valid()
        => new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.NL }
        .Should().BeValidFor(new RequiredModelValidator());
}
public class Uknown
{
    [TestCase("'Email' is required.", "en-GB")]
    [TestCase("'Email' is verplicht.", "nl-BE")]
    public void Not_valid_by_default(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            new RequiredModel { Email = EmailAddress.Unknown, Country = Country.NL }
            .Should().BeInvalidFor(new RequiredModelValidator())
            .WithMessage(ValidationMessage.Error(message, "Email"));
        }
    }

    [Test]
    public void Is_valid_when_explictly_allowed()
        => new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.Unknown }
        .Should().BeValidFor(new RequiredModelValidator());
}
public class Empty
{ 
    [TestCase("'Email' is required.", "en-GB")]
    [TestCase("'Email' is verplicht.", "nl-BE")]
    public void Not_valid(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            new RequiredModel { Email = EmailAddress.Empty, Country = Country.NL }
            .Should().BeInvalidFor(new RequiredModelValidator())
            .WithMessage(ValidationMessage.Error(message, "Email"));
        }
    }
}
