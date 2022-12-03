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
            var a = sack.Substring(0, sack.Length / 2);
            var b = sack.Substring(sack.Length / 2);

            var done = false;
            foreach (var c in a)
            {
                foreach (var d in b)
                {
                    if (c == d && !done)
                    {
                        score += GetScore(c);
                        done = true;
                    }
                }
            }
        }

        return score.ToString();
    }

    private int GetScore(char c)
    {
        if (c >= 'a' && c <= 'z')
        {
            return c - 'a' + 1;
        }

        if (c >= 'A' && c <= 'Z')
        {
            return c - 'A' + 27;
        }

        throw new Exception();
    }

    public override string PartTwo(string input)
    {
        var sacks = input.Lines().ToList();
        var score = 0;

        while (sacks.Count >= 3)
        { 
            var group = sacks.Take(3).ToList();
            sacks = sacks.Skip(3).ToList();
        
            var done = false;
            foreach (var c in group.First())
            {
                if (group.ElementAt(1).Contains(c) && group.ElementAt(2).Contains(c) && !done)
                {
                    score += GetScore(c);
                    done = true;
                }
            }
        }

        return score.ToString();
    }
}
