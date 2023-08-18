namespace System;

internal static class FloatingPointExtensions
{
    [Pure]
    public static bool IsFinite(this double d)
#if NETSTANDARD
        => !double.IsNaN(d) && !double.IsInfinity(d);
#else
        => double.IsFinite(d);
#endif

    [Pure]
    public static bool IsFinite(this float f)
#if NETSTANDARD
        => !float.IsNaN(f) && !float.IsInfinity(f);
#else
        => float.IsFinite(f);
#endif
}
