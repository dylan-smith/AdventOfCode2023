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

        foreach (var (Springs, Groups) in rows)
        {
            _groups = Groups;
            _springs = Springs;
            _sharedMemory = new SpringStatus[Springs.Count];
            
            CountSolutions(Springs.Count, Groups.Count);
        }

        return _validCount.ToString();
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

        foreach (var (Springs, Groups) in rows)
        {
            _groups = Groups;
            _springs = Springs;
            _sharedMemory = new SpringStatus[Springs.Count];
            
            CountSolutions(Springs.Count, Groups.Count);
        }

        return _validCount.ToString();
    }

    private void IsValid()
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

    private void CountSolutions(int springsCount, int groupsCount)
    {
        var springsSkip = _sharedMemory.Length - springsCount;

        if (groupsCount == 0)
        {
            for (var i = 0; i < springsCount; i++)
            {
                _sharedMemory[springsSkip + i] = SpringStatus.Operational;
            }

            IsValid();
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

                CountSolutions(newLength, groupsCount - 1);
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
