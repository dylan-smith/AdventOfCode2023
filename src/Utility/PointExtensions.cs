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

            var ax = a.X;
            var ay = a.Y;
            var px = point.X;
            var py = point.Y;

            foreach (var b in polygon)
            {
                var bx = b.X;
                var by = b.Y;

                // To be 100% accurate this code is probably needed to cover edge cases
                // However, in the one place we use this so far these edge cases aren't needed
                // so was commented out to speed up perf

                //if ((bx == px) && (by == py))
                //{
                //    return true;
                //}

                //if ((by == ay) && (py == ay))
                //{
                //    if ((ax <= px) && (px <= bx))
                //    {
                //        return true;
                //    }

                //    if ((bx <= px) && (px <= ax))
                //    {
                //        return true;
                //    }
                //}

                if (((by < py) && (ay >= py)) || ((ay < py) && (by >= py)))
                {
                    if (bx + ((py - by) / (ay - by) * (ax - bx)) <= px)
                    {
                        result = !result;
                    }
                }

                ax = bx;
                ay = by;
            }

            return result;
        }
    }
}