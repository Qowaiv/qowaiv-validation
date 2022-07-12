using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Nested_model_specs;

public class Child_model
{
    [Test]
    public void decorated_with_nested_model_attribute_validates_grand_children()
        => new WithNestedChild().Should()
        .BeInvalidFor(new AnnotatedModelValidator<WithNestedChild>());

    [Test]
    public void not_decorated_with_nested_model_attribute_skips_grand_children()
        => new WithoutNestedChild().Should()
        .BeValidFor(new AnnotatedModelValidator<WithoutNestedChild>());
}

internal class WithNestedChild
{
    public NestedChild Child { get; } = new();
}

internal class WithoutNestedChild
{
    public NotNestedChild Child { get; } = new();
}

[NestedModel]
internal class NestedChild
{
    public GrandChild Child { get; } = new();
}
internal class NotNestedChild
{
    public GrandChild Child { get; } = new();
}

internal class GrandChild
{
    [Mandatory]
    public int? Answer { get; set; }
}
