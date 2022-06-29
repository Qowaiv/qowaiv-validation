using System.ComponentModel.DataAnnotations;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

internal class ModelWithIValidatableObjectChild
{
    public ChildModel Child { get; set; } = new ChildModel();

    internal class ChildModel : IValidatableObject
    {
        public int Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Answer != 42) yield return ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", nameof(Answer));
        }
    }
}
