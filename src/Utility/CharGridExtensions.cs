using System.Drawing;
using System.Text;

namespace AdventOfCode;

public static class CharGridExtensions
{
    public static char[,] CreateCharGrid(this string input)
    {
        var lines = input.Lines().ToList();
        var result = new char[lines[0].Length, lines.Count];

        for (var y = 0; y < lines.Count; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                result[x, y] = lines[y][x];
            }
        }

        return result;
    }

    public static int[,] CreateIntGrid(this string input)
    {
        var lines = input.Lines().ToList();
        var result = new int[lines[0].Length, lines.Count];

        for (var y = 0; y < lines.Count; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                result[x, y] = int.Parse(lines[y][x].ToString());
            }
        }

        return result;
    }

    public static string GetString(this char[,] grid)
    {
        var sb = new StringBuilder(grid.GetLength(0) * grid.GetLength(1) + (Environment.NewLine.Length * grid.GetLength(1)));

        for (var y = 0; y <= grid.GetUpperBound(1); y++)
        {
            for (var x = 0; x <= grid.GetUpperBound(0); x++)
            {
                _ = sb.Append(grid[x, y]);
            }

            _ = sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    public static IEnumerable<Point> GetPoints<T>(this T[,] grid)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                yield return new Point(x, y);
            }
        }
    }

    public static IEnumerable<Point> GetPoints<T>(this T[,] grid, T filter)
    {
        return GetPoints(grid).Where(p => EqualityComparer<T>.Default.Equals(grid[p.X, p.Y], filter));
    }

    public static IEnumerable<Point> GetPoints(this char[,] grid, char filter)
    {
        return GetPoints(grid).Where(p => grid[p.X, p.Y] == filter);
    }

    public static IEnumerable<Point> GetPoints(this int[,] grid, int filter)
    {
        return GetPoints(grid).Where(p => grid[p.X, p.Y] == filter);
    }

    public static IEnumerable<Point> GetPoints<T>(this T[,] grid, Func<T, bool> filter)
    {
        return GetPoints(grid).Where(p => filter(grid[p.X, p.Y]));
    }

    public static IEnumerable<Point> GetPoints<T>(this T[,] grid, Func<Point, bool> filter)
    {
        return GetPoints(grid).Where(filter);
    }

    public static void Replace<T>(this T[,] grid, T filter, T value)
    {
        foreach (var p in grid.GetPoints(filter))
        {
            grid[p.X, p.Y] = value;
        }
    }

    public static void Replace<T>(this T[,] grid, Func<T, bool> filter, T value)
    {
        foreach (var p in grid.GetPoints(filter))
        {
            grid[p.X, p.Y] = value;
        }
    }

    public static void Replace<T>(this T[,] grid, Func<Point, bool> filter, T value)
    {
        foreach (var p in grid.GetPoints(filter))
        {
            grid[p.X, p.Y] = value;
        }
    }

    public static int Count<T>(this T[,] grid, T match)
    {
        var result = 0;

        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                if (EqualityComparer<T>.Default.Equals(grid[x, y], match))
                {
                    result++;
                }
            }
        }

        return result;
    }

    public static T[,] Clone<T>(this T[,] grid, Func<int, int, T, T> transform)
    {
        var result = new T[grid.GetLength(0), grid.GetLength(1)];

        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                result[x, y] = transform(x, y, grid[x, y]);
            }
        }

        return result;
    }

    public static T[,] Clone<T>(this T[,] grid, Func<T, T> transform)
    {
        var result = new T[grid.GetLength(0), grid.GetLength(1)];

        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                result[x, y] = transform(grid[x, y]);
            }
        }

        return result;
    }

    public static T[,] Clone<T>(this T[,] grid)
    {
        return grid.Clone(c => c);
    }

    public static IEnumerable<T> GetNeighbors<T>(this T[,] map, int x, int y, bool includeDiagonals)
    {
        var neighbors = new Point(x, y).GetNeighbors(includeDiagonals);

        foreach (var n in neighbors)
        {
            if (n.X >= 0 && n.X <= map.GetUpperBound(0) && n.Y >= 0 && n.Y <= map.GetUpperBound(1))
            {
                yield return map[n.X, n.Y];
            }
        }
    }

    public static IEnumerable<T> GetNeighbors<T>(this T[,] map, Point pos, bool includeDiagonals)
    {
        return map.GetNeighbors(pos.X, pos.Y, includeDiagonals);
    }

    public static IEnumerable<T> GetNeighbors<T>(this T[,] map, int x, int y)
    {
        return map.GetNeighbors(x, y, true);
    }

    public static IEnumerable<T> GetNeighbors<T>(this T[,] map, Point pos)
    {
        return map.GetNeighbors(pos, true);
    }

    public static IEnumerable<(Point point, T c)> GetNeighborPoints<T>(this T[,] map, int x, int y)
    {
        return map.GetNeighborPoints(new Point(x, y));
    }

    public static IEnumerable<(Point point, T c)> GetNeighborPoints<T>(this T[,] map, int x, int y, bool includeDiagonals)
    {
        return map.GetNeighborPoints(new Point(x, y), includeDiagonals);
    }

    public static IEnumerable<(Point point, T c)> GetNeighborPoints<T>(this T[,] map, Point p)
    {
        return map.GetNeighborPoints(p, false);
    }

    public static IEnumerable<(Point point, T c)> GetNeighborPoints<T>(this T[,] map, Point p, bool includeDiagonals)
    {
        var neighbors = p.GetNeighbors(includeDiagonals);

        foreach (var n in neighbors)
        {
            if (n.X >= 0 && n.X <= map.GetUpperBound(0) && n.Y >= 0 && n.Y <= map.GetUpperBound(1))
            {
                yield return (n, map[n.X, n.Y]);
            }
        }
    }

    public static Dictionary<Point, int> FindShortestPaths<T>(this T[,] grid, Func<T, bool> validMove, Point start)
    {
        var steps = 0;
        var result = new Dictionary<Point, int>
        {
            { start, 0 }
        };

        var reachable = start.GetNeighbors(false).Where(p => validMove(grid[p.X, p.Y]) && !result.ContainsKey(p)).ToList();

        while (reachable.Any())
        {
            steps++;

            reachable.ForEach(r => result.Add(r, steps));

            var newReachable = new List<Point>();
            reachable.ForEach(p => newReachable.AddRange(p.GetNeighbors(false).ToList()));
            reachable = newReachable.Where(p => validMove(grid[p.X, p.Y]) && !result.ContainsKey(p)).Distinct().ToList();
        }

        return result;
    }

    public static int FindShortestPath<T>(this T[,] grid, Func<T, bool> validMove, Point start, Point end)
    {
        var seen = new HashSet<Point>();
        var steps = 0;

        if (start == end)
        {
            return 0;
        }

        var reachable = start.GetNeighbors(false).Where(p => validMove(grid[p.X, p.Y]) && !seen.Contains(p)).ToList();

        while (reachable.Any())
        {
            steps++;

            if (reachable.Any(p => p == end))
            {
                return steps;
            }

            reachable.ForEach(r => seen.Add(r));

            var newReachable = new List<Point>();
            reachable.ForEach(p => newReachable.AddRange(p.GetNeighbors(false).ToList()));
            reachable = newReachable.Where(p => validMove(grid[p.X, p.Y]) && !seen.Contains(p)).Distinct().ToList();
        }

        return -1;
    }

    public static bool IsValidPoint<T>(this T[,] grid, Point point)
    {
        if (point.X >= 0 && point.X < grid.GetLength(0))
        {
            if (point.Y >= 0 && point.Y < grid.GetLength(1))
            {
                return true;
            }
        }

        return false;
    }

    public static string GetRow(this char[,] grid, int row)
    {
        var sb = new StringBuilder();

        for (var x = 0; x <= grid.GetUpperBound(0); x++)
        {
            _ = sb.Append(grid[x, row]);
        }

        return sb.ToString();
    }

    public static string GetColumn(this char[,] grid, int col)
    {
        var sb = new StringBuilder();

        for (var y = 0; y <= grid.GetUpperBound(1); y++)
        {
            _ = sb.Append(grid[col, y]);
        }

        return sb.ToString();
    }

    public static string GetBottomRow(this char[,] grid)
    {
        return grid.GetRow(grid.GetUpperBound(1));
    }

    public static string GetTopRow(this char[,] grid)
    {
        return grid.GetRow(0);
    }

    public static string GetLeftColumn(this char[,] grid)
    {
        return grid.GetColumn(0);
    }

    public static string GetRightColumn(this char[,] grid)
    {
        return grid.GetColumn(grid.GetUpperBound(0));
    }

    public static IEnumerable<string> GetRows(this char[,] grid)
    {
        for (var y = 0; y <= grid.GetUpperBound(1); y++)
        {
            yield return grid.GetRow(y);
        }
    }

    public static IEnumerable<string> GetColumns(this char[,] grid)
    {
        for (var x = 0; x <= grid.GetUpperBound(0); x++)
        {
            yield return grid.GetColumn(x);
        }
    }

    public static int Width<T>(this T[,] grid)
    {
        return grid.GetUpperBound(0) + 1;
    }

    public static int Height<T>(this T[,] grid)
    {
        return grid.GetUpperBound(1) + 1;
    }

    public static T GetItemWithWrapping<T>(this T[,] grid, int x, int y)
    {
        return grid[x % grid.Width(), y % grid.Height()];
    }

    public static T[,] RotateClockwise<T>(this T[,] grid, int count)
    {
        var result = grid;

        for (var i = 0; i < count; i++)
        {
            result = result.RotateClockwise();
        }

        return result;
    }

    public static T[,] RotateClockwise<T>(this T[,] grid)
    {
        var width = grid.Width();
        var height = grid.Height();

        var result = new T[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                result[width - y - 1, x] = grid[x, y];
            }
        }

        return result;
    }

    public static T[,] FlipVertical<T>(this T[,] grid)
    {
        var result = new T[grid.Width(), grid.Height()];

        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                result[x, grid.Height() - y - 1] = grid[x, y];
            }
        }

        return result;
    }

    public static T[,] FlipHorizontal<T>(this T[,] grid)
    {
        var result = new T[grid.Width(), grid.Height()];

        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                result[grid.Width() - x - 1, y] = grid[x, y];
            }
        }

        return result;
    }

    public static void Increment(this int[,] grid)
    {
        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                grid[x, y]++;
            }
        }
    }

    public static void Initialize<T>(this T[,] grid, T value)
    {
        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                grid[x, y] = value;
            }
        }
    }

    public static string ToStringGrid(this char[,] grid)
    {
        var result = new StringBuilder();
        
        for (var y = 0; y < grid.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                result.Append(grid[x, y]);
            }

            result.Append(Environment.NewLine);
        }

        return result.ToString();
    }

    public static T[,] InsertRow<T>(this T[,] grid, int after, T value)
    {
        var result = new T[grid.Width(), grid.Height() + 1];

        for (var y = 0; y <= after; y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                result[x, y] = grid[x, y];
            }
        }

        for (var x = 0; x < grid.Width(); x++)
        {
            result[x, after + 1] = value;
        }

        for (var y = after + 2; y < result.Height(); y++)
        {
            for (var x = 0; x < grid.Width(); x++)
            {
                result[x, y] = grid[x, y - 1];
            }
        }

        return result;
    }

    public static T[,] InsertCol<T>(this T[,] grid, int after, T value)
    {
        var result = new T[grid.Width() + 1, grid.Height()];

        for (var x = 0; x <= after; x++)
        {
            for (var y = 0; y < grid.Height(); y++)
            {
                result[x, y] = grid[x, y];
            }
        }

        for (var y = 0; y < grid.Height(); y++)
        {
            result[after + 1, y] = value;
        }

        for (var x = after + 2; x < result.Width(); x++)
        {
            for (var y = 0; y < grid.Height(); y++)
            {
                result[x, y] = grid[x - 1, y];
            }
        }

        return result;
    }
}