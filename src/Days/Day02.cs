namespace AdventOfCode.Days;

[Day(2023, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var games = input.Lines().Select(line => ParseGame(line));

        var bag = new Dictionary<string, long>();
        bag.Add("red", 12);
        bag.Add("green", 13);
        bag.Add("blue", 14);

        var possibleGames = games.Where(game => IsGamePossible(game, bag));
        var result = possibleGames.Sum(g => g.Id);

        return result.ToString();
    }

    private (long Id, List<Dictionary<string, long>> Handfuls) ParseGame(string line)
    {
        var id = line.Split(':')[0].Words().Last();
        var handfuls = new List<Dictionary<string, long>>();

        var handfulTexts = line.Split(':')[1].Split(';');

        foreach (var h in handfulTexts)
        {
            var words = h.Words().ToList();
            var cubes = new Dictionary<string, long>();

            for (var i = 0; i < words.Count(); i += 2)
            {
                cubes.Add(words[i + 1], long.Parse(words[i]));
            }

            handfuls.Add(cubes);
        }

        return (long.Parse(id), handfuls);
    }

    private bool IsGamePossible((long Id, List<Dictionary<string, long>> Handfuls) game, Dictionary<string, long> bag)
    {
        foreach (var handful in game.Handfuls)
        {
            foreach (var cube in handful)
            {
                if (cube.Value > bag[cube.Key])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override string PartTwo(string input)
    {
        var games = input.Lines().Select(line => ParseGame(line));

        var minCubes = games.Select(game => FindMinimumCubes(game));
        var result = minCubes.Sum(x => CalculatePower(x));

        return result.ToString();
    }

    private decimal CalculatePower(Dictionary<string, long> cubes)
    {
        return cubes.Multiply(x => x.Value);
    }

    private Dictionary<string, long> FindMinimumCubes((long Id, List<Dictionary<string, long>> Handfuls) game)
    {
        var result = new Dictionary<string, long>();

        foreach (var handful in game.Handfuls)
        {
            foreach (var cube in handful)
            {
                if (result.ContainsKey(cube.Key))
                {
                    if (result[cube.Key] < cube.Value)
                    {
                        result[cube.Key] = cube.Value;
                    }
                }
                else
                {
                    result.Add(cube.Key, cube.Value);
                }
            }
        }

        return result;
    }
}
