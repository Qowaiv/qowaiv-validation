using MathNet.Numerics.Random;
using System.Collections.Immutable;
using System.Linq;
using System.Net;

namespace Benchmarks;

internal static class Data
{
    public static class Enums
    {
        private static readonly HttpStatusCode[] nonContinous = Enum.GetValues<HttpStatusCode>();

        public static readonly ImmutableArray<HttpStatusCode> NonContinous = [.. Seeds().Select(_ => nonContinous[Rnd.Next(nonContinous.Length)])];
    }

    public static readonly MersenneTwister Rnd = new(42);

    private static IEnumerable<int> Seeds(int count = 1_000_000) => System.Linq.Enumerable.Range(0, count);
}
