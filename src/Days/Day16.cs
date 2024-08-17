using System.Drawing;

namespace AdventOfCode.Days;

// 0, 9 right
// 1, 10, up
// 1, 8 down
// 0, 2 left
// 2, 2 right
// 4, 3 up
// 5, 3 right
// 6, 2 down
// 5, 1 left
// 1, 2 up
// 0, 2 left CYCLE
// 2, 2 right  CYCLE
// 1, 0 down
// 7, 1 right
// 7, 2 up
// 6, 3 left
// 6, 4 up

[Day(2023, 16)]
public class Day16 : BaseDay
{
    private HashSet<(Point point, Direction direction)> _seen = new HashSet<(Point point, Direction direction)>();

    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        map = map.FlipVertical();

        var energy = ProcessBeam(map, new Point(0, map.Height() - 1), Direction.Right);

        return energy.GetPoints('#').Count().ToString();
    }

    private char[,] ProcessBeam(char[,] map, Point point, Direction direction)
    {
        Log($"Point: ({point.X}, {point.Y}) Direction: {direction}");
        var energy = new char[map.Width(), map.Height()];

        if (_seen.Contains((point, direction)))
        {
            Log($"CYCLE DETECTED. SKIPPING");
            return energy;
        }
        else
        {
            _seen.Add((point, direction));
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
        return string.Empty;
    }
}
