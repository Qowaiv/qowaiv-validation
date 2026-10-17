using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

var mand = new Root<WithMandatory> { Items = Fill<WithMandatory>(10000) };
var valid = new Root<WithValidatable> { Items = Fill<WithValidatable>(10000) };
var both = new Root<WithBoth> { Items = Fill<WithBoth>(10000) };
var plain = new Root<Plain> { Items = Fill<Plain>(10000) };
var nested = new Root<NestedLeaf> { Items = Fill<NestedLeaf>(10000) };

for (var i = 0; i < 3; i++)
{
    AnnotatedModelValidator.Validate(mand);
    AnnotatedModelValidator.Validate(valid);
    AnnotatedModelValidator.Validate(both);
    AnnotatedModelValidator.Validate(plain);
    AnnotatedModelValidator.Validate(nested);
}

Measure("10000 x Mandatory only", () => AnnotatedModelValidator.Validate(mand));
Measure("10000 x IValidatable only", () => AnnotatedModelValidator.Validate(valid));
Measure("10000 x Mandatory+Validatable", () => AnnotatedModelValidator.Validate(both));
Measure("10000 x plain (no attrs)", () => AnnotatedModelValidator.Validate(plain));
Measure("10000 x nested leaf", () => AnnotatedModelValidator.Validate(nested));

static List<T> Fill<T>(int n) where T : new()
{
    var list = new List<T>(n);
    for (var i = 0; i < n; i++) list.Add(new T());
    return list;
}

static void Measure(string name, Action act)
{
    for (var i = 0; i < 20; i++) act();
    GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
    var n = 200;
    var alloc0 = GC.GetAllocatedBytesForCurrentThread();
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < n; i++) act();
    sw.Stop();
    var alloc = GC.GetAllocatedBytesForCurrentThread() - alloc0;
    Console.WriteLine($"{name,-32} {sw.Elapsed.TotalMilliseconds * 1000 / n,9:F1} us/call   {alloc / n,10:F0} B/call");
}

public sealed class Root<T>
{
    [Any]
    public List<T> Items { get; set; } = [];
}

public sealed class Plain
{
    public int A { get; set; }
}

public sealed class WithMandatory
{
    [Mandatory]
    public Guid Email { get; set; } = Guid.NewGuid();
}

public sealed class WithValidatable : IValidatableObject
{
    public int A { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) { yield break; }
}

public sealed class WithBoth : IValidatableObject
{
    [Mandatory]
    public Guid Email { get; set; } = Guid.NewGuid();
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) { yield break; }
}

public sealed class NestedLeaf
{
    public Leaf Child { get; set; } = new();
    public sealed class Leaf
    {
        public int A { get; set; }
    }
}
