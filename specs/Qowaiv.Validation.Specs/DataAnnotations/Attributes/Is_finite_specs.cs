using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Is_finite_specs;

public class Is_valid_for
{
    [Test]
    public void Null() => new IsFiniteAttribute().IsValid(null).Should().BeTrue();

    [TestCase(double.MinValue)]
    [TestCase(0.33)]
    [TestCase(0.0)]
    [TestCase(42.0)]
    [TestCase(double.MaxValue)]
    [TestCase(float.MinValue)]
    [TestCase(0.33f)]
    [TestCase(0.0f)]
    [TestCase(42.0f)]
    [TestCase(float.MaxValue)]
    public void finite_floating_points(object value)
        => new IsFiniteAttribute().IsValid(value).Should().BeTrue();
}

public class Is_invalid_for
{
    [TestCase(float.NaN)]
    [TestCase(float.NegativeInfinity)]
    [TestCase(float.PositiveInfinity)]
    [TestCase(double.NaN)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(double.PositiveInfinity)]
    public void not_finite(object value)
        => new IsFiniteAttribute().IsValid(value).Should().BeFalse();

    [TestCase("Hello")]
    [TestCase(42)]
    [TestCase(128L)]
    [TestCase(1024UL)]
    public void non_floating_points(object value)
        => value.Invoking(_ => new IsFiniteAttribute().IsValid(value))
        .Should().Throw<UnsupportedType>();
}
public class With_message
{
    [TestCase("nl", "De waarde van het Total veld is geen eindig getal.")]
    [TestCase("en", "The value of the Total field is not a finite number.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().ValidateWith(new AnnotatedModelValidator<Model>())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "Total"));
    }

    internal class Model
    {
        [IsFinite]
        public double Total { get; set; } = double.NaN;
    }
}

