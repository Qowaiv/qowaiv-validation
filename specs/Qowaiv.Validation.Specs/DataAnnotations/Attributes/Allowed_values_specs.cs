namespace Data_annotations.Attributes.Allowed_values_specs;

public class Is_valid_for
{
    [Test]
    public void Null()
        => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(null).Should().BeTrue();

    [Test]
    public void value_in_allowed_values()
        => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(Country.GB).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void value_not_in_allowed_values()
       => new AllowedValuesAttribute("DE", "FR", "GB").IsValid(Country.TR).Should().BeFalse();
}
