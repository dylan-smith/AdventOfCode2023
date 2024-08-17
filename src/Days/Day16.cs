using System.Drawing;

namespace AdventOfCode.Days;

[Day(2023, 16)]
public class Day16 : BaseDay
{
    private Dictionary<(Point point, Direction direction), char[,]> _seen = new Dictionary<(Point point, Direction direction), char[,]>();

    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();

        var energy = ProcessBeam(map, new Point(0, map.Height() - 1), Direction.Right);

        return energy.GetPoints('#').Count().ToString();
    }

    private char[,] ProcessBeam(char[,] map, Point point, Direction direction)
    {
        //Log($"({point.X}, {point.Y}) {direction}");

        var energy = new char[map.Width(), map.Height()];
        var seenKey = (point, direction);

        if (_seen.ContainsKey(seenKey))
        {
            //Log("CYCLE DETECTED. SKIPPING");
            return _seen[seenKey];
        }
        else
        {
            _seen.Add(seenKey, energy);
        }

        if (!map.IsValidPoint(point))
        {
            return energy;
        }

        while (map[point.X, point.Y] == '.' || (map[point.X, point.Y] == '-' && direction is Direction.Left or Direction.Right) || (map[point.X, point.Y] == '|' && direction is Direction.Up or Direction.Down))
        {
            energy[point.X, point.Y] = '#';
            point = point.Move(direction);

            if (!map.IsValidPoint(point))
            {
                _seen.SafeSet(seenKey, energy);
                return energy;
            }
        }

        if (map[point.X, point.Y] is '|' or '-')
        {
            energy[point.X, point.Y] = '#';
            char[,] a;
            char[,] b;

            if (map[point.X, point.Y] == '|')
            {
                a = ProcessBeam(map, point.MoveUp(), Direction.Up);
                b = ProcessBeam(map, point.MoveDown(), Direction.Down);
            }
            else
            {
                a = ProcessBeam(map, point.MoveLeft(), Direction.Left);
                b = ProcessBeam(map, point.MoveRight(), Direction.Right);
            }

            foreach (var p in a.GetPoints('#'))
            {
                energy[p.X, p.Y] = '#';
            }

            foreach (var p in b.GetPoints('#'))
            {
                energy[p.X, p.Y] = '#';
            }

            _seen.SafeSet(seenKey, energy);
            return energy;
        }

        energy[point.X, point.Y] = '#';
        var newBeam = energy;

        if (map[point.X, point.Y] == '\\' && direction == Direction.Up)
        {
            newBeam = ProcessBeam(map, point.MoveLeft(), Direction.Left);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Right)
        {
            newBeam = ProcessBeam(map, point.MoveDown(), Direction.Down);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Down)
        {
            newBeam = ProcessBeam(map, point.MoveRight(), Direction.Right);
        }

        if (map[point.X, point.Y] == '\\' && direction == Direction.Left)
        {
            newBeam = ProcessBeam(map, point.MoveUp(), Direction.Up);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Up)
        {
            newBeam = ProcessBeam(map, point.MoveRight(), Direction.Right);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Right)
        {
            newBeam = ProcessBeam(map, point.MoveUp(), Direction.Up);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Down)
        {
            newBeam = ProcessBeam(map, point.MoveLeft(), Direction.Left);
        }

        if (map[point.X, point.Y] == '/' && direction == Direction.Left)
        {
            newBeam = ProcessBeam(map, point.MoveDown(), Direction.Down);
        }

        foreach (var p in newBeam.GetPoints('#'))
        {
            energy[p.X, p.Y] = '#';
        }

        return energy;
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();
        var maxEnergy = 0;

        var totalBeams = (map.Width() * 2) + (map.Height() * 2);
        var beamCount = 0;

        _seen = new Dictionary<(Point point, Direction direction), char[,]>();

        for (var x = 0; x < map.Width(); x++)
        {
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new Dictionary<(Point point, Direction direction), char[,]>();
            var upEnergy = CountEnergy(ProcessBeam(map, new Point(x, 0), Direction.Up));
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new Dictionary<(Point point, Direction direction), char[,]>();
            var downEnergy = CountEnergy(ProcessBeam(map, new Point(x, map.Height() - 1), Direction.Down));

            maxEnergy = Math.Max(maxEnergy, upEnergy);
            maxEnergy = Math.Max(maxEnergy, downEnergy);
        }

        for (var y = 0; y < map.Height(); y++)
        {
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new Dictionary<(Point point, Direction direction), char[,]>();
            var rightEnergy = CountEnergy(ProcessBeam(map, new Point(0, y), Direction.Right));
            Log($"Processing Beam {beamCount++} / {totalBeams}...");
            _seen = new Dictionary<(Point point, Direction direction), char[,]>();
            var leftEnergy = CountEnergy(ProcessBeam(map, new Point(map.Width() - 1, y), Direction.Left));

            maxEnergy = Math.Max(maxEnergy, rightEnergy);
            maxEnergy = Math.Max(maxEnergy, leftEnergy);
        }

        return maxEnergy.ToString();
    }

    private int CountEnergy(char[,] map)
    {
        return map.GetPoints('#').Count();
    }
}
