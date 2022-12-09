using System.Drawing;

namespace AdventOfCode.Days;

[Day(2022, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var head = new Point(0, 0);
        var tail = new Point(0, 0);

        var seen = new HashSet<Point>();
        _ = seen.Add(tail);

        var moves = input.ParseLines(ParseMove);

        foreach (var (direction, distance) in moves)
        {
            for (var i = 0; i < distance; i++)
            {
                head = head.Move(direction);
                tail = MoveTail(tail, head);

                _ = seen.Add(tail);
            }
        }

        return seen.Count.ToString();
    }

    private Point MoveTail(Point tail, Point head)
    {
        if (tail.X == head.X)
        {
            if (tail.ManhattanDistance(head) <= 1)
            {
                return tail;
            }
            
            if (tail.Y < head.Y)
            {
                return tail.MoveUp();
            }
            else
            {
                return tail.MoveDown();
            }
        }

        if (tail.Y == head.Y)
        {
            if (tail.ManhattanDistance(head) <= 1)
            {
                return tail;
            }

            if (tail.X < head.X)
            {
                return tail.MoveRight();
            }
            else
            {
                return tail.MoveLeft();
            }
        }

        if (tail.ManhattanDistance(head) == 2)
        {
            return tail;
        }

        if (tail.X < head.X && tail.Y < head.Y)
        {
            tail = tail.MoveRight();
            return tail.MoveUp();
        }

        if (tail.X < head.X && tail.Y > head.Y)
        {
            tail = tail.MoveRight();
            return tail.MoveDown();
        }

        if (tail.X > head.X && tail.Y < head.Y)
        {
            tail = tail.MoveLeft();
            return tail.MoveUp();
        }

        if (tail.X > head.X && tail.Y > head.Y)
        {
            tail = tail.MoveLeft();
            return tail.MoveDown();
        }

        throw new Exception();
    }

    private (Direction dir, int distance) ParseMove(string line)
    {
        var dir = line.Words().First().ToDirection();
        var distance = int.Parse(line.Words().Last());

        return (dir, distance);
    }

    public override string PartTwo(string input)
    {
        var knots = new List<Point>();
        knots.Initialize(new Point(0, 0), 10);

        var seen = new HashSet<Point>();
        seen.Add(knots.Last());

        var moves = input.ParseLines(ParseMove);

        foreach (var (direction, distance) in moves)
        {
            for (var i = 0; i < distance; i++)
            {
                knots[0] = knots[0].Move(direction);
                
                for (var x = 1; x < 10; x++)
                {
                    knots[x] = MoveTail(knots[x], knots[x - 1]);
                }

                seen.Add(knots.Last());
            }
        }

        return seen.Count.ToString();
    }
}
