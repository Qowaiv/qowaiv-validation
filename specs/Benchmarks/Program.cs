using BenchmarkDotNet.Running;

namespace Benchmarks;

internal static class Program
{
    public static void Main(string[] _)
    {
        BenchmarkRunner.Run<Data_Annotations.Validate>();
    }

    public static void All()
    {
        BenchmarkRunner.Run<AllowedValues>();
    }
}
