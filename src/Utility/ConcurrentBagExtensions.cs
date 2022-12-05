using System.Collections.Concurrent;

namespace AdventOfCode;

public static class ConcurrentBagExtensions
{
    public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> toAdd)
    {
        foreach (var element in toAdd)
        {
            bag.Add(element);
        }
    }
}
