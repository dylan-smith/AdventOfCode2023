using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateCharGrid();

        var symbols = GetSymbols(grid);
        var numbers = GetNumbers(grid);

        var partNumbers = numbers.Where(n => IsPartNumber(n.start, n.end, symbols)).ToList();

        return partNumbers.Sum(x => x.number).ToString();
    }

    private List<(long number, Point start, Point end)> GetNumbers(char[,] grid)
    {
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
                }
            }

            if (!curNumber.IsNullOrWhiteSpace())
            {
                numbers.Add((long.Parse(curNumber), new Point(grid.Width() - curNumber.Length, y), new Point(grid.Width() - 1, y)));
            }
        }

        return numbers;
    }

    private List<(char symbol, Point location)> GetSymbols(char[,] grid)
    {
        var symbols = new List<(char symbol, Point location)>();

        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                if (!grid[x, y].IsNumeric() && grid[x, y] != '.')
                {
                    symbols.Add((grid[x, y], new Point(x, y)));
                }
            }
        }

        return symbols;
    }

    private bool IsPartNumber(Point start, Point end, List<(char symbol, Point location)> symbols)
    {
        for (var x = start.X; x <= end.X; x++)
        {
            if (symbols.Any(s => s.location.GetNeighbors(true).Any(n => n.X == x && n.Y == start.Y)))
            {
                return true;
            }
        }

        return false;
    }

    public override string PartTwo(string input)
    {
        var grid = input.CreateCharGrid();

        var symbols = GetSymbols(grid);
        var numbers = GetNumbers(grid);

        var gears = symbols.Where(x => IsGear(x, numbers));
        var result = gears.Sum(g => GetGearRatio(g.location, numbers));

        return result.ToString();
    }

    private long GetGearRatio(Point location, List<(long number, Point start, Point end)> numbers)
    {
        return GetAdjacentNumbers(location, numbers).Multiply(x => x.number);
    }

    private bool IsGear((char symbol, Point location) symbol, List<(long number, Point start, Point end)> numbers)
    {
        var adjacentNumbers = GetAdjacentNumbers(symbol.location, numbers);
        return adjacentNumbers.Count() == 2;
    }

    private IEnumerable<(long number, Point start, Point end)> GetAdjacentNumbers(Point location, List<(long number, Point start, Point end)> numbers)
    {
        return numbers.Where(n => IsNumberAdjacent(n.start, n.end, location));
    }

    private bool IsNumberAdjacent(Point start, Point end, Point location)
    {
        for (var x = start.X; x <= end.X; x++)
        {
            if (new Point(x, start.Y).GetNeighbors(true).Any(n => n == location))
            {
                return true;
            }
        }

        return false;
    }
}
