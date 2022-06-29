using Qowaiv.Identifiers;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

[NestedModel]
internal class NestedModelWithGenerics
{
    [Mandatory]
    public Id<ForId> Id { get; set; }

    [Any]
    public List<Child> Children { get; set; } = new();

    [NestedModel]
    internal class Child
    {
        [Mandatory]
        public string Name { get; set; }
    }
}

public sealed class ForId : UuidBehavior { }
