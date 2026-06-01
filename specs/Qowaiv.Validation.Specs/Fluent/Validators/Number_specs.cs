using Qowaiv.Validation.Fluent;
using Specs.Fluent.Models;
using Test_tools.Result_Should_specs;

namespace Fluent_validation.Number_specs;

public class IsPositive
{
    [Test]
    public void Valid_for_null() => new ZeroModel
    {
        NullableNumber = null,
        Number = 42,
    }
    .ValidateWith(new IsPositiveValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(double.Epsilon)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(1.0)]
    [TestCase(42.0)]
    public void Valid_for_positive(double value) => new ZeroModel
    {
        NullableNumber = value,
        Number = value,
    }
    .ValidateWith(new IsPositiveValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(double.NaN)]
    [TestCase(0)]
    [TestCase(-1.0)]
    [TestCase(-42.0)]
    [TestCase(double.NegativeInfinity)]
    public void Invalid_for(double value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new ZeroModel
        {
            NullableNumber = value,
            Number = value,
        }
        .ValidateWith(new IsPositiveValidator())
        .Should().BeInvalid().WithMessages(
            ValidationMessage.Error("'Nullable Number' must be positive.", "NullableNumber"),
            ValidationMessage.Error("'Number' must be positive.", "Number"));
    }

    [TestCase("'Number' must be positive.", "en-GB")]
    [TestCase("'Number' moet positief zijn.", "nl-NL")]
    public void Invalid_with_message(string message, CultureInfo culture) {
        using var _ = culture.Scoped();

        new ZeroModel
        {
            NullableNumber = 42,
            Number = -42,
        }
        .ValidateWith(new IsPositiveValidator())
        .Should().BeInvalid().WithMessage(
            ValidationMessage.Error(message, "Number")); 
    }
}

public class IsNegative
{
    [Test]
    public void Valid_for_null() => new ZeroModel
    {
        NullableNumber = null,
        Number = -42,
    }
    .ValidateWith(new IsNegativeValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(-double.Epsilon)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(-1.0)]
    [TestCase(-42.0)]
    public void Valid_for_negative(double value) => new ZeroModel
    {
        NullableNumber = value,
        Number = value,
    }
    .ValidateWith(new IsNegativeValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(double.NaN)]
    [TestCase(0)]
    [TestCase(1.0)]
    [TestCase(42.0)]
    [TestCase(double.PositiveInfinity)]
    public void Invalid_for(double value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new ZeroModel
        {
            NullableNumber = value,
            Number = value,
        }
        .ValidateWith(new IsNegativeValidator())
        .Should().BeInvalid().WithMessages(
            ValidationMessage.Error("'Nullable Number' must be negative.", "NullableNumber"),
            ValidationMessage.Error("'Number' must be negative.", "Number"));
    }

    [TestCase("'Number' must be negative.", "en-GB")]
    [TestCase("'Number' moet negatief zijn.", "nl-NL")]
    public void Invalid_with_message(string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new ZeroModel
        {
            Number = 42,
            NullableNumber = -42,
        }
        .ValidateWith(new IsNegativeValidator())
        .Should().BeInvalid().WithMessage(
            ValidationMessage.Error(message, "Number"));
    }
}

public class IsNotPositive
{
    [Test]
    public void Valid_for_null() => new ZeroModel
    {
        NullableNumber = null,
        Number = -42,
    }
    .ValidateWith(new IsNotPositiveValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(0)]
    [TestCase(-double.Epsilon)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(-1.0)]
    [TestCase(-42.0)]
    public void Valid_for_not_positive(double value) => new ZeroModel
    {
        NullableNumber = value,
        Number = value,
    }
    .ValidateWith(new IsNotPositiveValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(double.NaN)]
    [TestCase(double.Epsilon)]
    [TestCase(+1.0)]
    [TestCase(+42.0)]
    [TestCase(double.PositiveInfinity)]
    public void Invalid_for(double value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new ZeroModel
        {
            NullableNumber = value,
            Number = value,
        }
        .ValidateWith(new IsNotPositiveValidator())
        .Should().BeInvalid().WithMessages(
            ValidationMessage.Error("'Nullable Number' must not be positive.", "NullableNumber"),
            ValidationMessage.Error("'Number' must not be positive.", "Number"));
    }

    [TestCase("'Number' must not be positive.", "en-GB")]
    [TestCase("'Number' mag niet positief zijn.", "nl-NL")]
    public void Invalid_with_message(string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new ZeroModel
        {
            NullableNumber = -42,
            Number = +42,
        }
        .ValidateWith(new IsNotPositiveValidator())
        .Should().BeInvalid().WithMessage(
            ValidationMessage.Error(message, "Number"));
    }
}

public class IsNotNegative
{
    [Test]
    public void Valid_for_null() => new ZeroModel
    {
        NullableNumber = null,
        Number = +42,
    }
    .ValidateWith(new IsNotNegativeValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(0)]
    [TestCase(double.Epsilon)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(+1.0)]
    [TestCase(+42.0)]
    public void Valid_for_negative(double value) => new ZeroModel
    {
        NullableNumber = value,
        Number = value,
    }
    .ValidateWith(new IsNotNegativeValidator())
    .Should().BeValid().WithoutMessages();

    [TestCase(double.NaN)]
    [TestCase(-1.0)]
    [TestCase(-42.0)]
    [TestCase(double.NegativeInfinity)]
    public void Invalid_for(double value)
    {
        using var _ = CultureInfo.InvariantCulture.Scoped();

        new ZeroModel
        {
            NullableNumber = value,
            Number = value,
        }
        .ValidateWith(new IsNotNegativeValidator())
        .Should().BeInvalid().WithMessages(
            ValidationMessage.Error("'Nullable Number' must not be negative.", "NullableNumber"),
            ValidationMessage.Error("'Number' must not be negative.", "Number"));
    }

    [TestCase("'Number' must not be negative.", "en-GB")]
    [TestCase("'Number' mag niet negatief zijn.", "nl-NL")]
    public void Invalid_with_message(string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new ZeroModel
        {
            Number = -42,
            NullableNumber = +42,
        }
        .ValidateWith(new IsNotNegativeValidator())
        .Should().BeInvalid().WithMessage(
            ValidationMessage.Error(message, "Number"));
    }
}
