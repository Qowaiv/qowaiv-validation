using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Bits = Benchmarks.Data.Enums.Bits;

namespace Benchmarks;

public partial class Attributes
{
    public class Attributes_DefinedEnumFlags
    {
        private readonly DefinedOnlyAttribute<Bits> generic = new();
        private readonly DefinedEnumValuesOnlyAttribute nonGeneric = new();


        [Benchmark(Baseline = true)]
        public void NonGeneric()
        {
            foreach (var value in Data.Enums.Flags) _ = nonGeneric.IsValid(value);
        }

        [Benchmark]
        public void Generic()
        {
            foreach (var value in Data.Enums.Flags) _ = generic.IsValid(value);
        }
    }

    public class Attributes_DefinedEnums
    {
        private readonly DefinedOnlyAttribute<HttpStatusCode> generic = new();
        private readonly DefinedEnumValuesOnlyAttribute nonGeneric = new();
        private readonly NotCachedAttribute notCached = new();


        [Benchmark(Baseline = true)]
        public void NonGeneric()
        {
            foreach (var value in Data.Enums.Values) _ = nonGeneric.IsValid(value);
        }

        [Benchmark]
        public void Generic()
        {
            foreach (var value in Data.Enums.Values) _ = generic.IsValid(value);
        }

        [Benchmark]
        public void Not_cached()
        {
            foreach (var value in Data.Enums.Values) _ = notCached.IsValid(value);
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
