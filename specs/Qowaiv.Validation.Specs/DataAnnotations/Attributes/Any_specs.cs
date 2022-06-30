namespace Data_annotations.Attributes.Any_specs;

public class Is_valid_for
{
    [Test]
    public void Not_empty_collection()
       => new AnyAttribute().IsValid(new[] { 42 }).Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void Null()
        => new AnyAttribute().IsValid(null).Should().BeFalse();

    [Test]
    public void Empty_collection()
       => new AnyAttribute().IsValid(Array.Empty<int>()).Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "Het veld Values is verplicht.")]
    [TestCase("en", "The Values field is required.")]
    public void culture_depedent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "Values"));
    }
    internal class Model
    {
        [Any]
        public IEnumerable<string> Values { get; set; } = Array.Empty<string>();
    }
}
