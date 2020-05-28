using FluentValidation;

namespace Qowaiv.Validation.Fluent.UnitTests.Models
{
    public class UnknownWithServerityModel
    {
        public EmailAddress Email { get; set; }
    }
    public class UnknownWithServerityModelValidator : AbstractValidator<UnknownWithServerityModel>
    {
        public UnknownWithServerityModelValidator()
        {
            RuleFor(m => m.Email)
                .NotEmptyOrUnknown()
                .WithSeverity(Severity.Warning);
        }
    }
}
