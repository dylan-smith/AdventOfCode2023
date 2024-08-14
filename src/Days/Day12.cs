namespace AdventOfCode.Days;

[Day(2023, 12)]
public class Day12 : BaseDay
{
    private Dictionary<(int position, int group, int damagedCount), long> _seen = new Dictionary<(int position, int group, int damagedCount), long>();

    public override string PartOne(string input)
    {
        var rows = input.ParseLines(ParseLine);
        var result = 0L;

        foreach (var (Springs, Groups) in rows)
        {
            _seen = new Dictionary<(int position, int group, int damagedCount), long>();
            result += CountSolutions(position: 0, group: 0, damagedCount: 0, Springs, Groups);
        }

        return result.ToString();
    }

    private (List<SpringStatus> Springs, List<int> Groups) ParseLine(string line)
    {
        var springs = line.Split(' ')[0];
        var groups = line.Split(' ')[1].Integers().ToList();
        var result = springs.Select(c => c switch { '.' => SpringStatus.Operational, '#' => SpringStatus.Damaged, _ => SpringStatus.Unknown }).ToList();

        return (result, groups);
    }

    public override string PartTwo(string input)
    {
        var rows = input.ParseLines(ParseLine);
        rows = rows.Select(r => ExpandRow(r));
        var result = 0L;

        foreach (var (Springs, Groups) in rows)
        {
            _seen = new Dictionary<(int position, int group, int damagedCount), long>();
            result += CountSolutions(position: 0, group: 0, damagedCount: 0, Springs, Groups);
        }

        return result.ToString();
    }

    private long CountSolutions(int position, int group, int damagedCount, List<SpringStatus> springs, List<int> groups)
    {
        var result = 0L;

        if (_seen.TryGetValue((position, group, damagedCount), out var value))
        {
            return value;
        }

        if (damagedCount > 0 && group == groups.Count)
        {
            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        if (damagedCount > 0 && damagedCount > groups[group])
        {
            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        if (position >= springs.Count)
        {
            if (damagedCount > 0 && damagedCount == groups[group] && group == (groups.Count - 1))
            {
                _seen.Add((position, group, damagedCount), 1);
                return 1;
            }

            if (damagedCount == 0 && group == groups.Count)
            {
                _seen.Add((position, group, damagedCount), 1);
                return 1;
            }

            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        var current = springs[position];

        if (current != SpringStatus.Unknown)
        {
            if (current == SpringStatus.Damaged)
            {
                result = CountSolutions(position + 1, group, damagedCount + 1, springs, groups);
                _seen.Add((position, group, damagedCount), result);
                return result;
            }

            if (damagedCount > 0 && current == SpringStatus.Operational && damagedCount < groups[group])
            {
                _seen.Add((position, group, damagedCount), 0);
                return 0;
            }

            if (damagedCount > 0 && damagedCount == groups[group])
            {
                result = CountSolutions(position + 1, group + 1, 0, springs, groups);
                _seen.Add((position, group, damagedCount), result);
                return result;
            }

            result =  CountSolutions(position + 1, group, damagedCount, springs, groups);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (damagedCount > 0 && damagedCount < groups[group])
        {
            result =  CountSolutions(position + 1, group, damagedCount + 1, springs, groups);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (damagedCount > 0 && damagedCount == groups[group])
        {
            result = CountSolutions(position + 1, group + 1, 0, springs, groups);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (group == groups.Count)
        {
            result = CountSolutions(position + 1, group, 0, springs, groups);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        result += CountSolutions(position + 1, group, 0, springs, groups);

        if (group < groups.Count)
        {
            result += CountSolutions(position + 1, group, 1, springs, groups);
        }

        _seen.Add((position, group, damagedCount), result);
        return result;
    }

    private (List<SpringStatus> Springs, List<int> Groups) ExpandRow((List<SpringStatus> Springs, List<int> Groups) row)
    {
        var springs = new List<SpringStatus>();
        var groups = new List<int>();

        Enumerable.Range(0, 5).ForEach(_ =>
        {
            springs.AddRange(row.Springs);
            springs.Add(SpringStatus.Unknown);
        });

        springs.RemoveLast();

        Enumerable.Range(0, 5).ForEach(_ => groups.AddRange(row.Groups));

        return (springs, groups);
    }

    private enum SpringStatus
    {
        Operational,
        Damaged,
        Unknown
    }
}
