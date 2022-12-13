using System.Drawing;

namespace AdventOfCode.Days;

[Day(2022, 12)]
public class Day12 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateCharGrid();

        var start = grid.GetPoints(x => x == 'S').Single();
        var target = grid.GetPoints(x => x == 'E').Single();

        grid[start.X, start.Y] = 'a';
        grid[target.X, target.Y] = 'z';

        var result = FindShortestPath(grid, start, target);

        return result.ToString();
    }
    
    public static int FindShortestPath(char[,] grid, Point start, Point end)
    {
        var seen = new HashSet<Point>();
        var steps = 0;

        if (start == end)
        {
            return 0;
        }

        var reachable = grid.GetNeighborPoints(start, false).Where(p => (grid[start.X, start.Y] - p.c) >= -1 && !seen.Contains(p.point)).Select(p => p.point).ToList();

        while (reachable.Any())
        {
            steps++;

            if (reachable.Any(p => p == end))
            {
                return steps;
            }

            reachable.ForEach(r => seen.Add(r));

            var newReachable = new List<Point>();
            
            foreach (var p in reachable)
            {
                newReachable.AddRange(grid.GetNeighborPoints(p, false).Where(n => (grid[p.X, p.Y] - n.c) >= -1 && !seen.Contains(n.point)).Select(p => p.point));
            }

            reachable = newReachable.Distinct().ToList();
        }

        return -1;
    }

    public override string PartTwo(string input)
    {
        var grid = input.CreateCharGrid();

        var start = grid.GetPoints(x => x == 'S').Single();
        var target = grid.GetPoints(x => x == 'E').Single();

        grid[start.X, start.Y] = 'a';
        grid[target.X, target.Y] = 'z';

        var starts = grid.GetPoints(x => x == 'a').ToList();

        var routes = starts.Select(s => FindShortestPath(grid, s, target)).Where(x => x > 0).ToList();

        return routes.Min().ToString();
    }
}
