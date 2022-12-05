namespace AdventOfCode.Days;

[Day(2022, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var sacks = input.Lines().ToList();
        var score = 0;
        
        foreach (var sack in sacks)
        {
            var firstCompartment = sack[..(sack.Length / 2)];
            var secondCompartment = sack[(sack.Length / 2)..];

            score += GetScore(firstCompartment.First(x => secondCompartment.Contains(x)));
        }

        return score.ToString();
    }

    private int GetScore(char c) => c.IsLower() ? c - 'a' + 1 : c - 'A' + 27;

    public override string PartTwo(string input)
    {
        var sacks = input.Lines();
        var groups = sacks.Chunk(3);
        var badges = groups.Select(g => g.First().First(c => g.ElementAt(1).Contains(c) && g.Last().Contains(c)));
        
        return badges.Sum(b => GetScore(b)).ToString();
    }
}
