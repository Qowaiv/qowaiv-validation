using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class EmailAddressValidationTest
    {
        [Test]
        public void NoIPBase_IsValid()
        {
            var model = new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("test@qowaiv.org") };
            FluentValidatorAssert.IsValid<NoIPBasedEmailAddressModelValidator, NoIPBasedEmailAddressModel>(model);
        }

        [TestCase("'Email' has a IP address based domain.", "en-GB")]
        [TestCase("'Email' heeft een IP-adres als domein.", "nl-BE")]
        public void IPBased(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("qowaiv@172.16.254.1") };
                FluentValidatorAssert.WithErrors<NoIPBasedEmailAddressModelValidator, NoIPBasedEmailAddressModel>(model,
                    ValidationMessage.Error(message, "Email")
                );
            }
        }
    }
}
