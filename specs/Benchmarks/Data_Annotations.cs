using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Hdn.DomainModel;
using Hdn.TestData;
using Qowaiv;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.Collections.Generic;

namespace Benchmarks;

[SimpleJob(RuntimeMoniker.Net10_0)]
[SimpleJob(RuntimeMoniker.Net11_0)]
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
[HideColumns(Column.StdDev, Column.Error, Column.Median, Column.RatioSD)]
public class Data_Annotations
{
    internal static readonly IDictionary<string, string[]> Empty = new Dictionary<string, string[]>();

    public class Validate_bigger
    {
        protected readonly BenchmarkBerichten Berichten = [];

        [Params("AX", "SX", "MX")]
        public string Bericht { get; set; } = string.Empty;

        protected HdnBericht HdnBericht => Berichten[Bericht].Bericht;

        [Benchmark]
        public Result Qowaiv() => HdnBericht.Validate();

        [Benchmark(Baseline = true)]
        public IDictionary<string, string[]> MiniValidator()
             => MiniValidation.MiniValidator.TryValidate(HdnBericht, out var errors)
             ? Empty
             : errors;
    }

    public class Validate_small
    {
        internal readonly BenchmarkModelValidator FluentValidator = new();
        internal BenchmarkModel Model = new(); 

        [IterationSetup]
        public void Setup() => Model = BenchmarkModel.New(Count);

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
