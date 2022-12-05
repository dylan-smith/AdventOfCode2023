using System.Drawing;

namespace AdventOfCode
{
    public static class RectangleExtensions
    {
        public static IEnumerable<Point> GetPoints(this Rectangle rect)
        {
            for (var x = rect.Left; x < rect.Left + rect.Width; x++)
            {
                for (var y = rect.Top; y < rect.Top + rect.Height; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}