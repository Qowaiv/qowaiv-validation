namespace System;

internal static class QowaivValidationArrayExtensions
{
    [Pure]
    public static T? Find<T>(this T[] array, Predicate<T> predicate) => Array.Find(array, predicate);

    [Pure]
    public static bool Exists<T>(this T[] array, Predicate<T> predicate) => Array.Exists(array, predicate);
}
