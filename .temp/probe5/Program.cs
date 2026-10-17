using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.Diagnostics;

var models = new Dictionary<int, BenchmarkModel>();
foreach (var n in new[] { 10, 100, 1000, 10_000 })
{
    models[n] = BenchmarkModel.New(n);
}

Measure("warm", () => AnnotatedModelValidator.Validate(models[10]));

foreach (var n in new[] { 10, 100, 1000, 10_000 })
{
    var model = models[n];
    Measure($"Validate(n={n})", () => AnnotatedModelValidator.Validate(model));
}

static void Measure(string name, Action act)
{
    for (var i = 0; i < 5000; i++) act();
    GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();

    var batch = 200;
    var batches = 40;
    var best = double.MaxValue;
    long bestAlloc = 0;
    for (var b = 0; b < batches; b++)
    {
        var alloc0 = GC.GetAllocatedBytesForCurrentThread();
        var sw = Stopwatch.StartNew();
        for (var i = 0; i < batch; i++) act();
        sw.Stop();
        var alloc = GC.GetAllocatedBytesForCurrentThread() - alloc0;
        var perOp = sw.Elapsed.TotalMilliseconds * 1000 / batch;
        if (perOp < best) { best = perOp; bestAlloc = alloc / batch; }
    }
    Console.WriteLine($"{name,-24} {best,12:F3} us/call   {bestAlloc,10:F0} B/call");
}
