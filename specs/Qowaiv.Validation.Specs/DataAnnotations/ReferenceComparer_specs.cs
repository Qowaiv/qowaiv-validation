using Specs.DataAnnotations.Models;

namespace Data_annotations.ReferenceComparer_specs;

public class Equal_by_reference
{
    [Test]
    public void Two_instances_with_same_hash_are_diffrent()
    => ReferenceComparer.Instance.Equals(
        new ModelWithOneState(),
        new ModelWithOneState()).Should().BeFalse();

    [Test]
    public void Same_refernce_are_equal()
    {
        var model = new ModelWithOneState();
        var other = model;

        ReferenceComparer.Instance
            .Equals(model, other)
            .Should().BeTrue();
    }
}
