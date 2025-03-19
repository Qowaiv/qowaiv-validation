using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Specs.DataAnnotations;

public class Required_modifier
{
    [TestCase(null)]
    [TestCase("")]
    public void makes_non_nullable_references_types_required(string? value)
    {
        var model = new RequiredModel
        {
            Nullable = "Some text",
            NotNullable = value!,
            Svo = EmailAddress.Parse("info@qowaiv.org"),
        };

        var validator = new AnnotatedModelValidator<RequiredModel>();

        validator.Validate(model)
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("The NotNullable field is required.", "NotNullable"));
    }

    [Test]
    public void has_lower_priority_than_attributes()
    {
        var model = new DecorateddModel { NotNullable = string.Empty };

        var validator = new AnnotatedModelValidator<DecorateddModel>();

        validator.Validate(model)
            .Should().BeValid()
            .WithMessages();
    }

    [TestCase(null)]
    [TestCase("")]
    public void does_not_alter_nullable_references_types(string? value)
    {
        var model = new RequiredModel
        {
            Nullable = value,
            NotNullable ="Some value",
            Svo = EmailAddress.Parse("info@qowaiv.org"),
        };

        var validator = new AnnotatedModelValidator<RequiredModel>();

        validator.Validate(model)
            .Should().BeValid()
            .WithMessages();
    }

    [Test]
    public void does_not_alter_reference_types()
    {
        var model = new RequiredModel
        {
            Nullable = null,
            NotNullable = "Some value",
            Svo = EmailAddress.Empty,
        };

        var validator = new AnnotatedModelValidator<RequiredModel>();

        validator.Validate(model)
            .Should().BeValid()
            .WithMessages();
    }

    internal class RequiredModel
    {
        public required string? Nullable { get; init; }

        public required string NotNullable { get; init; }

        public required EmailAddress Svo { get; init; }
    }

    internal class DecorateddModel
    {
        [Required(AllowEmptyStrings = true)]
        public required string NotNullable { get; init; }
    }
}
