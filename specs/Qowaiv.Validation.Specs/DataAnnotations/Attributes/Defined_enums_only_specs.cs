using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Defined_enums_only_specs;

public class Does_not_allow
{
    [Test]
    public void validation_on_non_enum_values()
    {
        Func<bool> validate = () => new DefinedEnumValuesOnlyAttribute().IsValid(1);
        validate.Should().Throw<ArgumentException>();
    }
}
public class Is_valid_for
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

public class Is_not_valid_for
{
    [Test]
    public void not_defined_enum_value()
        => new DefinedEnumValuesOnlyAttribute().IsValid((Number)666).Should().BeFalse();

    [Test]
    public void not_defined_enum_flag_value()
        => new DefinedEnumValuesOnlyAttribute().IsValid((Banners)666).Should().BeFalse();

    [Test]
    public void not_defined_mix_of_defined_enum_flag_values_when_defined_flag_combinations_are_required()
        => new DefinedEnumValuesOnlyAttribute
        {
            OnlyAllowDefinedFlagsCombinations = true,
        }
        .IsValid(Banners.UnionJack | Banners.StarsAndStripes).Should().BeFalse();
}

public class OnlyAllowDefinedFlagsCombinations
{
    [Test]
    public void is_disabled_by_default()
        => new DefinedEnumValuesOnlyAttribute().OnlyAllowDefinedFlagsCombinations.Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "De waarde van het veld Banners is niet toegestaan.")]
    [TestCase("en", "The value of the Banners field is not allowed.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "Banners"));
    }
    internal class Model
    {
        [DefinedEnumValuesOnly]
        public Banners Banners { get; set; } = (Banners)666;
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

