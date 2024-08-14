
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;
using System.Windows.Navigation;

namespace AdventOfCode.Days;

[Day(2023, 12)]
public class Day12 : BaseDay
{
    private SpringStatus[] _sharedMemory;
    private List<int> _groups;
    private List<SpringStatus> _springs;
    private long _validCount = 0L;
    private Dictionary<(int position, int group, int damagedCount), long> _seen = new Dictionary<(int position, int group, int damagedCount), long>();

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
        var result = 0L;

        foreach (var (Springs, Groups) in rows)
        {
            _groups = Groups;
            _springs = Springs;
            _sharedMemory = Springs.ToArray();

            _seen = new Dictionary<(int position, int group, int damagedCount), long>();
            result += CountSolutions2(position: 0, group: 0, damagedCount: 0);
        }

        return result.ToString();
    }

    private long CountSolutions2(int position, int group, int damagedCount)
    {
        var result = 0L;

        if (_seen.TryGetValue((position, group, damagedCount), out var value))
        {
            return value;
        }

        if (damagedCount > 0 && group == _groups.Count)
        {
            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        if (damagedCount > 0 && damagedCount > _groups[group])
        {
            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        if (position >= _sharedMemory.Length)
        {
            if (damagedCount > 0 && damagedCount == _groups[group] && group == (_groups.Count - 1))
            {
                _seen.Add((position, group, damagedCount), 1);
                return 1;
            }

            if (damagedCount == 0 && group == _groups.Count)
            {
                _seen.Add((position, group, damagedCount), 1);
                return 1;
            }

            _seen.Add((position, group, damagedCount), 0);
            return 0;
        }

        var current = _sharedMemory[position];

        if (current != SpringStatus.Unknown)
        {
            if (current == SpringStatus.Damaged)
            {
                result = CountSolutions2(position + 1, group, damagedCount + 1);
                _seen.Add((position, group, damagedCount), result);
                return result;
            }

            if (damagedCount > 0 && current == SpringStatus.Operational && damagedCount < _groups[group])
            {
                _seen.Add((position, group, damagedCount), 0);
                return 0;
            }

            if (damagedCount > 0 && damagedCount == _groups[group])
            {
                result = CountSolutions2(position + 1, group + 1, 0);
                _seen.Add((position, group, damagedCount), result);
                return result;
            }

            result =  CountSolutions2(position + 1, group, damagedCount);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (damagedCount > 0 && damagedCount < _groups[group])
        {
            result =  CountSolutions2(position + 1, group, damagedCount + 1);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (damagedCount > 0 && damagedCount == _groups[group])
        {
            result = CountSolutions2(position + 1, group + 1, 0);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        if (group == _groups.Count)
        {
            result = CountSolutions2(position + 1, group, 0);
            _seen.Add((position, group, damagedCount), result);
            return result;
        }

        result += CountSolutions2(position + 1, group, 0);

        if (group < _groups.Count)
        {
            result += CountSolutions2(position + 1, group, 1);
        }

        _seen.Add((position, group, damagedCount), result);
        return result;
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
        springs.Add(SpringStatus.Unknown);
        springs.AddRange(row.Springs);
        springs.Add(SpringStatus.Unknown);
        springs.AddRange(row.Springs);

        var groups = new List<int>();

        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);
        groups.AddRange(row.Groups);

        return (springs, groups);
    }

    private enum SpringStatus
    {
        Operational,
        Damaged,
        Unknown
    }
}
