using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Data_annotations.Annotation_specs;

public class Does_not_crash_on
{
    [Test]
    public void inaccessible_property()
        => new AnnotatedModelValidator<ModelWithInaccessibleProperty>()
        .Validate(new())
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The value is inaccessible.", "ThrowsOnGet"));

    [Test]
    public void indexed_property()
       => new AnnotatedModelValidator<ModelWithIndexedProperty>()
       .Validate(new())
       .Should().BeValid();

    [Test]
    public void set_only_property()
      => new AnnotatedModelValidator<ModelWithSetOnlyProperty>()
      .Validate(new())
      .Should().BeValid();

    public class ModelWithInaccessibleProperty
    {
        [Allowed<int>("42")]
        public int ThrowsOnGet => throw new NotImplementedException(ToString());
    }

    public class ModelWithIndexedProperty
    {
        public int this[int index] => index * 42;
    }

    public class ModelWithSetOnlyProperty
    {
#pragma warning disable S2376 // Write-only properties should not be used
        // This is a test to check if write-only properties are handled correctly.
        public int SomeProperty
        {
            set => field = value;
        }
#pragma warning restore S2376
    }
}

public class Resolves_property
{
    [Test]
    public void on_type_validation()
    {
        var annotated = Annotator.Annotate(typeof(ModelWithAnnotatedClassProp));
        var prop = annotated!.Members.Single();

        prop.Should().BeEquivalentTo(new
        {
            Name = "Child",
            TypeAnnotations = new { Attributes = new { Count = 1 } },
        });
    }

    [Test]
    public void validates_it()
        => new ModelWithAnnotatedClassProp()
        .ValidateAnnotations()
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("This is a class", "Child"));

    [Test]
    public void validates_attributes()
        => new AnnotatedModel() { Name = "An" }
        .ValidateAnnotations()
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The length of the Name field should be at least 3", "Name"));
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
        var annotated = Annotator.Annotate(typeof(ModelWithoutAnnotations));
        annotated.Should().BeNull();
    }
}

file class ModelWithoutAnnotations
{
    public FileInfo? File { get; init; }

    public string? Name { get; init; }

    public int Number { get; init; }

    public DateTime CreatedUtc => File?.CreationTimeUtc ?? Clock.UtcNow();

    [SkipValidation]
    public required string Required { get; init; }

    public Parent? WithLoop { get; init; }
}

file class Parent
{
    public Child[] Childen { get; init; } = [];
}

file class Child
{
    public Parent? Parent { get; init; }
}

file class ModelWithAnnotatedClassProp
{
    public AnnotatedClass Child { get; init; } = new();
}

[IsClass]
file class AnnotatedClass
{
    public string? Name { get; init; }
}

[AttributeUsage(AttributeTargets.Class)]
file sealed class IsClassAttribute() : ValidationAttribute("This is a class")
{
    public override bool IsValid(object? value) => false;
}

file sealed class AnnotatedModel
{
    [Length.AtLeast(3)]
    public string? Name { get; init; }
}
