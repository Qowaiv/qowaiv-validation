using FluentValidation;

namespace Qowaiv.Validation.Fluent.UnitTests.Models
{
    public class WarningModel
    {
        public string Message { get; set; } = "Some message";
    }

    public class WarningModelValidator : FluentModelValidator<WarningModel>
    {
        public WarningModelValidator()
        {
            RuleFor(m => m.Message).Must(v => false).WithMessage("Test warning.").WithSeverity(Severity.Warning);
            RuleFor(m => m.Message).Must(v => false).WithMessage("Nice that your validated this.").WithSeverity(Severity.Info);
        }
    }
}
