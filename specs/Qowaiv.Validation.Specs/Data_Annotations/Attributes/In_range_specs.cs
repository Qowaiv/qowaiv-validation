using Qowaiv.Financial;
using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.DataAnnotations.Attributes;

namespace Data_Annotations.Attributes.In_range_specs;

public class Is_valid_for
{
    [Test]
    public void Null() => new InRangeAttribute<int>(1, 3).IsValid(null).Should().BeTrue();

    [Test]
    public void values_in_range([Range(12, 42)]int value)
        => new InRangeAttribute<int>(12, 42).IsValid(value).Should().BeTrue();
}

public class Is_invalid_for
{
    [Test]
    public void value_below_range()
        => new InRangeAttribute<int>(12, 42).IsValid(11).Should().BeFalse();

    [Test]
    public void value_above_range()
        => new InRangeAttribute<int>(12, 42).IsValid(43).Should().BeFalse();

    [Test]
    public void value_of_different_type()
       => new InRangeAttribute<int>(12, 42).IsValid(20.0).Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "De waarde van het Total veld moet een waarde hebben tussen 0 en 1000.")]
    [TestCase("en", "The field Total must be between 0 and 1000.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().ValidateWith(new AnnotatedModelValidator<Model>())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "Total"));
    }

    internal class Model
    {
        [InRange<Amount>(0.00, 1000.00)]
        public Amount Total { get; set; } = 10_000.Amount();
    }
}
