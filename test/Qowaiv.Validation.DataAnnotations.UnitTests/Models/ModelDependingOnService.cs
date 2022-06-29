using System.ComponentModel.DataAnnotations;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

internal class ModelDependingOnService : IValidatableObject
{
    public int Answer { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var service = validationContext.GetSevice<AnswerService>();
        if (Answer != service.Answer)
        {
            yield return ValidationMessage.Error("Wrong answer!", nameof(Answer));
        }
    }

    internal sealed record AnswerService(int Answer);
}
