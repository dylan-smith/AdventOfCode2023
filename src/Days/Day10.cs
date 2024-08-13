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
        var (_, distance) = FindPipePoints(map);

        return (distance / 2).ToString();
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var (pipePoints, _) = FindPipePoints(map);
        map.Replace(p => !pipePoints.Contains(p), '.');

        var nestCount = 0L;

        foreach (var p in map.GetPoints(a => map[a.X, a.Y] == '.'))
        {
            if (p.IsInPolygon(pipePoints))
            {
                nestCount++;
            }
        }

        return nestCount.ToString();
    }

    private (List<Point>, long distance) FindPipePoints(char[,] map)
    {
        var pipePoints = new HashSet<Point>();

        var startPos = map.GetPoints('S').Single();
        var distance = -1L;

        var prevPoint = startPos;
        var newPoint = prevPoint;

        do
        {
            pipePoints.Add(newPoint);
            prevPoint = newPoint;
            distance++;

            if (map[prevPoint.X, prevPoint.Y] == 'S')
            {
                if (CanMoveRight(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveRight();
                    continue;
                }

                if (CanMoveLeft(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveLeft();
                    continue;
                }

                if (CanMoveUp(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveUp();
                    continue;
                }

                if (CanMoveDown(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveDown();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == '-')
            {
                if (CanMoveRight(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveRight();
                    continue;
                }

                if (CanMoveLeft(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveLeft();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == '|')
            {
                if (CanMoveUp(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveUp();
                    continue;
                }

                if (CanMoveDown(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveDown();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == 'L')
            {
                if (CanMoveRight(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveRight();
                    continue;
                }

                if (CanMoveDown(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveDown();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == 'J')
            {
                if (CanMoveLeft(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveLeft();
                    continue;
                }

                if (CanMoveDown(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveDown();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == '7')
            {
                if (CanMoveLeft(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveLeft();
                    continue;
                }

                if (CanMoveUp(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveUp();
                    continue;
                }
            }

            if (map[prevPoint.X, prevPoint.Y] == 'F')
            {
                if (CanMoveRight(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveRight();
                    continue;
                }

                if (CanMoveUp(prevPoint, map, pipePoints))
                {
                    newPoint = prevPoint.MoveUp();
                    continue;
                }
            }

            newPoint = startPos;
            pipePoints.Add(newPoint);
            distance++;
        } while (newPoint != startPos);

        return (pipePoints.ToList(), distance);
    }

    private bool CanMoveRight(Point point, char[,] map, HashSet<Point> seen)
    {
        var newPoint = point.MoveRight();

        if (seen.Contains(newPoint))
        {
            return false;
        }

        return map[newPoint.X, newPoint.Y] is '-' or 'J' or '7';
    }

    private bool CanMoveDown(Point point, char[,] map, HashSet<Point> seen)
    {
        var newPoint = point.MoveDown();

        if (seen.Contains(newPoint))
        {
            return false;
        }

        return map[newPoint.X, newPoint.Y] is '|' or 'F' or '7';
    }

    private bool CanMoveUp(Point point, char[,] map, HashSet<Point> seen)
    {
        var newPoint = point.MoveUp();

        if (seen.Contains(newPoint))
        {
            return false;
        }

        return map[newPoint.X, newPoint.Y] is '|' or 'J' or 'L';
    }

    private bool CanMoveLeft(Point point, char[,] map, HashSet<Point> seen)
    {
        var newPoint = point.MoveLeft();

        if (seen.Contains(newPoint))
        {
            return false;
        }

        return map[newPoint.X, newPoint.Y] is '-' or 'F' or 'L';
    }
}