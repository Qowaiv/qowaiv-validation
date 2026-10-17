using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

var provider = new Provider();
var items = new Dictionary<object, object?>();

var m10 = BenchmarkModel.New(10);
var m100 = BenchmarkModel.New(100);
var m1000 = BenchmarkModel.New(1000);
var m10000 = BenchmarkModel.New(10000);
var root = new BenchmarkModel { Children = [] };
var plain = new Plain { Name = "ok" };
var noAnn = new PlainNoAnnotations { Name = "ok" };

for (var i = 0; i < 2000; i++)
{
    AnnotatedModelValidator.Validate(m10);
    AnnotatedModelValidator.Validate(plain);
    AnnotatedModelValidator.Validate(noAnn);
    _ = new ValidationContext(m10, provider, items);
}

Measure("Validate(New(10))", () => AnnotatedModelValidator.Validate(m10), 200_000);
Measure("Validate(New(100))", () => AnnotatedModelValidator.Validate(m100), 50_000);
Measure("Validate(New(1000))", () => AnnotatedModelValidator.Validate(m1000), 5_000);
Measure("Validate(New(10000))", () => AnnotatedModelValidator.Validate(m10000), 500);
Measure("Mini(New(10))", () => MiniValidation.MiniValidator.TryValidate(m10, out _), 200_000);
Measure("Mini(New(100))", () => MiniValidation.MiniValidator.TryValidate(m100, out _), 50_000);
Measure("Mini(New(1000))", () => MiniValidation.MiniValidator.TryValidate(m1000, out _), 5_000);
Measure("Mini(New(10000))", () => MiniValidation.MiniValidator.TryValidate(m10000, out _), 500);
Measure("Validate(root only)", () => AnnotatedModelValidator.Validate(root), 200_000);
Measure("Validate(Plain w/ attrs)", () => AnnotatedModelValidator.Validate(plain), 200_000);
Measure("Validate(Plain no attrs)", () => AnnotatedModelValidator.Validate(noAnn), 200_000);

static void Measure(string name, Action act, int n)
{
    for (var i = 0; i < 2000; i++) act();
    GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
    var alloc0 = GC.GetAllocatedBytesForCurrentThread();
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < n; i++) act();
    sw.Stop();
    var alloc = GC.GetAllocatedBytesForCurrentThread() - alloc0;
    Console.WriteLine($"{name,-30} {sw.Elapsed.TotalMilliseconds * 1000 / n,10:F2} us/call   {alloc / n,9:F0} B/call");
}

sealed class Plain
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 10)]
    public int Number { get; set; }
}

sealed class PlainNoAnnotations
{
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }
}

sealed class Provider : IServiceProvider
{
    public object? GetService(Type serviceType) => null;
}
