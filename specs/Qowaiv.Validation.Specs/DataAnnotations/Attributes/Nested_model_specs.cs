﻿namespace Data_annotations.Attributes.Nested_model_specs;

public class Child_model
{
    [Test, Ignore("Somehting is wrong")]
    public void decorated_with_nested_model_attribute_valides_grand_children()
        => new WithNestedChild().Should()
        .BeInvalidFor(new AnnotatedModelValidator<WithNestedChild>());

    [Test]
    public void not_decorated_with_nested_model_attribute_skips_grand_children()
        => new WithoutNestedChild().Should()
        .BeValidFor(new AnnotatedModelValidator<WithoutNestedChild>());
}

internal class WithNestedChild
{
    public WithNestedChild Child { get; } = new();
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