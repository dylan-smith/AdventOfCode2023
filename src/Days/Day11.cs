
using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var galaxies = map.GetPoints('#').ToList();
        galaxies = ExpandUniverse(map, galaxies, 1);
        var galaxyPairs = galaxies.GetPairs();

        return galaxyPairs.Sum(x => x.A.ManhattanDistance(x.B)).ToString();
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var galaxies = map.GetPoints('#').ToList();
        galaxies = ExpandUniverse(map, galaxies, 999999);

        var galaxyPairs = galaxies.GetPairs();

        return galaxyPairs.Sum(x => (long)x.A.ManhattanDistance(x.B)).ToString();
    }

    private List<Point> ExpandUniverse(char[,] map, List<Point> galaxies, int expansionFactor)
    {
        var totalExpansion = 0;

        for (var y = 0; y < map.Height(); y++)
        {
            if (map.GetRow(y).All(c => c == '.'))
            {
                for (var i = 0; i < galaxies.Count; i++)
                {
                    if ((galaxies[i].Y - totalExpansion) > y)
                    {
                        galaxies[i] = new Point(galaxies[i].X, galaxies[i].Y + expansionFactor);
                    }
                }

                totalExpansion += expansionFactor;
            }
        }

        totalExpansion = 0;

        for (var x = 0; x < map.Width(); x++)
        {
            if (map.GetCol(x).All(c => c == '.'))
            {
                for (var i = 0; i < galaxies.Count; i++)
                {
                    if ((galaxies[i].X - totalExpansion) > x)
                    {
                        galaxies[i] = new Point(galaxies[i].X + expansionFactor, galaxies[i].Y);
                    }
                }

                totalExpansion += expansionFactor;
            }
        }

        return galaxies;
    }
}
