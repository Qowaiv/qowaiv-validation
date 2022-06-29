namespace Data_annotations.Attributes.Any_specs;

public class Is_valid_for
{
    [Test]
    public void Not_empty_collection()
       => new AnyAttribute().IsValid(new[] { 42 }).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void Null()
        => new AnyAttribute().IsValid(null).Should().BeFalse();

    [Test]
    public void Empty_collection()
       => new AnyAttribute().IsValid(Array.Empty<int>()).Should().BeFalse();
}
