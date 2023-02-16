using System;

internal static class Methods
{
    public static T Max<T>(T x, T y)
    {
        return ((IComparable)x).CompareTo((IComparable)y) > 0 ? x : y;
    }
}