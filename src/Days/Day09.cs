
namespace AdventOfCode.Days;

[Day(2023, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var sensors = input.ParseLines(ParseSensor).ToList();
        var result = 0L;

        foreach (var sensor in sensors)
        {
            var diffList = CreateDiffList(sensor);
            AddItemToDiffList(diffList);
            result += GetNextItem(diffList);
        }

        return result.ToString();
    }

    private long GetNextItem(List<List<long>> diffList)
    {
        return diffList[0].Last();
    }

    private void AddItemToDiffList(List<List<long>> diffList)
    {
        diffList.Last().Add(0);

        for (var i = diffList.Count - 2; i >= 0; i--)
        {
            diffList[i].Add(diffList[i].Last() + diffList[i + 1].Last());
        }
    }

    private List<List<long>> CreateDiffList(IEnumerable<long> sensor)
    {
        var result = new List<List<long>>
        {
            new(sensor)
        };

        while (result.Last().Any(x => x != 0))
        {
            result.Add(new List<long>());

            for (var i = 1; i < result[^2].Count; i++)
            {
                result.Last().Add(result[^2][i] - result[^2][i - 1]);
            }
        }

        return result;
    }

    private List<long> ParseSensor(string line)
    {
        return line.Longs().ToList();
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
