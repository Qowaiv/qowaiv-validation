using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.Unknown_specs;

public class Set_value
{
    [Test]
    public void Is_valid()
        => new UnknownModel { Email = EmailAddress.Parse("info@qowaiv.corg") }
        .ShouldBeValidFor(new UnknownModelValidator());
}

public class Unknown
{
    [TestCase("'Email' must not be unknown.", "en-GB")]
    [TestCase("'Email' mag niet onbekend zijn.", "nl-BE")]
    public void Not_valid(string message, CultureInfo culture)
    {
        using (new CultureInfoScope(culture))
        {
            new UnknownModel { Email = EmailAddress.Unknown }
                .ShouldBeInvalidFor(new UnknownModelValidator())
                .WithMessage(ValidationMessage.Error(message, "Email"));
        }
    }
}

public class Empty
{
    [Test]
    public void Is_valid()
        => new UnknownModel { Email = EmailAddress.Empty }
        .ShouldBeValidFor(new UnknownModelValidator());
}
