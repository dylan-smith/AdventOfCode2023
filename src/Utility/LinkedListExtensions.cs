namespace AdventOfCode;

public static class LinkedListExtensions
{
    public static LinkedListNode<T> PreviousCircular<T>(this LinkedListNode<T> node)
    {
        return node.Previous ?? node.List.Last;
    }

    public static LinkedListNode<T> PreviousCircular<T>(this LinkedListNode<T> node, int hops)
    {
        var result = node;

        Enumerable.Range(0, hops).ForEach(x => result = result.PreviousCircular());

        return result;
    }

    public static LinkedListNode<T> NextCircular<T>(this LinkedListNode<T> node)
    {
        return node.Next ?? node.List.First;
    }

    public static LinkedListNode<T> NextCircular<T>(this LinkedListNode<T> node, int hops)
    {
        var result = node;

        Enumerable.Range(0, hops).ForEach(x => result = result.NextCircular());

        return result;
    }

    public static void RotateLeft<T>(this LinkedList<T> list)
    {
        var temp = list.First.Value;
        list.RemoveFirst();
        _ = list.AddLast(temp);
    }

    public static void RotateRight<T>(this LinkedList<T> list)
    {
        var temp = list.Last.Value;
        list.RemoveLast();
        _ = list.AddFirst(temp);
    }

    public static void RotateLeft<T>(this LinkedList<T> list, int n)
    {
        if (n >= 0)
        {
            for (var i = 0; i < n; i++)
            {
                list.RotateLeft();
            }
        }
        else
        {
            for (var i = 0; i < Math.Abs(n); i++)
            {
                list.RotateRight();
            }
        }
    }
}
