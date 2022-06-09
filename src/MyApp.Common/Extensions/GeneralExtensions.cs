using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Common.Extensions;

public static class GeneralExtensions
{
    public static TOut Map<TIn, TOut>(this TIn x, Func<TIn, TOut> fn)
    {
        return fn(x);
    }

    public static T? Map<T>(this T? nullable, Func<T, T> fn)
        where T : struct
    {
        return nullable.HasValue ? fn(nullable.Value) : nullable;
    }

    public static T Tap<T>(this T x, Action<T> act)
    {
        act(x);
        return x;
    }

    public static bool ContainedIn<T>(this T item, params T[] items)
    {
        return items.Contains(item);
    }

    public static bool ContainedIn<T>(this T item, IEnumerable<T> items)
    {
        return items.Contains(item);
    }

    public static bool IsBetween<T>(this T value, T fromValue, T toValue)
        where T : IComparable
    {
        return value.CompareTo(fromValue) >= 0
            && value.CompareTo(toValue) <= 0;
    }
}
