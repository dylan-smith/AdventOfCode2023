namespace AdventOfCode.Days;

[Day(2023, 12)]
public class Day12 : BaseDay
{
    private SpringStatus[] _sharedMemory;
    private List<int> _groups;
    private List<SpringStatus> _springs;
    private long _validCount = 0L;

    public override string PartOne(string input)
    {
        var rows = input.ParseLines(ParseLine).ToList();
        var result = 0L;

        foreach (var (Springs, Groups) in rows)
        {
            var possibilities = GeneratePossibilities(Springs);

            foreach (var possibility in possibilities)
            {
                if (IsValid(possibility, Groups))
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    private bool IsValid(List<SpringStatus> possibility, List<int> groups)
    {
        var g = 0;
        var count = 0;
        var active = false;

        for (int i = 0; i < possibility.Count; i++)
        {
            if (possibility[i] == SpringStatus.Damaged)
            {
                active = true;
                count++;

                if (g >= groups.Count)
                {
                    return false;
                }
            }
            else
            {
                if (active)
                {
                    if (count != groups[g])
                    {
                        return false;
                    }

                    active = false;
                    count = 0;
                    g++;
                }
            }
        }

        if (active)
        {
            if (count != groups[g])
            {
                return false;
            }

            g++;
        }

        if (g == groups.Count)
        {
            return true;
        }

        return false;
    }

    private IEnumerable<List<SpringStatus>> GeneratePossibilities(List<SpringStatus> springs)
    {
        var unknownIndexes = GetUnknownIndexes(springs);
        var unknownCount = unknownIndexes.Count;

        for (var i = 0; i < Math.Pow(2, unknownCount); i++)
        {
            var binary = i.ToBinary();
            binary = binary.PadLeft(unknownCount, '0');

            var result = new List<SpringStatus>(springs);

            for (var pos = 0; pos < unknownCount; pos++)
            {
                if (binary[pos] == '1')
                {
                    result[unknownIndexes[pos]] = SpringStatus.Damaged;
                }
                else
                {
                    result[unknownIndexes[pos]] = SpringStatus.Operational;
                }
            }

            yield return result;
        }
    }

    private List<int> GetUnknownIndexes(List<SpringStatus> springs)
    {
        var result = new List<int>();

        for (var i = 0; i < springs.Count; i++)
        {
            if (springs[i] == SpringStatus.Unknown)
            {
                result.Add(i);
            }
        }

        return result;
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
        var rows = input.ParseLines(ParseLine).ToList();
        rows = rows.Select(r => ExpandRow(r)).ToList();
        var result = 0L;

        foreach (var (Springs, Groups) in rows)
        {
            _groups = Groups;
            _springs = Springs;

            _sharedMemory = new SpringStatus[Springs.Count];
            GeneratePossibilities2(Springs.Count, Groups.Count);

            //foreach (var possibility in possibilities)
            //{
            //    if (IsValid2(_sharedMemory, Springs))
            //    {
            //        result++;
            //    }
            //}
        }

        return _validCount.ToString();
    }

    private bool IsValid2(SpringStatus[] possibility, List<SpringStatus> springs)
    {
        for (var i = 0; i < springs.Count; i++)
        {
            if (springs[i] != SpringStatus.Unknown && possibility[i] != springs[i])
            {
                return false;
            }
        }

        return true;
    }

    private void IsValid3()
    {
        for (var i = 0; i < _springs.Count; i++)
        {
            if (_springs[i] != SpringStatus.Unknown && _sharedMemory[i] != _springs[i])
            {
                return;
            }
        }

        _validCount++;
    }

    private void GeneratePossibilities2(int springsCount, int groupsCount)
    {
        var springsSkip = _sharedMemory.Length - springsCount;

        if (groupsCount == 0)
        {
            for (var i = 0; i < springsCount; i++)
            {
                _sharedMemory[springsSkip + i] = SpringStatus.Operational;
            }

            IsValid3();
        }
        else
        {
            var groupsSkip = _groups.Count - groupsCount;
            var currentGroup = _groups[groupsSkip];
            var groupSum = 0;

            for (var i = groupsSkip; i < _groups.Count; i++)
            {
                groupSum += _groups[i];
            }

            var maxOffset = springsCount - (groupSum + groupsCount - 1);

            for (var offset = 0; offset <= maxOffset; offset++)
            {
                var pos = 0;

                for (var i = 0; i < offset; i++)
                {
                    _sharedMemory[springsSkip + pos++] = SpringStatus.Operational;
                }

                for (var i = 0; i < currentGroup; i++)
                {
                    _sharedMemory[springsSkip + pos++] = SpringStatus.Damaged;
                }

                if (groupsCount > 1)
                {
                    _sharedMemory[springsSkip + pos++] = SpringStatus.Operational;
                }

                var newLength = springsCount - pos;

                GeneratePossibilities2(newLength, groupsCount - 1);
            }
        }
    }

    private (List<SpringStatus> Springs, List<int> Groups) ExpandRow((List<SpringStatus> Springs, List<int> Groups) row)
    {
        var springs = new List<SpringStatus>();

        springs.AddRange(row.Springs);
        springs.Add(SpringStatus.Unknown);
        springs.AddRange(row.Springs);
        springs.Add(SpringStatus.Unknown);
        springs.AddRange(row.Springs);
        //springs.Add(SpringStatus.Unknown);
        //springs.AddRange(row.Springs);
        //springs.Add(SpringStatus.Unknown);
        //springs.AddRange(row.Springs);

        var groups = new List<int>();

        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);
        //groups.AddRange(row.Groups);
        //groups.AddRange(row.Groups);

        return (springs, groups);
    }

    private enum SpringStatus
    {
        Operational,
        Damaged,
        Unknown
    }
}
