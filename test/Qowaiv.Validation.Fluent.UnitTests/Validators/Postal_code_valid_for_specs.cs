using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent;
using System.Globalization;
using FluentValidation;
using FluentAssertions;

namespace Validation.Postal_code_valid_for_specs
{
    public class Valid_for
    {
        [Test]
        public void postal_code_for_countr()
            => new PostalCodeModel
            {
                PostalCode = PostalCode.Parse("2628ZD"),
                Country = Country.NL,
            }.Should().BeValidFor(new PostalCodeModelValidator());
    }

    public class Not_invalid_for
    {
        [Test]
        public void Empty_postal_code_for_empty_country()
            => new PostalCodeModel
            {
                PostalCode = PostalCode.Empty,
                Country = Country.Empty,
            }.Should().BeValidFor(new PostalCodeModelValidator());

        [Test]
        public void Empty_postal_code_for_country()
             => new PostalCodeModel
             {
                 PostalCode = PostalCode.Empty,
                 Country = Country.NL,
             }.Should().BeValidFor(new PostalCodeModelValidator());

        [Test]
        public void Postal_code_for_empty_country()
             => new PostalCodeModel
             {
                 PostalCode = PostalCode.Parse("12345"),
                 Country = Country.Empty,
             }.Should().BeValidFor(new PostalCodeModelValidator());
    }
    public class Invalid_for
    { 
        [TestCase("'Postal Code' 12345 is not valid for Netherlands.", "en-GB")]
        [TestCase("'Postal Code' 12345 is niet geldig voor Nederland.", "nl-BE")]
        public void postal_code_not_comform_country(string message, CultureInfo culture)
        {
            using (new CultureInfoScope(culture))
            {
                new PostalCodeModel
                {
                    Country = Country.NL,
                    PostalCode = PostalCode.Parse("12345")
                }.Should().BeInvalidFor(new PostalCodeModelValidator())
                .WithMessage(ValidationMessage.Error(message, "PostalCode"));
            }
        }
    }

    internal class PostalCodeModel
    {
        public PostalCode PostalCode { get; set; }
        public Country Country { get; set; }
    }

    internal class PostalCodeModelValidator : ModelValidator<PostalCodeModel>
    {
        public PostalCodeModelValidator()
            =>  RuleFor(m => m.PostalCode).ValidFor(m => m.Country);
    }
}
