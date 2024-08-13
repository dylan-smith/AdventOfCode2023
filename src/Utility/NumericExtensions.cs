namespace AdventOfCode;

public static class NumericExtensions
{
    public static bool IsPrime(this int number)
    {
        var sqrt = Math.Floor(Math.Sqrt((double)number));

        for (var x = 2; x <= sqrt; x++)
        {
            if (number % x == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static void Times(this int number, Action action)
    {
        for (var i = 0; i < number; i++)
        {
            action();
        }
    }

    public static void Times(this int number, Action<int> action)
    {
        for (var i = 0; i < number; i++)
        {
            action(i);
        }
    }

    public static string ToBinary(this int number)
    {
        return Convert.ToString(number, 2);
    }

    public static string ToBinary(this long number)
    {
        return Convert.ToString(number, 2);
    }

    public static string ToHex(this int number)
    {
        return Convert.ToString(number, 16);
    }

    public static string ToHex(this long number)
    {
        return Convert.ToString(number, 16);
    }
}