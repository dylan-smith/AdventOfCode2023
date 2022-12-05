namespace AdventOfCode.Days;

[Day(2022, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var elfPairs = input.ParseLines(l => l.Words().Select(w => new NumberRange(w)));

        var score = elfPairs.Count(elf => elf.First().Contains(elf.Last()) || elf.Last().Contains(elf.First()));

        return score.ToString();
    }

    public override string PartTwo(string input)
    {
        var elfPairs = input.ParseLines(l => l.Words().Select(w => new NumberRange(w)));

        var score = elfPairs.Count(elf => elf.First().Intersects(elf.Last()));
        
        return score.ToString();
    }
}
