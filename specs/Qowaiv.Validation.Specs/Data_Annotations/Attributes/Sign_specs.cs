using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Sign_specs;

public class Positive
{
    public class Is_valid_for
    {
        [Test]
        public void Null() => new PositiveAttribute().IsValid(null).Should().BeTrue();

        [TestCase(0.001)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        [TestCase(0.1f)]
        [TestCase(42.0)]
        [TestCase(double.MaxValue)]
        [TestCase((sbyte)1)]
        [TestCase((short)1)]
        [TestCase((short)32767)]
        [TestCase((long)1)]
        public void positive_numbers(object value)
            => new PositiveAttribute().IsValid(value).Should().BeTrue();
    }

    public class Is_invalid_for
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-42)]
        [TestCase(int.MinValue)]
        [TestCase(0.0f)]
        [TestCase(-0.1f)]
        [TestCase(-42.0)]
        [TestCase((sbyte)-1)]
        [TestCase((short)-1)]
        [TestCase((long)-1)]
        public void zero_or_negative_numbers(object value)
            => new PositiveAttribute().IsValid(value).Should().BeFalse();

        [TestCase("Hello")]
        [TestCase(true)]
        public void non_numeric_types(object value)
            => value.Invoking(_ => new PositiveAttribute().IsValid(value))
            .Should().Throw<UnsupportedType>();
    }

    public class With_message
    {
        [TestCase("nl", "De waarde van het Quantity veld moet positief zijn.")]
        [TestCase("en", "The value of the Quantity field must be positive.")]
        public void culture_dependent(CultureInfo culture, string message)
        {
            using var _ = culture.Scoped();
            new Model().ValidateWith(new AnnotatedModelValidator<Model>())
                .Should().BeInvalid()
                .WithMessage(ValidationMessage.Error(message, "Quantity"));
        }

        internal class Model
        {
            [Positive]
            public int Quantity { get; set; } = 0;
        }
    }
}

public class NotPositive
{
    public class Is_valid_for
    {
        [Test]
        public void Null() => new NotPositiveAttribute().IsValid(null).Should().BeTrue();

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-42)]
        [TestCase(int.MinValue)]
        [TestCase(0.0f)]
        [TestCase(-0.1f)]
        [TestCase(-42.0)]
        [TestCase((sbyte)-1)]
        [TestCase((sbyte)0)]
        [TestCase((short)-1)]
        [TestCase((short)0)]
        [TestCase((long)-1)]
        [TestCase((long)0)]
        public void zero_or_negative_numbers(object value)
            => new NotPositiveAttribute().IsValid(value).Should().BeTrue();
    }

    public class Is_invalid_for
    {
        [TestCase(0.001)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        [TestCase(0.1f)]
        [TestCase(42.0)]
        [TestCase(double.MaxValue)]
        [TestCase((sbyte)1)]
        [TestCase((short)1)]
        [TestCase((short)32767)]
        [TestCase((long)1)]
        public void positive_numbers(object value)
            => new NotPositiveAttribute().IsValid(value).Should().BeFalse();

        [TestCase("Hello")]
        [TestCase(true)]
        public void non_numeric_types(object value)
            => value.Invoking(_ => new NotPositiveAttribute().IsValid(value))
            .Should().Throw<UnsupportedType>();
    }

    public class With_message
    {
        [TestCase("nl", "De waarde van het Adjustment veld mag niet positief zijn.")]
        [TestCase("en", "The value of the Adjustment field must not be positive.")]
        public void culture_dependent(CultureInfo culture, string message)
        {
            using var _ = culture.Scoped();
            new Model().ValidateWith(new AnnotatedModelValidator<Model>())
                .Should().BeInvalid()
                .WithMessage(ValidationMessage.Error(message, "Adjustment"));
        }

        internal class Model
        {
            [NotPositive]
            public int Adjustment { get; set; } = 1;
        }
    }
}

public class Negative
{
    public class Is_valid_for
    {
        [Test]
        public void Null() => new NegativeAttribute().IsValid(null).Should().BeTrue();

