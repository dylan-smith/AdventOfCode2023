namespace AdventOfCode;

public static class ListExtensions
{
    public static void AddMany<T>(this IList<T> list, T item, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(item);
        }
    }
    public static void Initialize<T>(this IList<T> list, T value, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(value);
        }
    }

    public static void Initialize<T>(this IList<T> list, Func<T> value, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(value());
        }
    }

    public static void RemoveLast<T>(this IList<T> list)
    {
        list.RemoveAt(list.Count - 1);
    }

    public static void RemoveFirst<T>(this IList<T> list)
    {
        list.RemoveAt(0);
    }

    public static T Pop<T>(this IList<T> list)
    {
        var item = list.Last();
        list.RemoveLast();

        return item;
    }

    public static IEnumerable<T> Pop<T>(this IList<T> list, int count)
    {
        var items = list.Skip(list.Count - count).Take(count).ToList();
        list.RemoveRange(list.Count - count, count);

        return items;
    }

    public static void RemoveRange<T>(this IList<T> list, int index, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.RemoveAt(index);
        }
    }

    public static void AddFirst<T>(this IList<T> list, T item)
    {
        list.Insert(0, item);
    }

    public static List<(T A, T B)> GetPairs<T>(this List<T> list)
    {
        var result = new List<(T A, T B)>();

        for (var i = 0; i < list.Count - 1; i++)
        {
            for (var j = i + 1; j < list.Count; j++)
            {
                result.Add((list[i], list[j]));
            }
        }

        return result;
    }
}
