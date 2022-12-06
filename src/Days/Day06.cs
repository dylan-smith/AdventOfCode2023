namespace AdventOfCode.Days;

[Day(2022, 6)]
public class Day06 : BaseDay
{
    public override string PartOne(string input)
    {
        var count = 3;
        
        foreach (var window in input.Window(4))
        {
            count++;

            if (window.Distinct().Count() == 4)
            {
                return count.ToString();
            }
        }

        throw new Exception();
    }

    public override string PartTwo(string input)
    {
        var count = 13;

        foreach (var window in input.Window(14))
        {
            count++;

            if (window.Distinct().Count() == 14)
            {
                return count.ToString();
            }
        }

        throw new Exception();
    }
}
