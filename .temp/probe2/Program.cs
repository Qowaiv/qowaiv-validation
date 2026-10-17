using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.Diagnostics;

// Mimic BenchmarkDotNet Validate_small: IterationSetup creates fresh model, InvocationCount=1.
Mimic("Qowaiv", 10, m => AnnotatedModelValidator.Validate(m));
Mimic("Mini", 10, m => MiniValidation.MiniValidator.TryValidate(m, out _));
Mimic("Qowaiv", 100, m => AnnotatedModelValidator.Validate(m));
Mimic("Mini", 100, m => MiniValidation.MiniValidator.TryValidate(m, out _));

static void Mimic<T>(string name, int count, Func<BenchmarkModel, T> op)
{
    // warmup
    for (var i = 0; i < 200; i++) op(BenchmarkModel.New(count));

    var times = new List<double>();
    for (var i = 0; i < 60; i++)
    {
        var model = BenchmarkModel.New(count); // [IterationSetup]
        var sw = Stopwatch.StartNew();
        op(model);
        sw.Stop();
        times.Add(sw.Elapsed.TotalMilliseconds * 1000);
    }
    times.Sort();
    var mean = times.Average();
    var median = times[times.Count / 2];
    var p90 = times[(int)(times.Count * 0.9)];
    var max = times[^1];
    Console.WriteLine($"{name,-8} n={count,-6} mean={mean,9:F2} median={median,9:F2} p90={p90,9:F2} max={max,9:F2}  min={times[0],8:F2}");
}
