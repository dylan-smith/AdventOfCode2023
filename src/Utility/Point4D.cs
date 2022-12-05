namespace AdventOfCode
{
    public class Point4D : IEquatable<Point4D>
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public long T { get; set; }

        public Point4D(long x, long y, long z, long t)
        {
            X = x;
            Y = y;
            Z = z;
            T = t;
        }

        public Point4D(string coordinates) :
            this(long.Parse(coordinates.Words().ToList()[0]),
                 long.Parse(coordinates.Words().ToList()[1]),
                 long.Parse(coordinates.Words().ToList()[2]),
                 long.Parse(coordinates.Words().ToList()[3]))
        {
        }

        public long GetManhattanDistance()
        {
            return Math.Abs(X - 0) + Math.Abs(Y - 0) + Math.Abs(Z - 0) + Math.Abs(T - 0);
        }

        public long GetManhattanDistance(Point4D point)
        {
            return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z) + Math.Abs(T - point.T);
        }

        public static bool operator ==(Point4D a, Point4D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.T == b.T;
        }

        public static bool operator !=(Point4D a, Point4D b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.T != b.T;
        }

        public static Point4D operator +(Point4D a, Point4D b)
        {
            return new Point4D(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.T + b.T);
        }

        public static Point4D operator -(Point4D a, Point4D b)
        {
            return new Point4D(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.T - b.T);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{T}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point4D);
        }

        public bool Equals(Point4D other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            var hashCode = 17;
            hashCode = hashCode * 486187739 + X.GetHashCode();
            hashCode = hashCode * 486187739 + Y.GetHashCode();
            hashCode = hashCode * 486187739 + Z.GetHashCode();
            hashCode = hashCode * 486187739 * T.GetHashCode();

            return hashCode;
        }

        public IEnumerable<Point4D> GetNeighbors()
        {
            return GetNeighbors(false);
        }

        public IEnumerable<Point4D> GetNeighbors(bool includeDiagonals)
        {
            if (includeDiagonals)
            {
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        for (var z = -1; z <= 1; z++)
                        {
                            for (var t = -1; t <= 1; t++)
                            {
                                if (x != 0 || y != 0 || z != 0 || t != 0)
                                {
                                    yield return new Point4D(this.X + x, this.Y + y, this.Z + z, this.T + t);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                yield return new Point4D(this.X - 1, this.Y, this.Z, this.T);
                yield return new Point4D(this.X + 1, this.Y, this.Z, this.T);
                yield return new Point4D(this.X, this.Y - 1, this.Z, this.T);
                yield return new Point4D(this.X, this.Y + 1, this.Z, this.T);
                yield return new Point4D(this.X, this.Y, this.Z - 1, this.T);
                yield return new Point4D(this.X, this.Y, this.Z + 1, this.T);
                yield return new Point4D(this.X, this.Y, this.Z, this.T - 1);
                yield return new Point4D(this.X, this.Y, this.Z, this.T + 1);
            }
        }
    }
}