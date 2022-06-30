using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.ReferenceComparer_specs;

public class Equal_by_reference
{
    [Test]
    public void Two_instances_with_same_hash_are_diffrent()
    => ReferenceComparer.Instance.Equals(
        new SingelState(),
        new SingelState()).Should().BeFalse();

    [Test]
    public void Same_refernce_are_equal()
    {
        var model = new SingelState();
        var other = model;

        ReferenceComparer.Instance
            .Equals(model, other)
            .Should().BeTrue();
    }
}

internal sealed class SingelState : IEquatable<SingelState>
{
    public override bool Equals(object obj) => Equals(obj as SingelState);

    public bool Equals(SingelState other) => other is { };

    public override int GetHashCode() => 17;
}
