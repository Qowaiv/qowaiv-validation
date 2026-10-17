using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Data_annotations.Attributes.Any_specs;

public class Is_valid_for
{
    [Test]
    public void Not_empty_collection()
       => new AnyAttribute().IsValid(new[] { 42 }).Should().BeTrue();

    [Test]
    public void Not_empty_enumeration()
        => new AnyAttribute().IsValid(Enumerable.Repeat(0, 2)).Should().BeTrue();

    [Test]
    public void Not_empty_string()
        => new AnyAttribute().IsValid(" ").Should().BeTrue();
}

public class Is_not_valid_for
{
    [Test]
    public void Null()
        => new AnyAttribute().IsValid(null).Should().BeFalse();

    [Test]
    public void Empty_collection()
        => new AnyAttribute().IsValid(Array.Empty<int>()).Should().BeFalse();

    [Test]
    public void Empty_enumeration()
        => new AnyAttribute().IsValid(Enumerable.Repeat(0, 0)).Should().BeFalse();

    [Test]
    public void Empty_string()
        => new AnyAttribute().IsValid(string.Empty).Should().BeFalse();
}

public class With_message
{
    [TestCase("nl", "Het veld Values is verplicht.")]
    [TestCase("en", "The Values field is required.")]
    public void culture_dependent(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model().ValidateWith(new AnnotatedModelValidator<Model>())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "Values"));
    }
    internal class Model
    {
        [Any]
        public IEnumerable<string> Values { get; set; } = [];
    }

    [Test]
    public void supports_custom_error_message()
    {
        var attr = new AnyAttribute
        {
            ErrorMessage = "Custom error message"
        };

        attr.GetValidationMessage(Array.Empty<int>(), new(new object()))!
            .Message.Should().Be("Custom error message");
    }
}
