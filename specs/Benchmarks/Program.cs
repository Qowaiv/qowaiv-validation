using BenchmarkDotNet.Running;

namespace Benchmarks;

internal static class Program
{
    public static void Main(string[] _)
    {
        BenchmarkRunner.Run<Attributes.Range>();
    }

    public static void All()
    {
        BenchmarkRunner.Run<Attributes.AllowedValues>();
        BenchmarkRunner.Run<Attributes.Range>();
        BenchmarkRunner.Run<Data_Annotations.Validate>();
    }
}
