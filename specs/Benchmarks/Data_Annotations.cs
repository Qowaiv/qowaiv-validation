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
        const int Count = 1000;

        internal readonly DataAnnotatedModelValidator FluentValidator = new();

        internal readonly DataAnnotatedModel Model = DataAnnotatedModel.New(Count);

        internal readonly IDictionary<string, string[]> Empty = new Dictionary<string, string[]>();

        [Benchmark]
        public Result Qowaiv() => AnnotatedModelValidator.Validate(Model);


        //[Benchmark]
        public FluentValidation.Results.ValidationResult Fluent() => FluentValidator.Validate(Model);

        [Benchmark]
        public IDictionary<string, string[]> MiniValidator()
            => MiniValidation.MiniValidator.TryValidate(Model, out var errors)
            ? Empty
            : errors;

        [Benchmark]
        public IDictionary<string, string[]> MiniValidator2()
           => MiniValidation2.MiniValidator.TryValidate(Model, out var errors)
           ? Empty
           : errors;
    }
}
