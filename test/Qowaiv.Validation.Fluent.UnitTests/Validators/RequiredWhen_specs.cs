using FluentValidation;
using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace RequiredWhen_specs
{
    public class Fails
    {
        [Test]
        public void When_condition_is_met_and_property_is_empty()
        {
            using (new CultureInfo("en-UK").Scoped()) {
                var model = new ConditionalRequired(true, Gender.Empty);
                FluentValidatorAssert.WithErrors<ConditionalNotEmptyValidator, ConditionalRequired>(
                    model,
                    ValidationMessage.Error("'Value' is required.", "Value")
                ); 
            }
        }
    }

    public class IsValid
    {
        [Test]
        public void When_condition_is_met_and_property_is_not_empty()
        {
            var model = new ConditionalRequired(true, Gender.Female);
            FluentValidatorAssert.IsValid<ConditionalNotEmptyValidator, ConditionalRequired>(model);
        }

        [Test]
        public void When_condition_is_met_and_property_is_unknown()
        {
            var model = new ConditionalRequired(true, Gender.Female);
            FluentValidatorAssert.IsValid<ConditionalNotEmptyOrUnknownValidator, ConditionalRequired>(model);
        }

        [Test]
        public void When_condition_is_not_met()
        {
            var model = new ConditionalRequired(false, Gender.Empty);
            FluentValidatorAssert.IsValid<ConditionalNotEmptyValidator, ConditionalRequired>(model);
        }
    }

    internal class ConditionalRequired
    {
        public ConditionalRequired(bool isTrue, Gender value)
        {
            IsTrue = isTrue;
            Value = value;
        }

        public bool IsTrue { get; set; }
        public Gender Value { get; set; }
    }
    internal class ConditionalNotEmptyValidator : AbstractValidator<ConditionalRequired>
    {
        public ConditionalNotEmptyValidator()
        {
            RuleFor(m => m.Value).RequiredWhen(m => m.IsTrue);
        }
    }

    internal class ConditionalNotEmptyOrUnknownValidator : AbstractValidator<ConditionalRequired>
    {
        public ConditionalNotEmptyOrUnknownValidator()
        {
            RuleFor(m => m.Value).RequiredWhen(m => m.IsTrue, allowUnknown: true);
        }
    }
}
