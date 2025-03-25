using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.DataAnnotations;
using Qowaiv.Validation.TestData;
using System.Collections.Generic;

namespace Benchmarks;

[MemoryDiagnoser]
public static class Data_Annotations
{
    public class Validate
    {
        const int Count = 1000;

        internal readonly DataAnnotatedModelValidator FluentValidator = new();

        internal readonly DataAnnotatedModel Model = DataAnnotatedModel.New(Count);

        internal readonly IDictionary<string, string[]> Empty = new Dictionary<string, string[]>();

        [Benchmark]
        public Result Qowaiv() => AnnotatedModelValidator.Validate(Model);


        [Benchmark]
        public FluentValidation.Results.ValidationResult Fluent() => FluentValidator.Validate(Model);

        [Benchmark]
        public IDictionary<string, string[]> MiniValidator()
            => MiniValidation.MiniValidator.TryValidate(Model, out var errors)
            ? Empty
            : errors;
    }
}
