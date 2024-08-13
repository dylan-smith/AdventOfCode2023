using System.Drawing;

namespace AdventOfCode
{
    public static class PointExtensions
    {
        public static IEnumerable<Point> GetNeighbors(this Point point)
        {
            return point.GetNeighbors(true);
        }

        public static IEnumerable<Point> GetNeighbors(this Point point, bool includeDiagonals)
        {
            var adjacentPoints = new List<Point>(8);

            adjacentPoints.Add(new Point(point.X - 1, point.Y));
            adjacentPoints.Add(new Point(point.X + 1, point.Y));
            adjacentPoints.Add(new Point(point.X, point.Y + 1));
            adjacentPoints.Add(new Point(point.X, point.Y - 1));

            if (includeDiagonals)
            {
                adjacentPoints.Add(new Point(point.X - 1, point.Y - 1));
                adjacentPoints.Add(new Point(point.X + 1, point.Y - 1));
                adjacentPoints.Add(new Point(point.X + 1, point.Y + 1));
                adjacentPoints.Add(new Point(point.X - 1, point.Y + 1));
            }

            return adjacentPoints;
        }

        public static int ManhattanDistance(this Point point)
        {
            return point.ManhattanDistance(new Point(0, 0));
        }

        public static int ManhattanDistance(this Point point, Point target)
        {
            return Math.Abs(point.X - target.X) + Math.Abs(point.Y - target.Y);
        }

        public static Point MoveDown(this Point point, int distance)
        {
            return new Point(point.X, point.Y - distance);
        }

        public static Point MoveUp(this Point point, int distance)
        {
            return new Point(point.X, point.Y + distance);
        }

        public static Point MoveRight(this Point point, int distance)
        {
            return new Point(point.X + distance, point.Y);
        }

        public static Point MoveLeft(this Point point, int distance)
        {
            return new Point(point.X - distance, point.Y);
        }

        public static Point MoveDown(this Point point)
        {
            return point.MoveDown(1);
        }

        public static Point MoveUp(this Point point)
        {
            return point.MoveUp(1);
        }

        public static Point MoveRight(this Point point)
        {
            return point.MoveRight(1);
        }

        public static Point MoveLeft(this Point point)
        {
            return point.MoveLeft(1);
        }

        public static Point Move(this Point point, Direction direction, int distance)
        {
            return direction switch
            {
                Direction.Down => point.MoveDown(distance),
                Direction.Up => point.MoveUp(distance),
                Direction.Right => point.MoveRight(distance),
                Direction.Left => point.MoveLeft(distance),
                _ => throw new ArgumentException(),
            };
        }

        public static Point Move(this Point point, Direction direction)
        {
            return point.Move(direction, 1);
        }

        public static double CalcDistance(this Point p, Point to) => Math.Sqrt(Math.Pow(p.X - to.X, 2) + Math.Pow(p.Y - to.Y, 2));

        public static double CalcSlope(this Point p, Point to) => (double)(p.Y - to.Y) / (double)(p.X - to.X);

        public static bool IsInPolygon(this Point point, IEnumerable<Point> polygon)
        {
            bool result = false;
            var a = polygon.Last();

            foreach (var b in polygon)
            {
                if ((b.X == point.X) && (b.Y == point.Y))
                {
                    return true;
                }

                if ((b.Y == a.Y) && (point.Y == a.Y))
                {
                    if ((a.X <= point.X) && (point.X <= b.X))
                    {
                        return true;
                    }

                    if ((b.X <= point.X) && (point.X <= a.X))
                    {
                        return true;
                    }
                }

                if (((b.Y < point.Y) && (a.Y >= point.Y)) || ((a.Y < point.Y) && (b.Y >= point.Y)))
                {
                    if (b.X + ((point.Y - b.Y) / (a.Y - b.Y) * (a.X - b.X)) <= point.X)
                    {
                        result = !result;
                    }
                }

                a = b;
            }

            return result;
        }
    }
}