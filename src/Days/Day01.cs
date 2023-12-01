namespace AdventOfCode.Days;

[Day(2023, 1)]
public class Day01 : BaseDay
{
    public override string PartOne(string input)
    {
        var lines = input.Lines();

        var result = lines.Sum(line => GetCalibrationValue(line));

        return result.ToString();
    }

    private long GetCalibrationValue(string line)
    {
        var a = line.First(c => c.IsNumeric());
        var b = line.Last(c => c.IsNumeric());

        return a.ParseInt() * 10 + b.ParseInt();
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines();
        var result = lines.Sum(line => GetCalibrationValue2(line));

        return result.ToString();
    }

    private long GetCalibrationValue2(string line)
    {
        var a = FindFirstNumber(line);
        var b = FindLastNumber(line);

        return a * 10 + b;
    }

    private int FindFirstNumber(string line)
    {
        var pos = 0;

        while (pos < line.Length)
        {
            var number = FindNumber(line, pos);
            if (number >= 0)
            {
                return number;
            }

            pos++;
        }

        throw new Exception("Could not find first number");
    }

    private int FindLastNumber(string line)
    {
        var pos = line.Length - 1;

        while (pos >= 0)
        {
            var number = FindNumber(line, pos);
            if (number >= 0)
            {
                return number;
            }
           
            pos--;
        }

        throw new Exception("Could not find last number");
    }

    private int FindNumber(string line, int pos)
    {
        var NUMBERS = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        if (line[pos].IsNumeric())
        {
            return line[pos].ParseInt();
        }

        var word = NUMBERS.FirstOrDefault(w => line[pos..].StartsWith(w));
        if (word != null)
        {
            return NUMBERS.IndexOf(word);
        }

        return -1;
    }
}