        [TestCase(-0.001)]
        [TestCase(-1)]
        [TestCase(-42)]
        [TestCase(int.MinValue)]
        [TestCase(-0.1f)]
        [TestCase(-42.0)]
        [TestCase(-double.MaxValue)]
        [TestCase((sbyte)-1)]
        [TestCase((sbyte)-128)]
        [TestCase((short)-1)]
        [TestCase((short)-32768)]
        [TestCase((long)-1)]
        public void negative_numbers(object value)
            => new NegativeAttribute().IsValid(value).Should().BeTrue();
    }

    public class Is_invalid_for
    {
        [TestCase(0)]
        [TestCase(0.001)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(42.0)]
        [TestCase(double.MaxValue)]
        [TestCase((sbyte)0)]
        [TestCase((sbyte)1)]
        [TestCase((short)0)]
        [TestCase((short)1)]
        [TestCase((long)0)]
        [TestCase((long)1)]
        public void zero_or_positive_numbers(object value)
            => new NegativeAttribute().IsValid(value).Should().BeFalse();

        [TestCase("Hello")]
        [TestCase(true)]
        public void non_numeric_types(object value)
            => value.Invoking(_ => new NegativeAttribute().IsValid(value))
            .Should().Throw<UnsupportedType>();
    }

    public class With_message
    {
        [TestCase("nl", "De waarde van het Temperature veld moet negatief zijn.")]
        [TestCase("en", "The value of the Temperature field must be negative.")]
        public void culture_dependent(CultureInfo culture, string message)
        {
            using var _ = culture.Scoped();
            new Model().ValidateWith(new AnnotatedModelValidator<Model>())
                .Should().BeInvalid()
                .WithMessage(ValidationMessage.Error(message, "Temperature"));
        }

        internal class Model
        {
            [Negative]
            public int Temperature { get; set; } = 0;
        }
    }
}

public class NotNegative
{
    public class Is_valid_for
    {
        [Test]
        public void Null() => new NotNegativeAttribute().IsValid(null).Should().BeTrue();

        [TestCase(0)]
        [TestCase(0.001)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(42.0)]
        [TestCase(double.MaxValue)]
        [TestCase((sbyte)0)]
        [TestCase((sbyte)1)]
        [TestCase((short)0)]
        [TestCase((short)1)]
        [TestCase((short)32767)]
        [TestCase((long)0)]
        [TestCase((long)1)]
        public void zero_or_positive_numbers(object value)
            => new NotNegativeAttribute().IsValid(value).Should().BeTrue();
    }

    public class Is_invalid_for
    {
        [TestCase(-0.001)]
        [TestCase(-1)]
        [TestCase(-42)]
        [TestCase(int.MinValue)]
        [TestCase(-0.1f)]
        [TestCase(-42.0)]
        [TestCase(-double.MaxValue)]
        [TestCase((sbyte)-1)]
        [TestCase((sbyte)-128)]
        [TestCase((short)-1)]
        [TestCase((short)-32768)]
        [TestCase((long)-1)]
        public void negative_numbers(object value)
            => new NotNegativeAttribute().IsValid(value).Should().BeFalse();

        [TestCase("Hello")]
        [TestCase(true)]
        public void non_numeric_types(object value)
            => value.Invoking(_ => new NotNegativeAttribute().IsValid(value))
            .Should().Throw<UnsupportedType>();
    }

    public class With_message
    {
        [TestCase("nl", "De waarde van het Count veld mag niet negatief zijn.")]
        [TestCase("en", "The value of the Count field must not be negative.")]
        public void culture_dependent(CultureInfo culture, string message)
        {
            using var _ = culture.Scoped();
            new Model().ValidateWith(new AnnotatedModelValidator<Model>())
                .Should().BeInvalid()
                .WithMessage(ValidationMessage.Error(message, "Count"));
        }

        internal class Model
        {
            [NotNegative]
            public int Count { get; set; } = -1;
        }
    }
}
