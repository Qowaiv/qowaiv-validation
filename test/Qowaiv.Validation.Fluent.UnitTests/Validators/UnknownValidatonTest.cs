using FluentAssertions;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class UnknownValidatonTest
    {
        [Test]
        public void NotEmptyOrUnknown_is_valid_when_value()
            => new UnknownModel { Country = Country.NL }
            .Should().BeValidFor(new UnknownModelValidator());

        [TestCase("'Country' must not be empty or unknown.", "en-GB")]
        [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
        public void NotEmptyOrUnknown_with_error_when_empty(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new UnknownModel { Country = Country.Empty }
                    .Should().BeInvalidFor(new UnknownModelValidator())
                    .WithMessage(ValidationMessage.Error(message, "Country"));
            }
        }

        [TestCase("'Country' must not be empty or unknown.", "en-GB")]
        [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
        public void NotEmptyOrUnknown_with_error_when_unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new UnknownModel { Country = Country.Unknown }
                    .Should().BeInvalidFor(new UnknownModelValidator())
                    .WithMessage(ValidationMessage.Error(message, "Country"));
            }
        }

        [Test]
        public void NoUnknown_is_valid_when_empty()
            => new UnknownModel { Email = EmailAddress.Empty }
            .Should().BeValidFor(new UnknownModelValidator());

        [Test]
        public void NoUnknown_is_valid_when_set()
            => new UnknownModel { Email = EmailAddress.Parse("test@qowaiv.org") }
            .Should().BeValidFor(new UnknownModelValidator());

        [TestCase("'Email' must not be unknown.", "en-GB")]
        [TestCase("'Email' mag niet onbekend zijn.", "nl-BE")]
        public void NotUnknown_with_error_when_unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new UnknownModel { Email = EmailAddress.Unknown }
                    .Should().BeInvalidFor(new UnknownModelValidator())
                    .WithMessage(ValidationMessage.Error(message, "Email"));
            }
        }

        [Test]
        public void Validate_empty_WithSeverity_has_warning()
            => new UnknownWithSeverityModel { Email = EmailAddress.Empty }
            .Should().BeInvalidFor(new UnknownWithSeverityModelValidator())
            .WithMessage(ValidationMessage.Error("'Email' must not be empty or unknown.", "Email"));

        [Test]
        public void Validate_unknown_WithSeverity_has_warning()
            => new UnknownWithSeverityModel { Email = EmailAddress.Unknown }
            .Should().BeInvalidFor(new UnknownWithSeverityModelValidator())
            .WithMessage(ValidationMessage.Error("'Email' must not be empty or unknown.", "Email"));
    }
}
