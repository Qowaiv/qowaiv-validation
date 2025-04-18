using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.IPBased_specs;

public class Valid_for
{
    [Test]
    public void NoIPBase_IsValid()
        => new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("test@qowaiv.org") }
        .ValidateWith(new NoIPBasedEmailAddressModelValidator())
        .Should().BeValid();

    [TestCase("'Email' has an IP address based domain.", "en-GB")]
    [TestCase("'Email' heeft een IP-adres als domein.", "nl-BE")]
    public void IPBased(string message, CultureInfo culture)
    {
        using (culture.Scoped())
        {
            new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("qowaiv@172.16.254.1") }
            .ValidateWith(new NoIPBasedEmailAddressModelValidator())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "Email"));
        }
    }
}
