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
        {
            var model = new UnknownModel { Country = Country.NL };
            FluentValidatorAssert.IsValid<UnknownModelValidator, UnknownModel>(model);
        }

        [TestCase("'Country' must not be empty or unknown.", "en-GB")]
        [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
        public void NotEmptyOrUnknown_with_error_when_empty(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new UnknownModel { Country = Country.Empty };

                FluentValidatorAssert.WithErrors<UnknownModelValidator, UnknownModel>(model,
                    ValidationMessage.Error(message, "Country")
                );
            }
        }

        [TestCase("'Country' must not be empty or unknown.", "en-GB")]
        [TestCase("'Country' mag niet leeg of onbekend zijn.", "nl-BE")]
        public void NotEmptyOrUnknown_with_error_when_unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new UnknownModel { Country = Country.Unknown };

                FluentValidatorAssert.WithErrors<UnknownModelValidator, UnknownModel>(model,
                    ValidationMessage.Error(message, "Country")
                );
            }
        }

        [Test]
        public void NoUnknown_is_valid_when_empty()
        {
            var model = new UnknownModel { Email = EmailAddress.Empty };
            FluentValidatorAssert.IsValid<UnknownModelValidator, UnknownModel>(model);
        }

        [Test]
        public void NoUnknown_is_valid_when_set()
        {
            var model = new UnknownModel { Email = EmailAddress.Parse("test@qowaiv.org") };
            FluentValidatorAssert.IsValid<UnknownModelValidator, UnknownModel>(model);
        }

        [TestCase("'Email' must not be unknown.", "en-GB")]
        [TestCase("'Email' mag niet onbekend zijn.", "nl-BE")]
        public void NotUnknown_with_error_when_unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new UnknownModel { Email = EmailAddress.Unknown };

                FluentValidatorAssert.WithErrors<UnknownModelValidator, UnknownModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }

        [Test]
        public void Validate_empty_WithSeverity_has_warning()
        {
            var model = new UnknownWithSeverityModel { Email = EmailAddress.Empty };

            FluentValidatorAssert.IsValid<UnknownWithSeverityModelValidator, UnknownWithSeverityModel>(model,
                ValidationMessage.Warn("'Email' must not be empty or unknown.", "Email")
            );
        }

        [Test]
        public void Validate_unknown_WithSeverity_has_warning()
        {
            var model = new UnknownWithSeverityModel { Email = EmailAddress.Unknown };

            FluentValidatorAssert.IsValid<UnknownWithSeverityModelValidator, UnknownWithSeverityModel>(model,
                ValidationMessage.Warn("'Email' must not be empty or unknown.", "Email")
            );
        }

    }
}
