namespace Data_annotations.Attributes.Forbidden_values_specs;

public class Is_valid_for
{
    [Test]
    public void Null()
        => new ForbiddenValuesAttribute("DE", "FR", "GB").IsValid(null).Should().BeTrue();

    [Test]
    public void value_not_in_allowed_values()
        => new ForbiddenValuesAttribute("DE", "FR", "GB").IsValid(Country.TR).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void value_in_allowed_values()
       => new ForbiddenValuesAttribute("DE", "FR", "GB").IsValid(Country.GB).Should().BeFalse();
}
