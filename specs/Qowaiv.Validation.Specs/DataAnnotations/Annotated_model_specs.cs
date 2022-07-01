using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Annotated_model_specs;

public  class With_debugger_experience
{
    [Test]
    public void Has_debugger_display()
    {
        var annotated = AnnotatedModel.Get(typeof(Model));
        annotated.Should().HaveDebuggerDisplay("DataAnnotations.Annotated_model_specs.With_debugger_experience+Model, Attributes: 1, Properties: 2");
    }

    [NestedModel]
    internal class Model
    {
        public Guid Id { get; }
        public string Name { get; }
    }
}
