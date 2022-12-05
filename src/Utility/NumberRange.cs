namespace AdventOfCode;

public class NumberRange
{
    public int Start { get; }
    public int End { get; }

    public NumberRange(int start, int end)
    {
        Start = start;
        End = end;
    }

    public NumberRange(string range)
    {
        var split = range.Split('-');
        Start = int.Parse(split[0]);
        End = int.Parse(split[1]);
    }

    public bool Contains(int number) => number >= Start && number <= End;

    public bool Contains(NumberRange range)
    {
        return range.Start >= Start && range.End <= End;
    }

    public bool Intersects(NumberRange range)
    {
        return range.Start <= End && range.End >= Start;
    }
}
