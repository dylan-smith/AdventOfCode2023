namespace AdventOfCode
{
    public class Point3D : IEquatable<Point3D>
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public Point3D(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D()
        { }

        public Point3D(string coordinates) :
            this(long.Parse(coordinates.Words().ToList()[0]),
                 long.Parse(coordinates.Words().ToList()[1]),
                 long.Parse(coordinates.Words().ToList()[2]))
        {
        }

        public long GetManhattanDistance()
        {
            return Math.Abs(X - 0) + Math.Abs(Y - 0) + Math.Abs(Z - 0);
        }

        public long GetManhattanDistance(Point3D point)
        {
            return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z);
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point3D);
        }

        public bool Equals(Point3D other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public IEnumerable<Point3D> GetNeighbors()
        {
            return GetNeighbors(false);
        }

        public IEnumerable<Point3D> GetNeighbors(bool includeDiagonals)
        {
            if (!includeDiagonals)
            {
                yield return new Point3D(X - 1, Y, Z);
                yield return new Point3D(X + 1, Y, Z);
                yield return new Point3D(X, Y + 1, Z);
                yield return new Point3D(X, Y - 1, Z);
                yield return new Point3D(X, Y, Z + 1);
                yield return new Point3D(X, Y, Z - 1);
            }

            if (includeDiagonals)
            {
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        for (var z = -1; z <= 1; z++)
                        {
                            if (x != 0 || y != 0 || z != 0)
                            {
                                yield return new Point3D(this.X + x, this.Y + y, this.Z + z);
                            }
                        }
                    }
                }
            }
        }
    }
}