#if NETSTANDARD2_0
namespace System.Collections.Frozen;

internal sealed class FrozenSet<T>(IEnumerable<T> collection) : HashSet<T>(collection);

internal static class QowaivFrozenExtensions
{
    [Pure]
    public static FrozenSet<T> ToFrozenSet<T>(this IEnumerable<T> set) where T : notnull => new(set);
}
#endif
