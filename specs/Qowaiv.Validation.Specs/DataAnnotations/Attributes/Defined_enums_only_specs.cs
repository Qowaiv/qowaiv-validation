using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Defined_enums_only_specs;

public class Does_not_allow
{
    public class Generic
    {
        [Test]
        public void validation_on_non_enum_values()
        {
            Func<bool> validate = () => new DefinedOnlyAttribute<Number>().IsValid("value");
            validate.Should().Throw<InvalidCastException>();
        }
    }

    [Obsolete("Will be dropped with next major release.")]
    public class Non_generic
    {
        [Test]
        public void validation_on_non_enum_values()
        {
            Func<bool> validate = () => new DefinedEnumValuesOnlyAttribute().IsValid(1);
            validate.Should().Throw<ArgumentException>();
        }
    }
}
public class Is_valid_for
{
    public class Generic
    {
        [Test]
        public void Null()
            => new DefinedOnlyAttribute<Number>().IsValid(null).Should().BeTrue();

        [Test]
        public void defined_enum_value()
            => new DefinedOnlyAttribute<Number>().IsValid(Number.One).Should().BeTrue();

        [Test]
        public void defined_enum_flag_value()
            => new DefinedOnlyAttribute<Banners>().IsValid(Banners.UnionJack).Should().BeTrue();

        [Test]
        public void defined_mixed_enum_flag_values()
            => new DefinedOnlyAttribute<Banners>().IsValid(Banners.American).Should().BeTrue();

        [Test]
        public void not_defined_mix_of_defined_enum_flag_values()
            => new DefinedOnlyAttribute<Banners>().IsValid(Banners.UnionJack | Banners.StarsAndStripes).Should().BeTrue();

        [TestCase(1)]
        [TestCase(Banners.UnionJack)]
        public void castable_values(object value)
            => new DefinedOnlyAttribute<Number>().IsValid(value).Should().BeTrue();
    }

    [Obsolete("Will be dropped with next major release.")]
    public class Non_generic
    {
        [Test]
        public void Null()
            => new DefinedEnumValuesOnlyAttribute().IsValid(null).Should().BeTrue();

        [Test]
        public void defined_enum_value()
            => new DefinedEnumValuesOnlyAttribute().IsValid(Number.One).Should().BeTrue();

        [Test]
        public void defined_enum_flag_value()
            => new DefinedEnumValuesOnlyAttribute().IsValid(Banners.UnionJack).Should().BeTrue();

        [Test]
        public void defined_mixed_enum_flag_values()
            => new DefinedEnumValuesOnlyAttribute().IsValid(Banners.American).Should().BeTrue();

        [Test]
        public void not_defined_mix_of_defined_enum_flag_values()
            => new DefinedEnumValuesOnlyAttribute().IsValid(Banners.UnionJack | Banners.StarsAndStripes).Should().BeTrue();
    }
}

public class Is_not_valid_for
{
    public class Generic
    {
        [Test]
        public void not_defined_enum_value()
            => new DefinedOnlyAttribute<Number>().IsValid((Number)42).Should().BeFalse();

        [Test]
        public void not_defined_enum_flag_value()
            => new DefinedOnlyAttribute<Number>().IsValid((Banners)42).Should().BeFalse();

        [Test]
        public void not_defined_mix_of_defined_enum_flag_values_when_defined_flag_combinations_are_required()
            => new DefinedOnlyAttribute<Banners>
            {
                OnlyAllowDefinedFlagsCombinations = true,
            }
            .IsValid(Banners.UnionJack | Banners.StarsAndStripes).Should().BeFalse();
    }

    [Obsolete("Will be dropped with next major release.")]
    public class Non_generic
    {
        [Test]
        public void not_defined_enum_value()
            => new DefinedEnumValuesOnlyAttribute().IsValid((Number)42).Should().BeFalse();

        [Test]
        public void not_defined_enum_flag_value()
            => new DefinedEnumValuesOnlyAttribute().IsValid((Banners)42).Should().BeFalse();

        [Test]
        public void not_defined_mix_of_defined_enum_flag_values_when_defined_flag_combinations_are_required()
            => new DefinedEnumValuesOnlyAttribute
            {
                OnlyAllowDefinedFlagsCombinations = true,
            }
            .IsValid(Banners.UnionJack | Banners.StarsAndStripes).Should().BeFalse();
    }
}

public class OnlyAllowDefinedFlagsCombinations
{
    [Test]
    public void is_disabled_by_default()
        => new DefinedOnlyAttribute<Banners>().OnlyAllowDefinedFlagsCombinations.Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "De waarde van het veld Banners is niet toegestaan.")]
    [TestCase("en", "The value of the Banners field is not allowed.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().ShouldBeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "Banners"));
    }
    internal class Model
    {
        [DefinedOnly<Banners>]
        public Banners Banners { get; set; } = (Banners)42;
    }
}

[Flags]
public enum Banners
{
    UnionJack = 1,
    StarsAndStripes = 2,
    MapleLeaf = 4,
    American = StarsAndStripes | MapleLeaf,
}

public enum Number
{
    Zero,
    One,
}

