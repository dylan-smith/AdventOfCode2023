using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Days;

[Day(2023, 13)]
public class Day13 : BaseDay
{
    public override string PartOne(string input)
    {
        var paragraphs = input.Paragraphs();
        var result = 0L;

        foreach (var paragraph in paragraphs)
        {
            var grid = paragraph.CreateCharGrid();

            for (var x = 0; x < grid.Width() - 1; x++)
            {
                if (CanReflectVertical(grid, x))
                {
                    result += x + 1;
                }
            }

            for (var y = 0; y < grid.Height() - 1; y++)
            {
                if (CanReflectHorizontal(grid, y))
                {
                    result += 100 * (y + 1);
                }

            }
        }

        return result.ToString();
    }

    private bool CanReflectVertical(char[,] grid, int mirror)
    {
        for (var x = 0; x <= mirror; x++)
        {
            var mirrorX = mirror + (mirror - x) + 1;

            if (mirrorX < grid.Width())
            {
                for (var y = 0; y < grid.Height(); y++)
                {
                    if (grid[x, y] != grid[mirrorX, y])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool CanReflectHorizontal(char[,] grid, int mirror)
    {
        for (var y = 0; y <= mirror; y++)
        {
            var mirrorY = mirror + (mirror - y) + 1;

            if (mirrorY < grid.Height())
            {
                for (var x = 0; x < grid.Width(); x++)
                {
                    if (grid[x, y] != grid[x, mirrorY])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private int CanReflectVertical2(char[,] grid, int mirror)
    {
        var result = 0;

        for (var x = 0; x <= mirror; x++)
        {
            var mirrorX = mirror + (mirror - x) + 1;

            if (mirrorX < grid.Width())
            {
                for (var y = 0; y < grid.Height(); y++)
                {
                    if (grid[x, y] != grid[mirrorX, y])
                    {
                        result++;
                    }
                }
            }
        }

        return result;
    }

    private int CanReflectHorizontal2(char[,] grid, int mirror)
    {
        var result = 0;

        for (var y = 0; y <= mirror; y++)
        {
            var mirrorY = mirror + (mirror - y) + 1;

            if (mirrorY < grid.Height())
            {
                for (var x = 0; x < grid.Width(); x++)
                {
                    if (grid[x, y] != grid[x, mirrorY])
                    {
                        result++;
                    }
                }
            }
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        var paragraphs = input.Paragraphs();
        var result = 0L;

        foreach (var paragraph in paragraphs)
        {
            var grid = paragraph.CreateCharGrid();

            for (var x = 0; x < grid.Width() - 1; x++)
            {
                var smudgeCount = CanReflectVertical2(grid, x);
                
                if (smudgeCount == 1)
                {
                    result += x + 1;
                }
            }

            for (var y = 0; y < grid.Height() - 1; y++)
            {
                var smudgeCount = CanReflectHorizontal2(grid, y);

                if (smudgeCount == 1)
                {
                    result += 100 * (y + 1);
                }

            }
        }

        return result.ToString();
    }
}
