using BenchmarkDotNet.Attributes;
using Qowaiv.Diagnostics.Contracts;
using Qowaiv.Globalization;
using Qowaiv.Validation.DataAnnotations;

namespace Benchmarks;

[Inheritable]
public class AllowedValues
{
    private readonly AllowedAttribute<Country> WithGenerics = new("NL", "BE", "LU", "DE", "FR");

    [Benchmark]
    public bool generic()
    {
        var result = false;
        foreach (var country in Country.All)
        {
            result |= WithGenerics.IsValid(country);
        }
        return result;
    }
}
