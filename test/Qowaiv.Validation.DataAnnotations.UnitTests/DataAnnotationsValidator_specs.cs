using Qowaiv.Identifiers;

namespace DataAnnotationsValidator_specs;

public class Validates
{
    [Test]
    public void ModelWithMandatoryProperties_with_errors()
    {
        using (CultureInfoScope.NewInvariant())
        {
            new ModelWithMandatoryProperties()
                .Should().BeInvalidFor(new AnnotatedModelValidator<ModelWithMandatoryProperties>())
                .WithMessages(
                    ValidationMessage.Error("The E-mail address field is required.", "Email"),
                    ValidationMessage.Error("The SomeString field is required.", "SomeString"));
        }
    }

    [Test]
    public void ModelWithMandatoryProperties_as_valid()
        => new ModelWithMandatoryProperties
        {
            Email = EmailAddress.Parse("info@qowaiv.org"),
            SomeString = "Some value",
        }
        .Should().BeValidFor(new AnnotatedModelValidator<ModelWithMandatoryProperties>());

    [Test]
    public void ModelWithAllowedValues_with_error()
        => new ModelWithAllowedValues
        {
            Country = Country.TR
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<ModelWithAllowedValues>())
        .WithMessage(ValidationMessage.Error("The value of the Country field is not allowed.", "Country"));

    [Test]
    public void Validate_ModelWithAllowedValues_is_valid()
        => new ModelWithAllowedValues()
        .Should().BeValidFor(new AnnotatedModelValidator<ModelWithAllowedValues>());

    [Test]
    public void ModelWithForbiddenValues_with_error()
        => new ModelWithForbiddenValues
        {
            Email = EmailAddress.Parse("spam@qowaiv.org"),
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<ModelWithForbiddenValues>())
        .WithMessage(ValidationMessage.Error("The value of the Email field is not allowed.", "Email"));

    [Test]
    public void ModelWithForbiddenValues_as_valid()
        => new ModelWithForbiddenValues
        {
            Email = EmailAddress.Parse("info@qowaiv.org"),
        }
        .Should().BeValidFor(new AnnotatedModelValidator<ModelWithForbiddenValues>());

    [Test]
    public void Validate_NestedModelWithNullChild_with_error()
        => new NestedModel
        {
            Id = Guid.NewGuid()
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<NestedModel>())
        .WithMessage(ValidationMessage.Error("The Child field is required.", "Child"));

    [Test]
    public void NestedModelWithInvalidChild_with_error()
        => new NestedModel
        {
            Id = Guid.NewGuid(),
            Child = new NestedModel.ChildModel()
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<NestedModel>())
        .WithMessage(ValidationMessage.Error("The Name field is required.", "Child.Name"));

    [Test]
    public void NestedModelWithInvalidChildren_with_error()
        => new NestedModelWithChildren
        {
            Id = Guid.NewGuid(),
            Children = new[]
            {
                    new NestedModelWithChildren.ChildModel{ ChildName = "Valid Name" },
                    new NestedModelWithChildren.ChildModel(),
            }
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<NestedModelWithChildren>())
        .WithMessage(ValidationMessage.Error("The ChildName field is required.", "Children[1].ChildName"));

    [Test]
    public void NestedModelWithInvalidGrandchildren_with_error()
        => new NestedModelWithChildren
        {
            Id = Guid.NewGuid(),
            Children = new[]
            {
                    new NestedModelWithChildren.ChildModel{ ChildName = "Valid Name" },
                    new NestedModelWithChildren.ChildModel
                    {
                        Grandchildren = new[]
                        {
                            new NestedModelWithChildren.GrandchildModel(),
                            new NestedModelWithChildren.GrandchildModel{ GrandchildName = "Valid Name" },
                        },
                    },
            }
        }
        .Should().BeInvalidFor(new AnnotatedModelValidator<NestedModelWithChildren>())
        .WithMessages(
            ValidationMessage.Error("The ChildName field is required.", "Children[1].ChildName"),
            ValidationMessage.Error("The GrandchildName field is required.", "Children[1].Grandchildren[0].GrandchildName"));

    [Test]
    public void NestedModelWithLoop_with_error()
    {
        var model = new NestedModelWithLoop
        {
            Id = Guid.NewGuid(),
            Child = new NestedModelWithLoop.ChildModel(),
        };
        model.Child.Parent = model;

        model.Should().BeInvalidFor(new AnnotatedModelValidator<NestedModelWithLoop>())
            .WithMessage(ValidationMessage.Error("The Name field is required.", "Child.Name"));
    }

    [Test]
    public void ModelThatReturnsNoneMessage_is_valid()
        => new ModelThatReturnsNoneMessage()
        .Should().BeValidFor(new AnnotatedModelValidator<ModelThatReturnsNoneMessage>())
        .WithoutMessages();
}

public class Validates_
{
    [Test]
    public void Model_with_valid_IValidatableObject_child()
    {
        var model = new ModelWithIValidatableObjectChild()
        {
            Child = new(){ Answer = 42 },
        };
        model.Should().BeValidFor(new AnnotatedModelValidator<ModelWithIValidatableObjectChild>())
        .WithoutMessages();
    }

    [Test]
    public void Model_with_valid_decorated_child()
    {
        var model = new ModelWithDecoratedChild()
        {
            Child = new() { Answer = 42 },
        };
        model.Should().BeValidFor(new AnnotatedModelValidator<ModelWithDecoratedChild>())
        .WithoutMessages();
    }
}

public class Invalidates
{
    [Test]
    public void Model_with_invalid_IValidatableObject_child()
        => new ModelWithIValidatableObjectChild()
        .Should().BeInvalidFor(new AnnotatedModelValidator<ModelWithIValidatableObjectChild>())
        .WithMessage(ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", "Child.Answer"));

    [Test]
    public void Model_with_invalid_decorated_child()
        => new ModelWithDecoratedChild()
        .Should().BeInvalidFor(new AnnotatedModelValidator<ModelWithDecoratedChild>())
        .WithMessage(ValidationMessage.Error("Answer to the Ultimate Question of Life, the Universe, and Everything.", "Child"));
}

public class Validates_without_crashing
{
    [Test]
    public void Model_with_generic_typed_property()
    {
        var model = new NestedModelWithGenerics
        {
            Id = Id<ForId>.Next(),
            Children = new List<NestedModelWithGenerics.Child>
            {
                new NestedModelWithGenerics.Child { Name = "Indi" },
            },
        };
        model.Should().BeValidFor(new AnnotatedModelValidator<NestedModelWithGenerics>())
        .WithoutMessages();
    }

    [Test]
    public void Model_with_not_validatable_child()
    {
        var model = new ModelWithNotValidatableChild()
        {
            Stream = new System.IO.MemoryStream(),
        };
        model.Should().BeValidFor(new AnnotatedModelValidator<ModelWithNotValidatableChild>())
        .WithoutMessages();
    }
}
