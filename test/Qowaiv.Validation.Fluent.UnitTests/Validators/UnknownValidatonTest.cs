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
        public void Known_IsValid()
        {
            var model = new NotUnknownModel { Email = EmailAddress.Parse("test@qowaiv.org") };
            FluentValidatorAssert.IsValid<NotUnknownModelValidator, NotUnknownModel>(model);
        }

        [TestCase("'Email' must not be empty.", "en-GB")]
        [TestCase("'Email' mag niet leeg zijn.", "nl-BE")]
        public void Empty(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new NotUnknownModel { Email = EmailAddress.Empty };

                FluentValidatorAssert.WithErrors<NotUnknownModelValidator, NotUnknownModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }

        [TestCase("'Email' must not be unknown.", "en-GB")]
        [TestCase("'Email' mag niet onbekend zijn.", "nl-BE")]
        public void Unknown(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new NotUnknownModel { Email = EmailAddress.Unknown };

                FluentValidatorAssert.WithErrors<NotUnknownModelValidator, NotUnknownModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }
    }
}
