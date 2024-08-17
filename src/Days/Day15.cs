
namespace AdventOfCode.Days;

[Day(2023, 15)]
public class Day15 : BaseDay
{
    public override string PartOne(string input)
    {
        var commands = input.Words();
        var result = 0L;

        foreach (var command in commands)
        {
            result += CalcHash(command);
        }

        return result.ToString();
    }

    private long CalcHash(string command)
    {
        var hash = 0;

        foreach (var c in command)
        {
            hash += c;
            hash *= 17;
            hash %= 256;
        }

        return hash;
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
