using System.Runtime.CompilerServices;

namespace TheLair.Extensions.IEnumerable;

public static class SelectToSomething
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static U[] ToArray<T, U>(this IEnumerable<T> collection, Func<T, U> selector)
    {
        return (collection.Select(selector)
            .ToArray());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<U> ToList<T, U>(this IEnumerable<T> collection, Func<T, U> selector)
    {
        return (collection.Select(selector)
            .ToList());
    }
}