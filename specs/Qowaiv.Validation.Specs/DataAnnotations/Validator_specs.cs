using Specs.DataAnnotations.Subs;
using System.ComponentModel.DataAnnotations;

namespace Data_annotations.Valdator_specs;

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

    internal class WithDI : IValidatableObject
    {
        public int Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = validationContext.GetSevice<AnswerService>();
            if (Answer != service.Answer) yield return ValidationMessage.Error("Wrong answer!", nameof(Answer));
        }
    }
    internal sealed record AnswerService(int Answer);
}
