#if NETSTANDARD2_0
namespace System.Collections.Concurrent;

internal static class QowaivConcurrentDictionaryExtensions
{
    [Impure]
    public static TValue GetOrAdd<TKey, TValue, TArg>(
        this ConcurrentDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TKey, TArg, TValue> valueFactory,
        TArg factoryArgument)
        where TKey : notnull
        => dictionary.GetOrAdd(key, k => valueFactory(k, factoryArgument));
}
#endif
