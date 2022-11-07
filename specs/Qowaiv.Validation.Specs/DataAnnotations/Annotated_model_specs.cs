using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Annotated_model_specs;

public class Does_not_crash_on
{
    [Test]
    public void inaccessible_property()
        => new AnnotatedModelValidator<ModelWithInaccassibleProperty>()
        .Validate(new())
        .Should().BeInvalid()
        .WithMessage(ValidationMessage.Error("The value is inaccessible.", "SomeProperty"));

    [Test]
    public void indexed_property()
       => new AnnotatedModelValidator<ModelWithIndexedProperty>()
       .Validate(new())
       .Should().BeValid();

    class ModelWithInaccassibleProperty
    {
        public int SomeProperty => throw new NotImplementedException();
    }

    class ModelWithIndexedProperty
    {
        public int this[int index] => index * 42;
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

public  class With_debugger_experience
{
    [Test]
    public void Has_debugger_display()
    {
        var annotated = AnnotatedModel.Get(typeof(Model));
        annotated.Should().HaveDebuggerDisplay("Data_annotations.Annotated_model_specs.With_debugger_experience+Model, Attributes: 1, Properties: 2");
    }

    [NestedModel]
    internal class Model
    {
        public Guid Id { get; }
        public string Name { get; }
    }
}
