using System.Threading.Tasks.Sources;

namespace AdventOfCode.Days;

[Day(2022, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var lines = input.Lines();
        var wins = 0;
        var draws = 0;
        var score = 0;

        foreach (var line in lines)
        {
            var words = line.Words();

            var a = words.First();
            var b = words.Last();

            if (a == "A" && b == "X")
            {
                draws++;
            }

            if (a == "A" && b == "Y")
            {
                wins++;
            }

            if (a == "B" && b == "Y")
            {
                draws++;
            }

            if (a == "B" && b == "Z")
            {
                wins++;
            }

            if (a == "C" && b == "X")
            {
                wins++;
            }

            if (a == "C" && b == "Z")
            {
                draws++;
            }

            if (b == "X")
            {
                score += 1;
            }

            if (b == "Y")
            {
                score += 2;
            }

            if (b == "Z")
            {
                score += 3;
            }
        }

        score += (wins * 6) + (draws * 3);

        return score.ToString();
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines();
        var wins = 0;
        var draws = 0;
        var score = 0;

        foreach (var line in lines)
        {
            var words = line.Words();

            var a = words.First();
            var b = words.Last();

            if (b == "Y")
            {
                draws++;
            }

            if (b == "Z")
            {
                wins++;
            }

            if (a == "A" && b == "X")
            {
                score += 3;
            }

            if (a == "A" && b == "Y")
            {
                score += 1;
            }

            if (a == "A" && b == "Z")
            {
                score += 2;
            }

            if (a == "B" && b == "X")
            {
                score++;
            }

            if (a == "B" && b == "Y")
            {
                score += 2;
            }

            if (a == "B" && b == "Z")
            {
                score += 3;
            }

            if (a == "C" && b == "X")
            {
                score += 2;
            }

            if (a == "C" && b == "Y")
            {
                score += 3;
            }

            if (a == "C" && b == "Z")
            {
                score++;
            }
        }

        score += (wins * 6) + (draws * 3);

        return score.ToString();
    }
}
