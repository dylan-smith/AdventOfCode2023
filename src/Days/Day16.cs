using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 16)]
public class Day16 : BaseDay
{
    private HashSet<(Point point, Direction direction)> _seen;
    private HashSet<Point> _energy;

    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();

        _seen = new();
        _energy = new();
        ProcessBeam(map, new Point(0, map.Height() - 1), Direction.Right);

        return _energy.Count.ToString();
    }

    private void ProcessBeam(char[,] map, Point point, Direction direction)
    {
        if (!_seen.Add((point, direction)))
        {
            return;
        }

        if (!map.IsValidPoint(point))
        {
            return;
        }

        var currentChar = map[point.X, point.Y];

        while (currentChar == '.' || (currentChar == '-' && direction is Direction.Left or Direction.Right) || (currentChar == '|' && direction is Direction.Up or Direction.Down))
        {
            _energy.Add(point);
            point = point.Move(direction);

            if (!map.IsValidPoint(point))
            {
                return;
            }

            currentChar = map[point.X, point.Y];
        }

        if (currentChar is '|' or '-')
        {
            _energy.Add(point);

            if (currentChar == '|')
            {
                ProcessBeam(map, point.MoveUp(), Direction.Up);
                ProcessBeam(map, point.MoveDown(), Direction.Down);
            }
            else
            {
                ProcessBeam(map, point.MoveLeft(), Direction.Left);
                ProcessBeam(map, point.MoveRight(), Direction.Right);
            }

            return;
        }

        _energy.Add(point);

        if (currentChar == '\\')
        {
            if (direction == Direction.Up)
            {
                ProcessBeam(map, point.MoveLeft(), Direction.Left);
                return;
            }

            if (direction == Direction.Right)
            {
                ProcessBeam(map, point.MoveDown(), Direction.Down);
                return;
            }

            if (direction == Direction.Down)
            {
                ProcessBeam(map, point.MoveRight(), Direction.Right);
                return;
            }

            if (direction == Direction.Left)
            {
                ProcessBeam(map, point.MoveUp(), Direction.Up);
                return;
            }
        }

        if (currentChar == '/')
        {
            if (direction == Direction.Up)
            {
                ProcessBeam(map, point.MoveRight(), Direction.Right);
                return;
            }

            if (direction == Direction.Right)
            {
                ProcessBeam(map, point.MoveUp(), Direction.Up);
                return;
            }

            if (direction == Direction.Down)
            {
                ProcessBeam(map, point.MoveLeft(), Direction.Left);
                return;
            }

            if (direction == Direction.Left)
            {
                ProcessBeam(map, point.MoveDown(), Direction.Down);
                return;
            }
        }
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();
        var maxEnergy = 0;

        for (var x = 0; x < map.Width(); x++)
        {
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(x, 0), Direction.Up);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);

            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(x, map.Height() - 1), Direction.Down);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);
        }

        for (var y = 0; y < map.Height(); y++)
        {
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(0, y), Direction.Right);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);

            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(map.Width() - 1, y), Direction.Left);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);
        }

        return maxEnergy.ToString();
    }
}
