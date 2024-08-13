
using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();

        for (var y = 0; y < map.Height(); y++)
        {
            if (map.GetRow(y).All(c => c == '.'))
            {
                map = map.InsertRow(y, '.');
                y++;
            }
        }

        for (var x = 0; x < map.Width(); x++)
        {
            if (map.GetCol(x).All(c => c == '.'))
            {
                map = map.InsertCol(x, '.');
                x++;
            }
        }

        var galaxies = FindGalaxies(map);
        var galaxyPairs = GetPairs(galaxies);

        return galaxyPairs.Sum(x => x.A.ManhattanDistance(x.B)).ToString();
    }

    private List<(Point A, Point B)> GetPairs(List<Point> galaxies)
    {
        var result = new List<(Point A, Point B)>();

        for (var i = 0; i < galaxies.Count - 1; i++)
        {
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                result.Add((galaxies[i], galaxies[j]));
            }
        }

        return result;
    }

    private List<Point> FindGalaxies(char[,] map)
    {
        return map.GetPoints('#').ToList();
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var galaxies = FindGalaxies(map);
        var expansion = 999999;
        var totalExpansion = 0;

        for (var y = 0; y < map.Height(); y++)
        {
            if (map.GetRow(y).All(c => c == '.'))
            {
                for (var i = 0; i < galaxies.Count; i++)
                {
                    if ((galaxies[i].Y - totalExpansion) > y)
                    {
                        galaxies[i] = new Point(galaxies[i].X, galaxies[i].Y + expansion);
                    }
                }

                totalExpansion += expansion;
            }
        }

        totalExpansion = 0;

        for (var x = 0; x < map.Width(); x++)
        {
            if (map.GetCol(x).All(c => c == '.'))
            {
                for (var i = 0; i < galaxies.Count; i++)
                {
                    if ((galaxies[i].X  - totalExpansion) > x)
                    {
                        galaxies[i] = new Point(galaxies[i].X + expansion, galaxies[i].Y);
                    }
                }

                totalExpansion += expansion;
            }
        }

        foreach (var galaxy in galaxies)
        {
            Log(galaxy.ToString());
        }

        var galaxyPairs = GetPairs(galaxies);
        var foo = galaxyPairs.Select(x => (long)x.A.ManhattanDistance(x.B)).ToList();

        return galaxyPairs.Sum(x => (long)x.A.ManhattanDistance(x.B)).ToString();
    }
}
