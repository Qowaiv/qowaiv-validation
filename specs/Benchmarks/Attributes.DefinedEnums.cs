using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Benchmarks;

public partial class Attributes
{
    public class Attributes_DefinedEnums
    {
        private readonly DefinedOnlyAttribute<HttpStatusCode> generic = new();
        private readonly DefinedEnumValuesOnlyAttribute nonGeneric = new();
        private readonly NotCachedAttribute notChached = new();


        [Benchmark(Baseline = true)]
        public void NonGeneric()
        {
            foreach (var value in Data.Enums.NonContinous) _ = nonGeneric.IsValid(value);
        }

        [Benchmark]
        public void Generic()
        {
            foreach (var value in Data.Enums.NonContinous) _ = generic.IsValid(value);
        }

        [Benchmark]
        public void Not_chached()
        {
            foreach (var value in Data.Enums.NonContinous) _ = notChached.IsValid(value);
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class NotCachedAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value) => value switch
        {
            null => true,
            _ when value.GetType() is { IsEnum: true } enumType => Enum.IsDefined(enumType, value),
            _ => throw UnsupportedType.ForAttribute<NotCachedAttribute>(value.GetType()),
        };
    }
}
