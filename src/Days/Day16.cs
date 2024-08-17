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
        var seenKey = (point, direction);

        if (_seen.Contains(seenKey))
        {
            return;
        }
        else
        {
            _seen.Add(seenKey);
        }

        if (!map.IsValidPoint(point))
        {
            return;
        }

        while (map[point.X, point.Y] == '.' || (map[point.X, point.Y] == '-' && direction is Direction.Left or Direction.Right) || (map[point.X, point.Y] == '|' && direction is Direction.Up or Direction.Down))
        {
            _energy.Add(point);
            point = point.Move(direction);

            if (!map.IsValidPoint(point))
            {
                return;
            }
        }

        if (map[point.X, point.Y] is '|' or '-')
        {
            _energy.Add(point);

            if (map[point.X, point.Y] == '|')
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

        if (map[point.X, point.Y] == '\\' && direction == Direction.Up)
        {
            ProcessBeam(map, point.MoveLeft(), Direction.Left);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Right)
        {
            ProcessBeam(map, point.MoveDown(), Direction.Down);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Down)
        {
            ProcessBeam(map, point.MoveRight(), Direction.Right);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Left)
        {
            ProcessBeam(map, point.MoveUp(), Direction.Up);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Up)
        {
            ProcessBeam(map, point.MoveRight(), Direction.Right);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Right)
        {
            ProcessBeam(map, point.MoveUp(), Direction.Up);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Down)
        {
            ProcessBeam(map, point.MoveLeft(), Direction.Left);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Left)
        {
            ProcessBeam(map, point.MoveDown(), Direction.Down);
        }
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();
        var maxEnergy = 0;

        var totalBeams = (map.Width() * 2) + (map.Height() * 2);
        var beamCount = 1;

        for (var x = 0; x < map.Width(); x++)
        {
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(x, 0), Direction.Up);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);

            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(x, map.Height() - 1), Direction.Down);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);
        }

        for (var y = 0; y < map.Height(); y++)
        {
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(0, y), Direction.Right);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);

            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new();
            _energy = new();
            ProcessBeam(map, new Point(map.Width() - 1, y), Direction.Left);
            maxEnergy = Math.Max(maxEnergy, _energy.Count);
        }

        return maxEnergy.ToString();
    }
}
