namespace AdventOfCode.Days;

[Day(2023, 12)]
public class Day12 : BaseDay
{
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
            var springs = new SpringStatus[Springs.Count];
            var possibilities = GeneratePossibilities2(Groups, Springs.Count, springs);

            foreach (var possibility in possibilities)
            {
                if (IsValid2(springs, Springs))
                {
                    result++;
                }
            }
        }

        return result.ToString();
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

    private IEnumerable<bool> GeneratePossibilities2(List<int> groups, int length, SpringStatus[] sharedMemory)
    {
        var skip = sharedMemory.Length - length;

        if (groups.Count == 1)
        {
            for (var offset = 0; offset <= length - groups[0]; offset++)
            {
                //var springs = new List<SpringStatus>();
                var pos = 0;

                for (var i = 0; i < offset; i++)
                {
                    //springs.Add(SpringStatus.Operational);
                    sharedMemory[skip + pos++] = SpringStatus.Operational;
                }

                for (var i = 0; i < groups[0]; i++)
                {
                    //springs.Add(SpringStatus.Damaged);
                    sharedMemory[skip + pos++] = SpringStatus.Damaged;
                }

                for (var i = 0; i < length - (offset + groups[0]); i++)
                {
                    //springs.Add(SpringStatus.Operational);
                    sharedMemory[skip + pos++] = SpringStatus.Operational;
                }

                yield return true;
            }
        }
        else
        {
            var maxOffset = length - (groups.Sum() + groups.Count - 1);

            for (var offset = 0; offset <= maxOffset; offset++)
            {
                //var springs = new List<SpringStatus>();
                var pos = 0;

                for (var i = 0; i < offset; i++)
                {
                    //springs.Add(SpringStatus.Operational);
                    sharedMemory[skip + pos++] = SpringStatus.Operational;
                }

                for (var i = 0; i < groups[0]; i++)
                {
                    //springs.Add(SpringStatus.Damaged);
                    sharedMemory[skip + pos++] = SpringStatus.Damaged;
                }

                //springs.Add(SpringStatus.Operational);
                sharedMemory[skip + pos++] = SpringStatus.Operational;

                var newGroups = groups.Skip(1).ToList();
                //var newLength = length - springs.Count;
                var newLength = length - pos;

                var endings = GeneratePossibilities2(newGroups, newLength, sharedMemory);

                foreach (var ending in endings)
                {
                    //var newSprings = new List<SpringStatus>(springs);
                    //newSprings.AddRange(ending);

                    yield return true;
                }
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
