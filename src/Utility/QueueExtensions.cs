namespace AdventOfCode
{
    public static class QueueExtensions
    {
        public static void Enqueue<T>(this Queue<T> q, IEnumerable<T> items)
        {
            items.ForEach(i => q.Enqueue(i));
        }
    }
}