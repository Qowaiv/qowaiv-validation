namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

public class NestedModelWithChildren
{
    [Mandatory]
    public Guid Id { get; set; }

    [Mandatory]
    public ChildModel[] Children { get; set; }

    public class ChildModel
    {
        [Mandatory]
        public string ChildName { get; set; }

        [Optional]
        public GrandchildModel[] Grandchildren { get; set; }
    }

    public class GrandchildModel
    {
        [Mandatory]
        public string GrandchildName { get; set; }
    }
}
