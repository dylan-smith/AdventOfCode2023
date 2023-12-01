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

    public static bool IsNumeric(this char c)
    {
        return c is >= '0' and <= '9';
    }

    public static int ParseInt(this char c)
    {
        return int.Parse(c.ToString());
    }
}