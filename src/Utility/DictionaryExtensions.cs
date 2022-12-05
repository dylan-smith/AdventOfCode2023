namespace AdventOfCode;

public static class DictionaryExtensions
{
    public static void SafeIncrement<TKey>(this IDictionary<TKey, int> dict, TKey key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]++;
        }
        else
        {
            dict.Add(key, 1);
        }
    }

    public static void SafeIncrement<TKey>(this IDictionary<TKey, int> dict, TKey key, int amount)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] += amount;
        }
        else
        {
            dict.Add(key, amount);
        }
    }

    public static void SafeDecrement<TKey>(this IDictionary<TKey, int> dict, TKey key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]--;
        }
        else
        {
            dict.Add(key, -1);
        }
    }

    public static void SafeIncrement<TKey>(this IDictionary<TKey, long> dict, TKey key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]++;
        }
        else
        {
            dict.Add(key, 1);
        }
    }

    public static void SafeIncrement<TKey>(this IDictionary<TKey, long> dict, TKey key, long amount)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] += amount;
        }
        else
        {
            dict.Add(key, amount);
        }
    }

    public static void SafeDecrement<TKey>(this IDictionary<TKey, long> dict, TKey key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]--;
        }
        else
        {
            dict.Add(key, -1);
        }
    }

    public static void SafeSet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] = value;
        }
        else
        {
            dict.Add(key, value);
        }
    }

    public static bool SafeCompare<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
        {
            return dict[key].Equals(value);
        }

        return false;
    }

    public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) => dict.ContainsKey(key) ? dict[key] : default;
}
