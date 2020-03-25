using FluentValidation;

namespace Qowaiv.Validation.Fluent.UnitTests.Models
{
    public class UnknownModel
    {
        public EmailAddress Email { get; set; }
    }
    public class UnknownModelValidator : AbstractValidator<UnknownModel>
    {
        public UnknownModelValidator()
        {
            RuleFor(m => m.Email).NotEmptyOrUnknown();
        }
    }
}
