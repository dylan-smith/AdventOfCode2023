using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateCharGrid();
        var symbols = new List<Point>();
        var numbers = new List<(long number, Point start, Point end)>();

        for (var y = 0; y < grid.Height(); y++)
        {
            var curNumber = string.Empty;

            for (var x = 0; x < grid.Width(); x++)
            {
                if (grid[x, y].IsNumeric())
                {
                    curNumber += grid[x, y].ToString();
                }
                else
                {
                    if (!curNumber.IsNullOrWhiteSpace())
                    {
                        numbers.Add((long.Parse(curNumber), new Point(x - curNumber.Length, y), new Point(x - 1, y)));
                        curNumber = string.Empty;
                    }

                    if (grid[x, y] != '.')
                    {
                        symbols.Add(new Point(x, y));
                    }
                }
            }

            if (!curNumber.IsNullOrWhiteSpace())
            {
                numbers.Add((long.Parse(curNumber), new Point(grid.Width() - curNumber.Length, y), new Point(grid.Width() - 1, y)));
            }
        }

        var partNumbers = numbers.Where(n => IsPartNumber(n.start, n.end, symbols)).ToList();

        return partNumbers.Sum(x => x.number).ToString();
    }

    private bool IsPartNumber(Point start, Point end, List<Point> symbols)
    {
        for (var x = start.X; x <= end.X; x++)
        {
            if (symbols.Any(s => s.GetNeighbors(true).Any(n => n.X == x && n.Y == start.Y)))
            {
                return true;
            }
        }

        return false;
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
