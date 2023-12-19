namespace AdventOfCode;

public static class PermutationExtensions
{
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
    {
        var array = enumerable as T[] ?? enumerable.ToArray();

        var factorials = Enumerable.Range(0, array.Length + 1)
            .Select(Factorial)
            .ToArray();

        for (var i = 0L; i < factorials[array.Length]; i++)
        {
            var sequence = GenerateSequence(i, array.Length - 1, factorials);

            yield return GeneratePermutation(array, sequence);
        }
    }

    public static IEnumerable<string> GetPermutations<T>(this string source)
    {
        return source.ToCharArray().GetPermutations().Select(x => new string(x.ToArray()));
    }

    private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
    {
        var clone = (T[])array.Clone();

        for (int i = 0; i < clone.Length - 1; i++)
        {
            Swap(ref clone[i], ref clone[i + sequence[i]]);
        }

        return clone;
    }

    private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
    {
        var sequence = new int[size];

        for (var j = 0; j < sequence.Length; j++)
        {
            var facto = factorials[sequence.Length - j];

            sequence[j] = (int)(number / facto);
            number = (int)(number % facto);
        }

        return sequence;
    }

    public static IEnumerable<string> GetCombinations(this string source)
    {
        return source.ToCharArray().GetCombinations().Select(c => new string(c.ToArray()));
    }

    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> source)
    {
        var result = new List<IEnumerable<T>>();

        for (var i = 1; i <= source.Count(); i++)
        {
            result.AddRange(source.GetCombinations(i));
        }

        return result;
    }

    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> source, int length)
    {
        if (length == 1)
        {
            foreach (var x in source)
            {
                yield return new List<T>(1) { x };
            }

            yield break;
        }

        for (var i = 0; i < source.Count(); i++)
        {
            var subList = source.Take(i).Concat(source.Skip(i + 1));

            var subCombos = subList.GetCombinations(length - 1);

            foreach (var c in subCombos)
            {
                var newCombo = new List<T>(length)
                {
                    source.ElementAt(i)
                };
                newCombo.AddRange(c);

                yield return newCombo;
            }
        }
    }

    public static IEnumerable<IEnumerable<T>> GetCombinations2<T>(this IEnumerable<T> source, int length)
    {
        if (length == 1)
        {
            foreach (var x in source)
            {
                yield return new List<T>(1) { x };
            }

            yield break;
        }

        for (var i = 0; i < source.Count(); i++)
        {
            var subCombos = source.GetCombinations2(length - 1);

            foreach (var c in subCombos)
            {
                var newCombo = new List<T>(length)
                {
                    source.ElementAt(i)
                };
                newCombo.AddRange(c);

                yield return newCombo;
            }
        }
    }

    private static void Swap<T>(ref T a, ref T b)
    {
        (b, a) = (a, b);
    }

    private static long Factorial(int n)
    {
        long result = n;

        for (int i = 1; i < n; i++)
        {
            result *= i;
        }

        return result;
    }
}