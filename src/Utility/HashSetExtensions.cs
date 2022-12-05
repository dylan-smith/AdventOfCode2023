namespace AdventOfCode;

public static class HashSetExtensions
{
    public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> collection)
    {
        collection.ForEach(x => set.Add(x));
    }
}
