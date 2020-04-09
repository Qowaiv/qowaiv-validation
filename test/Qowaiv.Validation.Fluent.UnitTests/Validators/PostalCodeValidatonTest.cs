using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class PostalCodeValidatonTest
    {
        [Test]
        public void Empty_IsValidFor_Empty()
        {
            var model = new PostalCodeModel { PostalCode = PostalCode.Empty, Country = Country.Empty };
            FluentValidatorAssert.IsValid<PostalCodeModelValidator, PostalCodeModel>(model);
        }

        [Test]
        public void Empty_IsValidFor_NL()
        {
            var model = new PostalCodeModel { PostalCode = PostalCode.Empty, Country = Country.NL };
            FluentValidatorAssert.IsValid<PostalCodeModelValidator, PostalCodeModel>(model);
        }

        [Test]
        public void PostalCode_IsValidFor_Empty()
        {
            var model = new PostalCodeModel { PostalCode = PostalCode.Parse("12345"), Country = Country.Empty };
            FluentValidatorAssert.IsValid<PostalCodeModelValidator, PostalCodeModel>(model);
        }

        [Test]
        public void PostalCode_IsValidFor_NL()
        {
            var model = new PostalCodeModel { PostalCode = PostalCode.Parse("2624DP"), Country = Country.NL };
            FluentValidatorAssert.IsValid<PostalCodeModelValidator, PostalCodeModel>(model);
        }

        [TestCase("'Postal Code' 12345 is not valid for Netherlands.", "en-GB")]
        [TestCase("'Postal Code' 12345 is niet geldig voor Nederland.", "nl-BE")]
        public void PostalCode_NotValidFor_NL(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                var model = new PostalCodeModel { Country = Country.NL, PostalCode = PostalCode.Parse("12345") };
                FluentValidatorAssert.WithErrors<PostalCodeModelValidator, PostalCodeModel>(model,
                    ValidationMessage.Error(message, "PostalCode")
                );
            }
        }
    }
}
