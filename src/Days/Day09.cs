namespace AdventOfCode.Days;

[Day(2023, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var sensors = input.ParseLines(ParseSensor);
        var result = 0L;

        foreach (var sensor in sensors)
        {
            var diffLists = CreateDiffLists(sensor);
            AddItemToEndOfDiffList(diffLists);
            result += GetNextItem(diffLists);
        }

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        var sensors = input.ParseLines(ParseSensor);
        var result = 0L;

        foreach (var sensor in sensors)
        {
            var diffList = CreateDiffLists(sensor);
            AddItemToStartOfDiffList(diffList);
            result += GetFirstItem(diffList);
        }

        return result.ToString();
    }

    private IEnumerable<long> ParseSensor(string line) => line.Longs();
    private long GetFirstItem(List<List<long>> diffList) => diffList[0].First();
    private long GetNextItem(List<List<long>> diffList) => diffList[0].Last();

    private List<List<long>> CreateDiffLists(IEnumerable<long> sensor)
    {
        var diffLists = new List<List<long>>
        {
            new(sensor)
        };

        while (diffLists.Last().Any(x => x != 0))
        {
            diffLists.Add(new List<long>());

            for (var i = 1; i < diffLists[^2].Count; i++)
            {
                diffLists.Last().Add(diffLists[^2][i] - diffLists[^2][i - 1]);
            }
        }

        return diffLists;
    }

    private void AddItemToStartOfDiffList(List<List<long>> diffList)
    {
        diffList.Last().AddFirst(0);

        for (var i = diffList.Count - 2; i >= 0; i--)
        {
            diffList[i].AddFirst(diffList[i].First() - diffList[i + 1].First());
        }
    }

    private void AddItemToEndOfDiffList(List<List<long>> diffList)
    {
        diffList.Last().Add(0);

        for (var i = diffList.Count - 2; i >= 0; i--)
        {
            diffList[i].Add(diffList[i].Last() + diffList[i + 1].Last());
        }
    }
}
