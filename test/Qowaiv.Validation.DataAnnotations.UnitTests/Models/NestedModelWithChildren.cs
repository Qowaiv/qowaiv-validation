namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

[NestedModel]
public class NestedModelWithChildren
{
    [Mandatory]
    public Guid Id { get; set; }

    [Mandatory]
    public ChildModel[] Children { get; set; }

    [NestedModel]
    public class ChildModel
    {
        [Mandatory]
        public string ChildName { get; set; }

        [Optional]
        public GrandchildModel[] Grandchildren { get; set; }
    }

    [NestedModel]
    public class GrandchildModel
    {
        [Mandatory]
        public string GrandchildName { get; set; }
    }
}
