using Specs.DataAnnotations.Subs;
using System.ComponentModel.DataAnnotations;

namespace Data_annotations.Valdator_specs;

public class Validates_children_when_child_type
{
    [Test]
    public void is_IValidatableObject()
        => new WithIValidatableChild()
        .Should().BeInvalidFor(new AnnotatedModelValidator<WithIValidatableChild>())
        .WithMessage(ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", "Child.Answer"));
    
    [Test]
    public void has_any_validation_attribute()
     => new WithChildTypeDecorated()
        .Should().BeInvalidFor(new AnnotatedModelValidator<WithChildTypeDecorated>())
        .WithMessage(ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", "Child"));

    [Test]
    public void Has_any_property_with_validation_attribute()
        => new WithChildWithAttributes()
        .Should().BeInvalidFor(new AnnotatedModelValidator<WithChildWithAttributes>())
        .WithMessage(ValidationMessage.Error("The Answer field is required.", "Child.Answer"));

    internal class WithIValidatableChild
    {
        public IValidatableChild Child { get; } = new();
    }
    internal class WithChildTypeDecorated
    {
        public ChildTypeDecorated Child { get; } = new();
    }
    internal class WithChildWithAttributes
    {
        public ChildWithAttributes Child { get; } = new();
    }

    internal class IValidatableChild : IValidatableObject
    {
        public int Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Answer != 42) yield return ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", nameof(Answer));
        }
    }

    [MustHaveTheAnswer]
    internal class ChildTypeDecorated
    {
        public int Answer { get; set; }
    }

    internal class ChildWithAttributes
    {
        [Mandatory]
        public int? Answer { get; set; }
    }

    internal sealed class MustHaveTheAnswerAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
            => value is ChildTypeDecorated model
            && model.Answer == 42;
        public override string FormatErrorMessage(string name)
            => "Answer to the Ultimate Question of Life, the Universe, and Everything.";
    }
}

public class Supports
{
    [Test]
    public void depedency_injection()
    {
        var model = new WithDI { Answer = 42 };
        var provider = new ServiceProviderStub
        {
            { typeof(AnswerService), new AnswerService(42) }
        };
        model.Should().BeValidFor(new AnnotatedModelValidator<WithDI>(provider));
    }

    [Test]
    public void models_with_circularity()
    {
        var model = new WithLoop();
        model.Child.Parent = model;
        model.Should().BeInvalidFor(new AnnotatedModelValidator<WithLoop>())
            .WithMessage(ValidationMessage.Error("The Answer field is required.", "Child.Answer"));
    }

    [Test]
    public void models_with_collections_of_children_to_validate()
    {
        var model = new WithChildren
        {
            Records = new[]
            {
                new ChildRecord(),
                new ChildRecord(),
            } 
        };
        model.Should().BeInvalidFor(new AnnotatedModelValidator<WithChildren>()).WithMessages(
            ValidationMessage.Error("The Value field is required.", "Records[0].Value"),
            ValidationMessage.Error("The Value field is required.", "Records[1].Value"));
    }

    internal class WithDI : IValidatableObject
    {
        public int Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = validationContext.GetSevice<AnswerService>();
            if (Answer != service.Answer) yield return ValidationMessage.Error("Wrong answer!", nameof(Answer));
        }
    }

    internal class WithLoop
    {
        public ChildWithLoop Child { get; } = new();
    }
    internal class ChildWithLoop
    {
        public WithLoop Parent { get; set; }
        [Mandatory]
        public int? Answer { get; set; }
    }

    internal class WithChildren
    {
        public ChildRecord[] Records { get; set; }
    }
    internal sealed record ChildRecord
    {
        [Mandatory]
        public int? Value { get; init; }
    }

    internal sealed record AnswerService(int Answer);
}
