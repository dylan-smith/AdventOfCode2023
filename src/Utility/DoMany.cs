namespace AdventOfCode;

public static class DoMany
{
    public static void Do(int count, Action action)
    {
        for (var i = 0; i < count; i++)
        {
            action();
        }
    }

    public static void Do(int count, Action<int> action)
    {
        for (var i = 0; i < count; i++)
        {
            action(i);
        }
    }
}
