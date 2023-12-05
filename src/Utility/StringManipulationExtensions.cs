using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode
{
    public static class StringManipulationExtensions
    {
        public static StringBuilder SwapPositions(this StringBuilder source, int x, int y)
        {
            var xChar = source[x];
            var yChar = source[y];

            source = source.Remove(x, 1);
            source = source.Insert(x, yChar);

            source = source.Remove(y, 1);
            source = source.Insert(y, xChar);

            return source;
        }

        public static string SwapPositions(this string source, int x, int y)
        {
            return new StringBuilder(source).SwapPositions(x, y).ToString();
        }

        public static StringBuilder RotateLeft(this StringBuilder source)
        {
            var startChar = source[0];

            source.Remove(0, 1);
            source.Insert(source.Length, startChar);

            return source;
        }

        public static StringBuilder RotateRight(this StringBuilder source)
        {
            var endChar = source[source.Length - 1];

            source.Remove(source.Length - 1, 1);
            source.Insert(0, endChar);

            return source;
        }

        public static string RotateLeft(this string source)
        {
            return new StringBuilder(source).RotateLeft().ToString();
        }

        public static string RotateRight(this string source)
        {
            return new StringBuilder(source).RotateRight().ToString();
        }

        public static StringBuilder RotateRight(this StringBuilder source, int rotateCount)
        {
            for (var i = 0; i < rotateCount; i++)
            {
                source.RotateRight();
            }

            return source;
        }

        public static StringBuilder RotateLeft(this StringBuilder source, int rotateCount)
        {
            for (var i = 0; i < rotateCount; i++)
            {
                source.RotateLeft();
            }

            return source;
        }

        public static string RotateRight(this string source, int rotateCount)
        {
            for (var i = 0; i < rotateCount; i++)
            {
                source.RotateRight();
            }

            return source;
        }

        public static string RotateLeft(this string source, int rotateCount)
        {
            for (var i = 0; i < rotateCount; i++)
            {
                source.RotateLeft();
            }

            return source;
        }

        public static string ReverseString(this string source)
        {
            return new string(source.Reverse().ToArray());
        }

        public static IEnumerable<string> Lines(this string input)
        {
            return input.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Paragraphs(this string input)
        {
            return input.Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Words(this string input)
        {
            return input.Split(new string[] { " ", "\t", Environment.NewLine, ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<int> Integers(this string input)
        {
            return input.Words().Select(x => int.Parse(x)).ToList();
        }

        public static IEnumerable<long> Longs(this string input)
        {
            return input.Words().Select(x => long.Parse(x)).ToList();
        }

        public static IEnumerable<double> Doubles(this string input)
        {
            return input.Words().Select(x => double.Parse(x)).ToList();
        }

        public static IEnumerable<T> ParseLines<T>(this string input, Func<string, T> parser)
        {
            return input.Lines().Select(parser);
        }

        public static string RemoveWhitespace(this string input)
        {
            return input.Strip("\n", Environment.NewLine, " ", "\t");
        }

        public static bool IsAnagram(this string a, string b)
        {
            return a.ToCharArray().UnorderedEquals(b.ToCharArray());
        }

        public static bool IsHex(this string input)
        {
            foreach (var c in input)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F')))
                {
                    return false;
                }
            }

            return true;
        }

        public static string ShaveLeft(this string a, int characters)
        {
            return a.Substring(characters);
        }

        public static string ShaveLeft(this string a, string shave)
        {
            var result = a;

            while (result.StartsWith(shave))
            {
                result = result.Substring(shave.Length);
            }

            return result;
        }

        public static string ShaveRight(this string a, int characters)
        {
            return a.Substring(0, a.Length - characters);
        }

        public static string ShaveRight(this string a, string shave)
        {
            var result = a;

            while (result.EndsWith(shave))
            {
                result = result.Substring(0, result.Length - shave.Length);
            }

            return result;
        }

        public static string Shave(this string a, int characters)
        {
            return a.Substring(characters, a.Length - (characters * 2));
        }

        public static string Shave(this string a, string shave)
        {
            var result = a;

            while (result.StartsWith(shave))
            {
                result = result.Substring(shave.Length);
            }

            while (result.EndsWith(shave))
            {
                result = result.Substring(0, result.Length - shave.Length);
            }

            return result;
        }

        public static string Strip(this string a, params string[] remove)
        {
            var result = a;

            while (remove.Any(x => result.Contains(x)))
            {
                var r = remove.First(x => result.Contains(x));

                result = result.Remove(result.IndexOf(r), r.Length);
            }

            return result;
        }

        public static string HexToBinary(this string hex)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in hex.ToCharArray())
            {
                var intValue = int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
                sb.Append(Convert.ToString(intValue, 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }

        public static string Overlap(this string a, string b)
        {
            return new string(Overlap<char>(a, b).ToArray());
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static IEnumerable<T> Overlap<T>(this IEnumerable<T> a, IEnumerable<T> b) where T : IEquatable<T>
        {
            var result = new List<T>();
            var c = a.ToList();
            var d = b.ToList();

            for (var x = 0; x < Math.Min(c.Count, d.Count); x++)
            {
                if (c[x].Equals(d[x]))
                {
                    result.Add(c[x]);
                }
            }

            return result;
        }

        public static void Deconstruct<T>(this IEnumerable<T> list, out T first)
        {
            first = list.Count() > 0 ? list.ElementAt(0) : default(T); // or throw
        }

        public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second)
        {
            first = list.Count() > 0 ? list.ElementAt(0) : default(T); // or throw
            second = list.Count() > 1 ? list.ElementAt(1) : default(T); // or throw
        }

        public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second, out T third)
        {
            first = list.Count() > 0 ? list.ElementAt(0) : default(T); // or throw
            second = list.Count() > 1 ? list.ElementAt(1) : default(T); // or throw
            third = list.Count() > 2 ? list.ElementAt(2) : default(T); // or throw
        }

        public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second, out T third, out T fourth)
        {
            first = list.Count() > 0 ? list.ElementAt(0) : default(T); // or throw
            second = list.Count() > 1 ? list.ElementAt(1) : default(T); // or throw
            third = list.Count() > 2 ? list.ElementAt(2) : default(T); // or throw
            fourth = list.Count() > 3 ? list.ElementAt(3) : default(T); // or throw
        }

        public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
        {
            first = list.Count() > 0 ? list.ElementAt(0) : default(T); // or throw
            second = list.Count() > 1 ? list.ElementAt(1) : default(T); // or throw
            third = list.Count() > 2 ? list.ElementAt(2) : default(T); // or throw
            fourth = list.Count() > 3 ? list.ElementAt(3) : default(T); // or throw
            fifth = list.Count() > 4 ? list.ElementAt(4) : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
            fourth = list.Count > 3 ? list[3] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
            fourth = list.Count > 3 ? list[3] : default(T); // or throw
            fifth = list.Count > 4 ? list[4] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
            fourth = list.Count > 3 ? list[3] : default(T); // or throw
            fifth = list.Count > 4 ? list[4] : default(T); // or throw
            sixth = list.Count > 5 ? list[5] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
            fourth = list.Count > 3 ? list[3] : default(T); // or throw
            fifth = list.Count > 4 ? list[4] : default(T); // or throw
            sixth = list.Count > 5 ? list[5] : default(T); // or throw
            seventh = list.Count > 6 ? list[6] : default(T); // or throw
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out T eigth)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            third = list.Count > 2 ? list[2] : default(T); // or throw
            fourth = list.Count > 3 ? list[3] : default(T); // or throw
            fifth = list.Count > 4 ? list[4] : default(T); // or throw
            sixth = list.Count > 5 ? list[5] : default(T); // or throw
            seventh = list.Count > 6 ? list[6] : default(T); // or throw
            eigth = list.Count > 7 ? list[7] : default(T); // or throw
        }

        public static Direction ToDirection(this char c)
        {
            return c switch
            {
                'R' => Direction.Right,
                'D' => Direction.Down,
                'U' => Direction.Up,
                'L' => Direction.Left,
                _ => throw new ArgumentException($"Unrecognized character [{c}]")
            };
        }

        public static Compass ToCompass(this char c)
        {
            return c switch
            {
                'N' => Compass.North,
                'S' => Compass.South,
                'E' => Compass.East,
                'W' => Compass.West,
                _ => throw new ArgumentException($"Unrecognized character [{c}]")
            };
        }

        public static long ParseLong(this string value)
        {
            return long.Parse(value);
        }

        public static int ParseInt(this string value)
        {
            return int.Parse(value);
        }
    }
}