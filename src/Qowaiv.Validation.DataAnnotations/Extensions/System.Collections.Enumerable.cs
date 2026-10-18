namespace System.Collections.Enumerable;

internal static class QowaivEnumerableExtensions
{
    extension(IEnumerable enumerable)
    {
        [Pure]
        public int Count()
        {
            var count = 0;
            var counter = enumerable.GetEnumerator();

            while (counter.MoveNext())
                count++;

            return count;
        }
    }
}
