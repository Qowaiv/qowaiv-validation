using Qowaiv.Identifiers;

namespace DataAnnotationsValidator_specs;

public class Validates
{
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
    public void ModelThatReturnsNoneMessage_is_valid()
        => new ModelThatReturnsNoneMessage()
        .Should().BeValidFor(new AnnotatedModelValidator<ModelThatReturnsNoneMessage>())
        .WithoutMessages();
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
