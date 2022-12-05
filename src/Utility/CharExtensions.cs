namespace AdventOfCode;

public static class CharExtensions
{
    public static bool IsLower(this char c)
    {
        return c is >= 'a' and <= 'z';
    }

    public static bool IsUpper(this char c)
    {
        return c is >= 'A' and <= 'Z';
    }
}