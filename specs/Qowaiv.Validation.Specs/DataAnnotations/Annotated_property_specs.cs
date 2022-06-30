using Qowaiv.Validation.DataAnnotations;

namespace DataAnnotations.Annotated_property_specs;

public  class With_debugger_experience
{
    [Test]
    public void Has_debugger_display()
    {
        var annotate = AnnotatedModel.Get(typeof(Model));
        var property = annotate.Properties.Single();
        property.Should().HaveDebuggerDisplay("System.Guid Id, Attributes: Mandatory");
    }

    internal class Model
    {
        [Mandatory]
        public Guid Id { get; set; }
    }
}
