namespace AdventOfCode;

public static class ArrayExtensions
{
    public static void ForEach<T>(this T[,] a, Action<int, int> action)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                action(x, y);
            }
        }
    }

    public static void ForEach<T>(this T[,] a, Action<T> action)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                action(a[x, y]);
            }
        }
    }
    
    public static IEnumerable<T> ToList<T>(this T[,] a)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                yield return a[x, y];
            }
        }
    }

    public static void ForEach<T>(this T[,,] a, Action<int, int, int> action)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                for (var z = a.GetLowerBound(2); z <= a.GetUpperBound(2); z++)
                {
                    action(x, y, z);
                }
            }
        }
    }

    public static void ForEach<T>(this T[,,] a, Action<T> action)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                for (var z = a.GetLowerBound(2); z <= a.GetUpperBound(2); z++)
                {
                    action(a[x, y, z]);
                }
            }
        }
    }

    public static IEnumerable<T> ToList<T>(this T[,,] a)
    {
        for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
        {
            for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
            {
                for (var z = a.GetLowerBound(2); z <= a.GetUpperBound(2); z++)
                {
                    yield return a[x, y, z];
                }
            }
        }
    }

    public static int IndexOf<T>(this T[] a, T b)
    {
        for (var i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
        {
            if (a[i].Equals(b))
            {
                return i;
            }
        }

        return -1;
    }

    public static void Replace<T>(this T[,,] array, T value, T replace)
    {
        for (var x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
        {
            for (var y = array.GetLowerBound(1); y <= array.GetUpperBound(1); y++)
            {
                for (var z = array.GetLowerBound(2); z <= array.GetUpperBound(2); z++)
                {
                    // Can't use == unless we constraing the generic type
                    if (EqualityComparer<T>.Default.Equals(array[x, y, z], value))
                    {
                        array[x, y, z] = replace;
                    }
                }
            }
        }
    }

    public static void SetRow<T>(this T[,] array, int row, IEnumerable<T> values)
    {
        var col = 0;

        foreach (var value in values)
        {
            array[col++, row] = value;
        }
    }

    public static void SetCol<T>(this T[,] array, int col, IEnumerable<T> values)
    {
        var row = 0;

        foreach (var value in values)
        {
            array[col, row++] = value;
        }
    }

    public static IEnumerable<T> GetRow<T>(this T[,] array, int row)
    {
        for (var x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
        {
            yield return array[x, row];
        }
    }

    public static IEnumerable<T> GetCol<T>(this T[,] array, int col)
    {
        for (var y = array.GetLowerBound(1); y <= array.GetUpperBound(1); y++)
        {
            yield return array[col, y];
        }
    }

    public static IEnumerable<IEnumerable<T>> Rows<T>(this T[,] array)
    {
        for (var y = array.GetLowerBound(1); y <= array.GetUpperBound(1); y++)
        {
            yield return array.GetRow(y);
        }
    }

    public static IEnumerable<IEnumerable<T>> Cols<T>(this T[,] array)
    {
        for (var x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
        {
            yield return array.GetCol(x);
        }
    }
}
