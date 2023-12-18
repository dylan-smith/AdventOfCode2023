
using System.Diagnostics;

namespace AdventOfCode.Days;

[Day(2023, 6)]
public class Day06 : BaseDay
{
    public override string PartOne(string input)
    {
        var times = input.Lines().First().Words().Skip(1).Longs();
        var distances = input.Lines().Last().Words().Skip(1).Longs();
        var result = 1L;

        for (var race = 0; race < times.Count(); race++)
        {
            var wins = 0L;

            for (var buttonTime = 1; buttonTime < times.ElementAt(race); buttonTime++)
            {
                var distance = GetTotalDistance(buttonTime, times.ElementAt(race));

                if (distance > distances.ElementAt(race))
                {
                    wins++;
                }
            }

            result *= wins;
        }

        return result.ToString();
    }

    private long GetTotalDistance(int buttonTime, long raceTime)
    {
        return buttonTime * (raceTime - buttonTime);
    }

    public override string PartTwo(string input)
    {
        var raceTime = 59688274L;
        var raceDistance = 543102016641022L;
        var wins = 0L;

        for (var buttonTime = 1; buttonTime < raceTime; buttonTime++)
        {
            var distance = GetTotalDistance(buttonTime, raceTime);

            if (distance > raceDistance)
            {
                wins++;
            }
        }

        return wins.ToString();
    }
}
