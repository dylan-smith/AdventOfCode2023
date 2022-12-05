namespace AdventOfCode;

public static class EnumerableExtensions
{
    public static bool UnorderedEquals<T>(this IEnumerable<T> a, IEnumerable<T> b)
    {
        if (a.Count() != b.Count())
        {
            return false;
        }

        var sortedA = a.OrderBy(x => x);
        var sortedB = b.OrderBy(x => x);

        return sortedA.SequenceEqual(sortedB);
    }

    public static IEnumerable<(int index, T item)> SelectWithIndex<T>(this IEnumerable<T> a)
    {
        var list = a.ToList();

        for (var i = 0; i < list.Count; i++)
        {
            yield return (i, list[i]);
        }
    }

    public static IEnumerable<IEnumerable<T>> Window<T>(this IEnumerable<T> list, int size)
    {
        for (var i = size - 1; i < list.Count(); i++)
        {
            yield return list.Skip(i - size + 1).Take(size);
        }
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> list, int chunkSize)
    {
        var pos = 0;

        while (pos < list.Count())
        {
            yield return list.Skip(pos).Take(chunkSize);
            pos += chunkSize;
        }
    }

    public static long Multiply(this IEnumerable<long> list)
    {
        return list.Aggregate(1L, (acc, x) => acc * x);
    }

    public static long Multiply<T>(this IEnumerable<T> list, Func<T, long> projection)
    {
        return list.Aggregate(1L, (acc, x) => acc * projection(x));
    }

    public static int Multiply(this IEnumerable<int> list)
    {
        return list.Aggregate(1, (acc, x) => acc * x);
    }

    public static int Multiply<T>(this IEnumerable<T> list, Func<T, int> projection)
    {
        return list.Aggregate(1, (acc, x) => acc * projection(x));
    }

    public static T WithMin<T>(this IEnumerable<T> a, Func<T, int> selector)
    {
        var min = a.Min(selector);
        return a.First(x => selector(x) == min);
    }

    public static T WithMin<T>(this IEnumerable<T> a, Func<T, long> selector)
    {
        var min = a.Min(selector);
        return a.First(x => selector(x) == min);
    }

    public static T WithMin<T>(this IEnumerable<T> a, Func<T, double> selector)
    {
        var min = a.Min(selector);
        return a.First(x => selector(x) == min);
    }

    public static T WithMax<T>(this IEnumerable<T> a, Func<T, int> selector)
    {
        var max = a.Max(selector);
        return a.First(x => selector(x) == max);
    }

    public static T WithMax<T>(this IEnumerable<T> a, Func<T, long> selector)
    {
        var max = a.Max(selector);
        return a.First(x => selector(x) == max);
    }

    public static T WithMax<T>(this IEnumerable<T> a, Func<T, double> selector)
    {
        var max = a.Max(selector);
        return a.First(x => selector(x) == max);
    }

    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> a)
    {
        while (true)
        {
            foreach (var x in a)
            {
                yield return x;
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        list.ToList().ForEach(action);
    }

    public static long LeastCommonMultiple(this IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    public static long LeastCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    public static long GreatestCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }

    public static int LeastCommonMultiple(this IEnumerable<int> numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    public static int LeastCommonMultiple(int a, int b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    public static int GreatestCommonDivisor(int a, int b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }
}
