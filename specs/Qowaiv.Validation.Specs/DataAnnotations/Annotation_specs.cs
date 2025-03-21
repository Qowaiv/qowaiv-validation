using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Annotation_specs;

public class Does_not_crash_on
{
    [Test]
    public void inaccessible_property()
        => new AnnotatedModelValidator<Model.WithInaccessibleProperty>()
        .Validate(new())
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The value is inaccessible.", "ThrowsOnGet"));

    [Test]
    public void indexed_property()
        => new AnnotatedModelValidator<Model.WithIndexedProperty>()
        .Validate(new())
        .Should().BeValid();

    [Test]
    public void set_only_property()
        => new AnnotatedModelValidator<Model.WithSetOnlyProperty>()
        .Validate(new())
        .Should().BeValid();
}

public class Resolves_property
{
    [Test]
    public void on_type_validation()
    {
        var annotated = Annotator.Annotate(typeof(Model.WithTypeAnnotatedMember));
        var prop = annotated!.Members.Single();

        prop.Should().BeEquivalentTo(new
        {
            Name = "Member",
            TypeAnnotations = new { Attributes = new { Count = 1 } },
        });
    }

    [Test]
    public void validates_it()
        => new Model.WithTypeAnnotatedMember() { Member = new() }
        .ValidateAnnotations()
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("This is an invalid class", "Member"));

    [Test]
    public void validates_attributes()
        => new Model.WithAnnotatedProperty() { Name = "An" }
        .ValidateAnnotations()
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The length of the Name field should be at least 3.", "Name"));
}

public class Resolves
{
    [Test]
    public void Display_attribute()
        => new Model.WithDisplay() { Prop = "Too long" }
        .ValidateAnnotations()
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The length of the Property field should be at most 2.", "Prop"));
}

public class Has_no_properties_for
{
    [TestCase(typeof(int))]
    [TestCase(typeof(double))]
    [TestCase(typeof(bool))]
    public void primitives(Type primitive)
        => Annotator.Annotate(primitive).Should().BeNull();

    [Test]
    public void @string()
        => Annotator.Annotate(typeof(string)).Should().BeNull();

    [Test]
    public void enums()
        => Annotator.Annotate(typeof(TypeCode)).Should().BeNull();
}

public class Is_None_for
{
    [Test]
    public void not_annotated_model()
    {
        var annotated = Annotator.Annotate(typeof(Model.WithoutAnnotations));
        annotated.Should().BeNull();
    }
}
