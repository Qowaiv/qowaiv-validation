using Qowaiv.Financial;
using Qowaiv.Validation.DataAnnotations;

namespace Data_Annotations.Attributes.Multiple_of_specs;

public class Valid_for
{
    [TestCase("45.000", 5.000)]
    [TestCase("45.000", 1.000)]
    [TestCase("45.100", 0.100)]
    [TestCase("45.170", 0.010)]
    [TestCase("45.178", 0.001)]
    public void floats(float value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase("45.000", 5.000)]
    [TestCase("45.000", 1.000)]
    [TestCase("45.100", 0.100)]
    [TestCase("45.170", 0.010)]
    [TestCase("45.178", 0.001)]
    public void doubles(double value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase("45.000", 5.000)]
    [TestCase("45.000", 1.000)]
    [TestCase("45.100", 0.100)]
    [TestCase("45.170", 0.010)]
    [TestCase("45.178", 0.001)]
    public void decimals(decimal value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase("45.000", 5.000)]
    [TestCase("45.000", 1.000)]
    [TestCase("45.100", 0.100)]
    [TestCase("45.170", 0.010)]
    [TestCase("45.178", 0.001)]
    public void amounts(Amount value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase("EUR 45.000", 5.000)]
    [TestCase("EUR 45.000", 1.000)]
    [TestCase("EUR 45.100", 0.100)]
    [TestCase("EUR 45.170", 0.010)]
    [TestCase("EUR 45.178", 0.001)]
    public void money(Money value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase("45.000%", 5.000)]
    [TestCase("45.000%", 1.000)]
    [TestCase("45.100%", 0.100)]
    [TestCase("45.170%", 0.010)]
    [TestCase("45.178%", 0.001)]
    public void percentages(Percentage value, double factor)
       => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase(nameof(NonFloatingPoints))]
    public void non_floating_points(object model)
      => new MultipleOfAttribute(10).IsValid(model).Should().BeTrue();

    static IEnumerable<object?> NonFloatingPoints()
    {
        yield return null;
        yield return string.Empty;
        yield return new[] { 42 };
        yield return new object();
    }
}

public class Initialization
{
    [Test]
    public void has_sufficient_precision([Range(1, 23)]int decimals)
    {
        var factor = (decimal)Math.Pow(10, -decimals);
        factor.Should().Be(Pow(-decimals));
    }

    private static decimal Pow(int decimals) => new(1, 0, 0, false, scale: (byte)-decimals);
}

public class Not_valid_for
{
    [TestCase(float.NaN, 1)]
    [TestCase(float.NegativeInfinity, 1)]
    [TestCase(float.PositiveInfinity, 1)]
    [TestCase(float.MinValue, 1)]
    [TestCase(float.MaxValue, 1)]
    [TestCase("45.500", 1.00)]
    [TestCase("45.120", 0.10)]
    [TestCase("45.178", 0.01)]
    public void floats(float value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

    [TestCase(double.NaN, 1)]
    [TestCase(double.NegativeInfinity, 1)]
    [TestCase(double.PositiveInfinity, 1)]
    [TestCase(double.MinValue, 1)]
    [TestCase(double.MaxValue, 1)]
    [TestCase("45.500", 1.00)]
    [TestCase("45.120", 0.10)]
    [TestCase("45.178", 0.01)]
    public void doubles(double value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

    [TestCase("45.000", 6.00)]
    [TestCase("45.500", 1.00)]
    [TestCase("45.120", 0.10)]
    [TestCase("45.178", 0.01)]
    public void decimals(decimal value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

    [TestCase("45.000", 6.00)]
    [TestCase("45.500", 1.00)]
    [TestCase("45.120", 0.10)]
    [TestCase("45.178", 0.01)]
    public void amounts(Amount value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

    [TestCase("EUR 45.000", 6.00)]
    [TestCase("EUR 45.500", 1.00)]
    [TestCase("EUR 45.120", 0.10)]
    [TestCase("EUR 45.178", 0.01)]
    public void money(Money value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

    [TestCase("45.000%", 6.00)]
    [TestCase("45.500%", 1.00)]
    [TestCase("45.120%", 0.10)]
    [TestCase("45.178%", 0.01)]
    public void percentages(Percentage value, double factor)
       => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "De waarde van veld Total field is geen veelvoud van 0,001.")]
    [TestCase("en", "The value of the Total field is not a multiple of 0.001.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "Total"));
    }

    internal class Model
    {
        [MultipleOf(0.001)]
        public Amount Total { get; set; } = 0.12345.Amount();

        [MultipleOf(0.001)]
        public Amount Sub { get; set; } = 0.123.Amount();
    }
}
