using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Annotated_model_specs;

public class Does_not_crash_on
{
    [Test]
    public void inaccessible_property()
        => new AnnotatedModelValidator<ModelWithInaccessibleProperty>()
        .Validate(new())
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The value is inaccessible.", "SomeProperty"));

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
        public int SomeProperty => throw new NotImplementedException();
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
            set => number = value;
        }
        private int number;
#pragma warning restore S2376 // Write-only properties should not be used
    }
}

public class Has_no_properties_for
{
    [TestCase(typeof(int))]
    [TestCase(typeof(double))]
    [TestCase(typeof(bool))]
    public void primitives(Type primitive)
        => AnnotatedModel.Get(primitive).Properties.Should().BeEmpty();

    [Test]
    public void @string()
        => AnnotatedModel.Get(typeof(string)).Properties.Should().BeEmpty();

    [Test]
    public void enums()
        => AnnotatedModel.Get(typeof(TypeCode)).Properties.Should().BeEmpty();
}
