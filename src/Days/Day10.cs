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

        map.Replace(p => !pipePoints.ContainsKey(p), '.');
        Log(map.ToStringGrid());

        return pipePoints.Max(x => x.Value).ToString();
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
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

            if (pipe2 == 'L' && ((p2.Y == p1.Y && p2.X < p1.X) || (p2.Y > p1.Y && p2.X ==  p1.X)))
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