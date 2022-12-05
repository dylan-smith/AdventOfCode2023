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
}
