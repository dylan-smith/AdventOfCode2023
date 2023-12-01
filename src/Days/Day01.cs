namespace AdventOfCode.Days;

[Day(2023, 1)]
public class Day01 : BaseDay
{
    public override string PartOne(string input)
    {
        var lines = input.Lines();

        var result = lines.Sum(line => ProcessLine(line));

        return result.ToString();
    }

    private long ProcessLine(string line)
    {
        var a = line.First(c => c >= '0' && c <= '9');
        var b = line.Last(c => c >= '0' && c <= '9');

        return ((a - '0') * 10) + (b - '0');
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines();

        var firstNumbers = lines.Select(line => FindFirstNumber(line));
        var lastNumbers = lines.Select(line => FindLastNumber(line));

        var numbers = new List<long>();

        for (var i = 0; i < lines.Count(); i++)
        {
            numbers.Add(firstNumbers.ElementAt(i) * 10 + lastNumbers.ElementAt(i));
        }

        //var numbers = numberLines.Select(line => ProcessLine(line));

        //for (var i = 0; i < lines.Count(); i++)
        //{
        //    Log($"{lines.ElementAt(i)} -> {numberLines.ElementAt(i)} -> {numbers.ElementAt(i)}");
        //}

        return numbers.Sum().ToString();
    }

    private int FindFirstNumber(string line)
    {
        var words = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var pos = 0;

        while (pos < line.Length)
        {
            if (line[pos] >= '0' && line[pos] <= '9')
            {
                return int.Parse(line[pos].ToString());
            }

            var word = words.FirstOrDefault(w => line.Substring(pos).StartsWith(w));
            if (word != null)
            {
                return words.IndexOf(word);
            }
            pos++;
        }

        return 0;
    }

    private int FindLastNumber(string line)
    {
        var words = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var pos = line.Length - 1;

        while (pos >= 0)
        {
            if (line[pos] >= '0' && line[pos] <= '9')
            {
                return int.Parse(line[pos].ToString());
            }

            var word = words.FirstOrDefault(w => line.Substring(pos).StartsWith(w));
            if (word != null)
            {
                return words.IndexOf(word);
            }
            pos--;
        }

        return 0;
    }

    private string ReplaceWordsWithNumbers(string line)
    {
        var words = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        var pos = 0;

        while (pos < line.Length)
        {
            var word = words.FirstOrDefault(w => line.Substring(pos).StartsWith(w));
            if (word != null)
            {
                line = line.Substring(0, pos) + words.IndexOf(word) + line.Substring(pos + word.Length);
            }
            pos++;
        }

        return line;
    }
}
