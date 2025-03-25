using BenchmarkDotNet.Running;

namespace Benchmarks;

internal static class Program
{
    public static void Main(string[] _)
    {
        BenchmarkRunner.Run<AllowedValues>();
    }
}
