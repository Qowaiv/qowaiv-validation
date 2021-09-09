using FluentAssertions;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class RequiredValidatonTest
    {
        [Test]
        public void Known_IsValid()
            => new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.NL }
            .Should().BeValidFor(new RequiredModelValidator());

        [Test]
        public void Unknown_IsValid()
            => new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.Unknown }
            .Should().BeValidFor(new RequiredModelValidator()); 

        [TestCase("'Email' is required.", "en-GB")]
        [TestCase("'Email' is verplicht.", "nl-BE")]
        public void Empty(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new RequiredModel { Email = EmailAddress.Empty, Country = Country.NL }
                .Should().BeInvalidFor(new RequiredModelValidator())
                .WithMessage(ValidationMessage.Error(message, "Email"));
            }
        }

        [TestCase("'Email' is required.", "en-GB")]
        [TestCase("'Email' is verplicht.", "nl-BE")]
        public void Unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new RequiredModel { Email = EmailAddress.Unknown, Country = Country.NL }
                .Should().BeInvalidFor(new RequiredModelValidator())
                .WithMessage(ValidationMessage.Error(message, "Email"));
            }
        }
    }
}
