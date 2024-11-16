using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;

namespace Fluent_validation.Is_finite_specs;

public class Valid_for
{
    [TestCase(double.MinValue)]
    [TestCase(0.33)]
    [TestCase(0)]
    [TestCase(42)]
    [TestCase(double.MaxValue)]
    public void finite_doubles(double value)
        => new FloatingPointsModel { Double = value }
        .Should().BeValidFor(new FloatingPointsValidator());

    [TestCase(double.MinValue)]
    [TestCase(0.33f)]
    [TestCase(0)]
    [TestCase(42)]
    [TestCase(double.MaxValue)]
    public void finite_nullable_doubles(double value)
       => new FloatingPointsModel { NullableDouble = value }
       .Should().BeValidFor(new FloatingPointsValidator());

    [TestCase(float.MinValue)]
    [TestCase(0.33f)]
    [TestCase(0)]
    [TestCase(42)]
    [TestCase(float.MaxValue)]
    public void finite_floats(float value)
        => new FloatingPointsModel { Float = value }
        .Should().BeValidFor(new FloatingPointsValidator());

    [TestCase(float.MinValue)]
    [TestCase(0.33f)]
    [TestCase(0)]
    [TestCase(42)]
    [TestCase(float.MaxValue)]
    public void finite_nullable_floats(float value)
       => new FloatingPointsModel { NullableFloat = value }
       .Should().BeValidFor(new FloatingPointsValidator());

    [Test]
    public void not_set_nullable_double()
        => new FloatingPointsModel { NullableFloat = null }
        .Should().BeValidFor(new FloatingPointsValidator());

    [Test]
    public void not_set_nullable_float()
       => new FloatingPointsModel { NullableFloat = null }
       .Should().BeValidFor(new FloatingPointsValidator());
}

public class Invalid_for
{
    [TestCase(double.NaN)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(double.PositiveInfinity)]
    public void not_finite_doubles(double value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new FloatingPointsModel { Double = value, NullableDouble = value }
            .Should().BeInvalidFor(new FloatingPointsValidator())
            .WithMessages(
                ValidationMessage.Error("'Double' must be a finite number.", "Double"),
                ValidationMessage.Error("'Nullable Double' must be a finite number.", "NullableDouble"));
    }

    [TestCase(float.NaN)]
    [TestCase(float.NegativeInfinity)]
    [TestCase(float.PositiveInfinity)]
    public void not_finite_floats(float value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new FloatingPointsModel { Float = value, NullableFloat = value }
            .Should().BeInvalidFor(new FloatingPointsValidator())
            .WithMessages(
                ValidationMessage.Error("'Float' must be a finite number.", "Float"),
                ValidationMessage.Error("'Nullable Float' must be a finite number.", "NullableFloat"));
    }

    [TestCase("nl", "'Double' moet een eindig getal zijn.")]
    [TestCase("en", "'Double' must be a finite number.")]
    public void with_localized_message(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();

        new FloatingPointsModel { Double = double.NaN }
            .Should().BeInvalidFor(new FloatingPointsValidator())
            .WithMessage(
                ValidationMessage.Error(message, "Double"));
    }
}
