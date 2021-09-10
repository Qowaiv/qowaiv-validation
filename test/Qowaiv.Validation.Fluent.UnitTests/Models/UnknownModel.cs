using FluentValidation;
using Qowaiv.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Models
{
    public class UnknownModel
    {
        public EmailAddress Email { get; set; }
        public Country Country { get; set; } = Country.NL;
    }
    public class UnknownModelValidator : ModelValidator<UnknownModel>
    {
        public UnknownModelValidator()
        {
            RuleFor(m => m.Email).NotUnknown();
            RuleFor(m => m.Country).NotEmptyOrUnknown();
        }
    }
}
