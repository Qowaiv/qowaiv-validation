using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class RequiredValidatonTest
    {
        [Test]
        public void Known_IsValid()
        {
            var model = new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.NL };
            FluentValidatorAssert.IsValid<RequiredModelValidator, RequiredModel>(model);
        }

        [Test]
        public void Unknown_IsValid()
        {
            var model = new RequiredModel { Email = EmailAddress.Parse("test@qowaiv.org"), Country = Country.Unknown };
            FluentValidatorAssert.IsValid<RequiredModelValidator, RequiredModel>(model);
        }

        [TestCase("'Email' is required.", "en-GB")]
        [TestCase("'Email' is verplicht.", "nl-BE")]
        public void Empty(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new RequiredModel { Email = EmailAddress.Empty, Country = Country.NL };

                FluentValidatorAssert.WithErrors<RequiredModelValidator, RequiredModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }

        [TestCase("'Email' is required.", "en-GB")]
        [TestCase("'Email' is verplicht.", "nl-BE")]
        public void Unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new RequiredModel { Email = EmailAddress.Unknown, Country = Country.NL };

                FluentValidatorAssert.WithErrors<RequiredModelValidator, RequiredModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }
    }
}
