using FluentValidation;

namespace Qowaiv.Validation.Fluent.UnitTests.Models
{
    public class UnknownWithSeverityModel
    {
        public EmailAddress Email { get; set; }
    }
    public class UnknownWithSeverityModelValidator : AbstractValidator<UnknownWithSeverityModel>
    {
        public UnknownWithSeverityModelValidator()
        {
            RuleFor(m => m.Email)
                .NotEmptyOrUnknown()
                .WithSeverity(Severity.Warning);
        }
    }
}
