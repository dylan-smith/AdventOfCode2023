using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AdventOfCode.Days;

[Day(2023, 10)]
public class Day10 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var pipePoints = FindPipePoints(map);

        return pipePoints.Max(x => x.Value).ToString();
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var pipePoints = FindPipePoints(map);
        map.Replace(p => !pipePoints.ContainsKey(p), '.');
        map.Replace('S', '|');

        var nestCount = 0L;
        var nestPoints = new List<Point>();

        Log(map.ToStringGrid());

        foreach (var p in map.GetPoints())
        {
            if (map[p.X, p.Y] == '.')
            {
                if (IsContainedByPipe(p, map))
                {
                    nestCount++;
                    nestPoints.Add(p);
                }
            }
        }

        return nestCount.ToString();
    }

    private bool IsContainedByPipe(Point p, char[,] map)
    {
        var accessiblePoints = new HashSet<(Point Point, bool Bottom)>();

        var newPoints = new List<(Point Point, bool Bottom)>();
        newPoints.Add((p, false));
        accessiblePoints.Add((p, false));

        while (newPoints.Any())
        {
            var nextPoints = new List<(Point Point, bool Bottom)>();

            foreach (var newPoint in newPoints)
            {
                var neighbors = newPoint.Point.GetNeighbors(includeDiagonals: false);

                foreach (var neighbor in neighbors)
                {
                    var (valid, bottom) = IsValidMove(newPoint, neighbor, map);

                    if (valid)
                    {
                        if (!map.IsValidPoint(neighbor))
                        {
                            return false;
                        }

                        if (accessiblePoints.Add((neighbor, bottom)))
                        {
                            nextPoints.Add((neighbor, bottom));
                        }
                    }
                }
            }

            newPoints = nextPoints;

            //if (p.X == 14 && p.Y == 3)
            //{
            //    var map2 = map.Clone((x, y, c) =>
            //    {
            //        if (accessiblePoints.Any(p => p.Point.X == x && p.Point.Y == y))
            //        {
            //            if (accessiblePoints.Any(p => p.Point.X == x && p.Point.Y == y && p.Bottom) && accessiblePoints.Any(p => p.Point.X == x && p.Point.Y == y && !p.Bottom))
            //            {
            //                return 'X';
            //            }

            //            if (accessiblePoints.Any(p => p.Point.X == x && p.Point.Y == y && p.Bottom))
            //            {
            //                return 'B';
            //            }

            //            return 'T';
            //        }

            //        return c;
            //    });
            //    Log("===================");
            //    Log(map2.ToStringGrid());
            //}
        }

        return true;
    }

    private (bool valid, bool bottom) IsValidMove((Point Point, bool Bottom) start, Point end, char[,] map)
    {
        var startValue = map[start.Point.X, start.Point.Y];
        var endValue = '.';

        if (map.IsValidPoint(end))
        {
            endValue = map[end.X, end.Y];
        }

        if (startValue == '.')
        {
            if (endValue == '.')
            {
                return (true, false);
            }

            if (endValue == '-')
            {
                if (start.Point.Y < end.Y)
                {
                    return (true, false);
                }
                else
                {
                    return (true, true);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X < end.X)
                {
                    return (true, true);
                }
                else
                {
                    return (true, false);
                }
            }

            if (endValue == 'L')
            {
                return (true, true);
            }

            if (endValue == 'J')
            {
                return (true, true);
            }

            if (endValue == '7')
            {
                return (true, false);
            }

            if (endValue == 'F')
            {
                return (true, false);
            }
        }

        if (startValue == '-')
        {
            if (endValue == '.')
            {
                if (end.Y < start.Point.Y)
                {
                    return (!start.Bottom, false);
                }
                else
                {
                    return (start.Bottom, false);
                }
            }

            if (endValue == '-')
            {
                if (start.Point.Y == end.Y)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    if (start.Point.Y > end.Y)
                    {
                        return (!start.Bottom, true);
                    }
                    else
                    {
                        return (start.Bottom, false);
                    }
                }
            }

            if (endValue == 'L')
            {
                if (start.Point.X == end.X)
                {
                    return (!start.Bottom, true);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == 'J')
            {
                if (start.Point.X == end.X)
                {
                    return (!start.Bottom, true);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '7')
            {
                if (start.Point.X == end.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == 'F')
            {
                if (start.Point.X == end.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }
        }

        if (startValue == '|')
        {
            if (endValue == '.')
            {
                if (end.X < start.Point.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (!start.Bottom, false);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X == end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    if (start.Point.X > end.X)
                    {
                        return (start.Bottom, false);
                    }
                    else
                    {
                        return (!start.Bottom, true);
                    }
                }
            }

            if (endValue == 'L')
            {
                if (start.Point.X == end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    return (!start.Bottom, true);
                }
            }

            if (endValue == 'J')
            {
                if (start.Point.X > end.X)
                {
                    return (start.Bottom, true);
                }
                else
                {
                    return (true, !start.Bottom);
                }
            }

            if (endValue == '7')
            {
                if (start.Point.X > end.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == 'F')
            {
                if (start.Point.X == end.X)
                {
                    return (true, !start.Bottom);
                }
                else
                {
                    return (!start.Bottom, false);
                }
            }
        }

        if (startValue == 'L')
        {
            if (endValue == '.')
            {
                return (start.Bottom, false);
            }

            if (endValue == '-')
            {
                if (start.Point.X == end.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X == end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    return (start.Bottom, false);
                }
            }

            if (endValue == 'J')
            {
                if (start.Point.X > end.X)
                {
                    return (start.Bottom, true);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '7')
            {
                if (start.Point.Y == end.Y)
                {
                    if (start.Point.X < end.X)
                    {
                        return (true, start.Bottom);
                    }
                    else
                    {
                        return (start.Bottom, false);
                    }
                }
                else
                {
                    if (start.Point.Y < end.Y)
                    {
                        return (start.Bottom, false);
                    }
                    else
                    {
                        return (true, start.Bottom);
                    }
                }
            }

            if (endValue == 'F')
            {
                if (start.Point.Y < end.Y)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, !start.Bottom);
                }
            }
        }

        if (startValue == 'J')
        {
            if (endValue == '.')
            {
                return (start.Bottom, false);
            }

            if (endValue == '-')
            {
                if (start.Point.X == end.X)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X == end.X)
                {
                    return (true, !start.Bottom);
                }
                else
                {
                    return (start.Bottom, true);
                }
            }

            if (endValue == 'L')
            {
                if (start.Point.X > end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    return (start.Bottom, true);
                }
            }

            if (endValue == '7')
            {
                if (start.Point.Y < end.Y)
                {
                    return (start.Bottom, false);
                }
                else
                {
                    return (true, !start.Bottom);
                }
            }

            if (endValue == 'F')
            {
                if (start.Point.Y == end.Y)
                {
                    if (start.Point.X < end.X)
                    {
                        return (start.Bottom, false);
                    }
                    else
                    {
                        return (true, start.Bottom);
                    }
                }
                else
                {
                    if (start.Point.Y < end.Y)
                    {
                        return (start.Bottom, false);
                    }
                    else
                    {
                        return (true, start.Bottom);
                    }
                }
            }
        }

        if (startValue == '7')
        {
            if (endValue == '.')
            {
                return (!start.Bottom, false);
            }

            if (endValue == '-')
            {
                if (start.Point.X == end.X)
                {
                    return (!start.Bottom, true);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X == end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    return (!start.Bottom, true);
                }
            }

            if (endValue == 'L')
            {
                if (start.Point.Y == end.Y)
                {
                    if (start.Point.X < end.X)
                    {
                        return (!start.Bottom, true);
                    }
                    else
                    {
                        return (true, start.Bottom);
                    }
                }
                else
                {
                    if (start.Point.Y < end.Y)
                    {
                        return (true, start.Bottom);
                    }
                    else
                    {
                        return (!start.Bottom, true);
                    }
                }
            }

            if (endValue == 'J')
            {
                if (start.Point.Y < end.Y)
                {
                    return (true, !start.Bottom);
                }
                else
                {
                    return (!start.Bottom, true);
                }
            }

            if (endValue == 'F')
            {
                if (start.Point.X < end.X)
                {
                    return (!start.Bottom, false);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }
        }

        if (startValue == 'F')
        {
            if (endValue == '.')
            {
                return (!start.Bottom, false);
            }

            if (endValue == '-')
            {
                if (start.Point.X == end.X)
                {
                    return (!start.Bottom, true);
                }
                else
                {
                    return (true, start.Bottom);
                }
            }

            if (endValue == '|')
            {
                if (start.Point.X == end.X)
                {
                    return (true, !start.Bottom);
                }
                else
                {
                    return (!start.Bottom, false);
                }
            }

            if (endValue == 'L')
            {
                if (start.Point.Y > end.Y)
                {
                    return (!start.Bottom, true);
                }
                else
                {
                    return (true, !start.Bottom);
                }
            }

            if (endValue == '7')
            {
                if (start.Point.X < end.X)
                {
                    return (true, start.Bottom);
                }
                else
                {
                    return (!start.Bottom, false);
                }
            }

            if (endValue == 'J')
            {
                if (start.Point.Y == end.Y)
                {
                    if (start.Point.X < end.X)
                    {
                        return (true, start.Bottom);
                    }
                    else
                    {
                        return (!start.Bottom, true);
                    }
                }
                else
                {
                    if (start.Point.Y < end.Y)
                    {
                        return (true, start.Bottom);
                    }
                    else
                    {
                        return (!start.Bottom, true);
                    }
                }
            }
        }

        throw new InvalidOperationException();
    }

    private Dictionary<Point, long> FindPipePoints(char[,] map)
    {
        var pipePoints = new Dictionary<Point, long>();

        var startPos = map.GetPoints('S').Single();
        var distance = 0L;

        pipePoints.Add(startPos, distance);

        var prevPoints = new List<Point>
        {
            startPos
        };

        while (prevPoints.Count > 0)
        {
            var newPoints = GetAccessiblePipes(prevPoints, map).Distinct().Where(p => !pipePoints.ContainsKey(p)).ToList();
            distance++;

            foreach (var p in newPoints)
            {
                pipePoints.Add(p, distance);
            }
            prevPoints = newPoints;
        }

        return pipePoints;
    }

    private List<Point> GetAccessiblePipes(List<Point> points, char[,] map)
    {
        var pipes = new List<Point>();

        foreach (var p in points)
        {
            var neighbors = p.GetNeighbors(false).Where(p => map.IsValidPoint(p));

            foreach (var neighbor in neighbors)
            {
                if (DoPipesConnect(p, map[p.X, p.Y], neighbor, map[neighbor.X, neighbor.Y]))
                {
                    pipes.Add(neighbor);
                }
            }
        }

        return pipes;
    }

    private bool DoPipesConnect(Point p1, char pipe1, Point p2, char pipe2)
    {
        if (pipe1 == 'S')
        {
            if (pipe2 == '-' && p2.Y == p1.Y)
            {
                return true;
            }

            if (pipe2 == '|' && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'J' && ((p2.Y == p1.Y && p2.X > p1.X) || (p2.Y > p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == 'L' && ((p2.Y == p1.Y && p2.X < p1.X) || (p2.Y > p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == '7' && ((p2.Y == p1.Y && p2.X > p1.X) || (p2.Y < p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == 'F' && ((p2.Y == p1.Y && p2.X < p1.X) || (p2.Y < p1.Y && p2.X == p1.X)))
            {
                return true;
            }
        }

        if (pipe1 == '-')
        {
            if (pipe2 == '-' && p2.Y == p1.Y)
            {
                return true;
            }

            if (pipe2 == 'J' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }

            if (pipe2 == 'L' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }

            if (pipe2 == '7' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }

            if (pipe2 == 'F' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }
        }

        if (pipe1 == '|')
        {
            if (pipe2 == '|' && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'J' && p2.Y > p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'L' && p2.Y > p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == '7' && p2.Y < p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'F' && p2.Y < p1.Y && p2.X == p1.X)
            {
                return true;
            }
        }

        if (pipe1 == 'J')
        {
            if (pipe2 == '-' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }

            if (pipe2 == '|' && p2.X == p1.X && p2.Y < p1.Y)
            {
                return true;
            }

            if (pipe2 == 'L' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }

            if (pipe2 == '7' && p2.Y < p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'F' && ((p2.Y == p1.Y && p2.X < p1.X) || (p2.Y < p1.Y && p2.X == p1.X)))
            {
                return true;
            }
        }

        if (pipe1 == 'L')
        {
            if (pipe2 == '-' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }

            if (pipe2 == '|' && p2.X == p1.X && p2.Y < p1.Y)
            {
                return true;
            }

            if (pipe2 == 'J' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }

            if (pipe2 == '7' && ((p2.Y == p1.Y && p2.X > p1.X) || (p2.Y < p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == 'F' && p2.Y < p1.Y && p2.X == p1.X)
            {
                return true;
            }
        }

        if (pipe1 == '7')
        {
            if (pipe2 == '-' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }

            if (pipe2 == '|' && p2.X == p1.X && p2.Y > p1.Y)
            {
                return true;
            }

            if (pipe2 == 'J' && p2.Y > p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == 'L' && ((p2.Y == p1.Y && p2.X < p1.X) || (p2.Y > p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == 'F' && p2.Y == p1.Y && p2.X < p1.X)
            {
                return true;
            }
        }

        if (pipe1 == 'F')
        {
            if (pipe2 == '-' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }

            if (pipe2 == '|' && p2.X == p1.X && p2.Y > p1.Y)
            {
                return true;
            }

            if (pipe2 == 'J' && ((p2.Y == p1.Y && p2.X > p1.X) || (p2.Y > p1.Y && p2.X == p1.X)))
            {
                return true;
            }

            if (pipe2 == 'L' && p2.Y > p1.Y && p2.X == p1.X)
            {
                return true;
            }

            if (pipe2 == '7' && p2.Y == p1.Y && p2.X > p1.X)
            {
                return true;
            }
        }

        return false;
    }
}