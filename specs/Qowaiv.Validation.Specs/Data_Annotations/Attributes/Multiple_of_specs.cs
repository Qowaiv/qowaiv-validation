using Qowaiv.Financial;
using Qowaiv.Validation.DataAnnotations;

namespace Data_Annotations.Attributes.Multiple_of_specs;

public class Valid_for
{
    [Test]
    public void Null() => new MultipleOfAttribute(0.1).IsValid(null).Should().BeTrue();

    [TestCase(45, 5.000)]
    [TestCase(45, 1.000)]
    [TestCase(45, 0.100)]
    [TestCase(45, 0.010)]
    [TestCase(45, 0.001)]
    public void shorts(short value, double factor)
      => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase(45, 5.000)]
    [TestCase(45, 1.000)]
    [TestCase(45, 0.100)]
    [TestCase(45, 0.010)]
    [TestCase(45, 0.001)]
    public void ints(int value, double factor)
       => new MultipleOfAttribute(factor).IsValid(value).Should().BeTrue();

    [TestCase(45, 5.000)]
    [TestCase(45, 1.000)]
    [TestCase(45, 0.100)]
    [TestCase(45, 0.010)]
    [TestCase(45, 0.001)]
    public void longs(long value, double factor)
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
}

public class Not_valid_for
{
    [TestCase(45, 06)]
    [TestCase(45, 10)]
    public void integers(long value, double factor)
        => new MultipleOfAttribute(factor).IsValid(value).Should().BeFalse();

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

    [TestCaseSource(nameof(NotSupportedTypes))]
    public void not_supported_types(object model)
        => new MultipleOfAttribute(10).IsValid(model).Should().BeFalse();

    static IEnumerable<object?> NotSupportedTypes()
    {
        yield return true;
        yield return "Hello, World!";
        yield return 'C';
        yield return new DateTime(2017, 06, 11, 06, 15, 00, DateTimeKind.Local);
        yield return DBNull.Value;
    }
}

public class Initialization
{
    [Test]
    public void has_sufficient_precision([Range(1, 23)] int decimals)
    {
        var factor = (decimal)Math.Pow(10, -decimals);
        factor.Should().Be(Pow(-decimals));
    }

    private static decimal Pow(int decimals) => new(1, 0, 0, false, scale: (byte)-decimals);
}

public class With_message
{
    [TestCase("nl", "De waarde van veld Total is geen veelvoud van 0,001.")]
    [TestCase("en", "The value of the Total field is not a multiple of 0.001.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().ValidateWith(new AnnotatedModelValidator<Model>())
            .Should().BeInvalid()
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
