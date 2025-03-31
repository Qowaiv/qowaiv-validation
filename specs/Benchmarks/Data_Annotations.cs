using BenchmarkDotNet.Attributes;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.Collections.Generic;

namespace Benchmarks;

public static class Data_Annotations
{
    [MemoryDiagnoser]
    public class Validate
    {
        internal readonly BenchmarkModelValidator FluentValidator = new();
        internal BenchmarkModel Model = new(); 

        internal readonly IDictionary<string, string[]> Empty = new Dictionary<string, string[]>();

        [IterationSetup]
        public void Setup()
        {
            Model  = BenchmarkModel.New(Count);
        }

        [Params(10, 100, 1000, 10_000)]
        public int Count { get; set; }

        [Benchmark]
        public Result Qowaiv() => AnnotatedModelValidator.Validate(Model);


        [Benchmark]
        public FluentValidation.Results.ValidationResult Fluent() => FluentValidator.Validate(Model);

        [Benchmark(Baseline = true)]
        public IDictionary<string, string[]> MiniValidator()
            => MiniValidation.MiniValidator.TryValidate(Model, out var errors)
            ? Empty
            : errors;
    }
}
