namespace AdventOfCode.Days;

[Day(2023, 14)]
public class Day14 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateCharGrid();

        grid = TiltPlatform(grid);

        return CalculateLoad(grid).ToString();
    }

    public override string PartTwo(string input)
    {
        var grid = input.CreateCharGrid();
        var seen = new Dictionary<string, int>();
        var totalCycles = 1000000000;

        for (var i = 0; i < totalCycles; i++)
        {
            grid = PerformCycle(grid);

            if (!seen.TryAdd(grid.ToStringGrid(), i))
            {
                var prev = seen[grid.ToStringGrid()] + 1;
                var loopLength = i - (prev - 1);

                var loopCount = (totalCycles - prev) / loopLength;
                var remainingCycles = totalCycles - (prev + (loopCount * loopLength));

                DoMany.Do(remainingCycles, () => grid = PerformCycle(grid));

                return CalculateLoad(grid).ToString();
            }
        }

        throw new InvalidOperationException();
    }

    public char[,] PerformCycle(char[,] grid)
    {
        grid = TiltPlatform(grid);
        grid = grid.RotateClockwise();
        grid = TiltPlatform(grid);
        grid = grid.RotateClockwise();
        grid = TiltPlatform(grid);
        grid = grid.RotateClockwise();
        grid = TiltPlatform(grid);
        grid = grid.RotateClockwise();

        return grid;
    }

    private char[,] TiltPlatform(char[,] grid)
    {
        var width = grid.Width();
        var height = grid.Height();

        for (var x = 0; x < width; x++)
        {
            var topY = 0;

            for (var y = 0; y < height; y++)
            {
                if (grid[x, y] == '#')
                {
                    topY = y + 1;
                }

                if (grid[x, y] == 'O')
                {
                    grid[x, y] = '.';
                    grid[x, topY] = 'O';
                    topY++;
                }
            }
        }

        return grid;
    }

    private long CalculateLoad(char[,] grid)
    {
        var result = 0L;

        for (var y = 0; y < grid.Height(); y++)
        {
            var score = grid.Height() - y;

            for (var x = 0; x < grid.Width(); x++)
            {
                if (grid[x, y] == 'O')
                {
                    result += score;
                }
            }
        }

        return result;
    }
}
