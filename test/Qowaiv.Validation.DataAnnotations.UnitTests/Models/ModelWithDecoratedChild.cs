using System.ComponentModel.DataAnnotations;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

internal class ModelWithDecoratedChild
{
    public ChildModel Child { get; set; } = new ChildModel();

    [MustHaveTheAnswer]
    internal class ChildModel
    {
        public int Answer { get; set; }
    }

    internal sealed class MustHaveTheAnswerAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
            => value is ChildModel model
            && model.Answer == 42;
        public override string FormatErrorMessage(string name)
            => "Answer to the Ultimate Question of Life, the Universe, and Everything.";
    }
}
