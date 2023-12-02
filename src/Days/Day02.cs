namespace AdventOfCode.Days;

[Day(2023, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var games = input.Lines().Select(line => new Game(line));

        var bag = new Dictionary<string, long>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        var possibleGames = games.Where(game => game.IsGamePossible(bag));
        var result = possibleGames.Sum(g => g.Id);

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        var games = input.Lines().Select(line => new Game(line));

        var minCubes = games.Select(game => game.FindMinimumCubes());
        var result = minCubes.Sum(CalculatePower);

        return result.ToString();
    }

    private decimal CalculatePower(Dictionary<string, long> cubes)
    {
        return cubes.Multiply(x => x.Value);
    }

    private class Game
    {
        public long Id { get; set; }
        public List<Dictionary<string, long>> Handfuls { get; set; }

        public Game(string input)
        {
            Id = long.Parse(input.Split(':')[0].Words().Last());
            Handfuls = new List<Dictionary<string, long>>();

            var handfulTexts = input.Split(':')[1].Split(';');

            foreach (var h in handfulTexts)
            {
                var words = h.Words().ToList();
                var cubes = new Dictionary<string, long>();

                for (var i = 0; i < words.Count; i += 2)
                {
                    cubes.Add(words[i + 1], long.Parse(words[i]));
                }

                Handfuls.Add(cubes);
            }
        }

        public bool IsGamePossible(Dictionary<string, long> bag)
        {
            foreach (var handful in Handfuls)
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

        public Dictionary<string, long> FindMinimumCubes()
        {
            var result = new Dictionary<string, long>();

            foreach (var handful in Handfuls)
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
}
