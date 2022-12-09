using System.Drawing;

namespace AdventOfCode.Days;

[Day(2022, 8)]
public class Day08 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateIntGrid();

        var score = 0;

        foreach (var point in grid.GetPoints())
        {
            var height = grid[point.X, point.Y];

            var good = true;

            for (var x = 0; x < point.X; x++)
            {
                if (grid[x, point.Y] >= height)
                {
                    good = false;
                    break;
                }
            }

            if (good)
            {
                score++;
                continue;
            }

            good = true;
            for (var x = point.X + 1; x < grid.Width(); x++)
            {
                if (grid[x, point.Y] >= height)
                {
                    good = false;
                    break;
                }
            }

            if (good)
            {
                score++;
                continue;
            }

            good = true;
            for (var y = 0; y < point.Y; y++)
            {
                if (grid[point.X, y] >= height)
                {
                    good = false;
                    break;
                }
            }

            if (good)
            {
                score++;
                continue;
            }

            good = true;
            for (var y = point.Y + 1; y < grid.Width(); y++)
            {
                if (grid[point.X, y] >= height)
                {
                    good = false;
                    break;
                }
            }

            if (good)
            {
                score++;
                continue;
            }
        }

        return score.ToString();
    }

    public override string PartTwo(string input)
    {
        var grid = input.CreateIntGrid();

        var score = grid.GetPoints().Max(p => GetScenicScore(p, grid));

        return score.ToString();
    }

    private int GetScenicScore(Point p, int[,] grid)
    {
        var left = 0;
        var right = 0;
        var up = 0;
        var down = 0;

        var height = grid[p.X, p.Y];

        for (var x = p.X - 1; x >= 0; x--)
        {
            left++;

            if (grid[x, p.Y] >= height)
            {
                break;
            }
        }

        for (var x = p.X + 1; x < grid.Width(); x++)
        {
            right++;

            if (grid[x, p.Y] >= height)
            {
                break;
            }
        }

        for (var y = p.Y - 1; y >= 0; y--)
        {
            up++;

            if (grid[p.X, y] >= height)
            {
                break;
            }
        }

        for (var y = p.Y + 1; y < grid.Height(); y++)
        {
            down++;

            if (grid[p.X, y] >= height)
            {
                break;
            }
        }

        return left * right * up * down;
    }
}
