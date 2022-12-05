namespace AdventOfCode.Days;

[Day(2022, 5)]
public class Day05 : BaseDay
{
    public override string PartOne(string input)
    {
        var stacks = new List<List<char>>();

        stacks.Add(new List<char>() { 'N', 'R', 'G', 'P' });
        stacks.Add(new List<char>() { 'J', 'T', 'B', 'L', 'F', 'G', 'D', 'C' });
        stacks.Add(new List<char>() { 'M', 'S', 'V' });
        stacks.Add(new List<char>() { 'L', 'S', 'R', 'C', 'Z', 'P' });
        stacks.Add(new List<char>() { 'P', 'S', 'L', 'V', 'C', 'W', 'D', 'Q' });
        stacks.Add(new List<char>() { 'C', 'T', 'N', 'W', 'D', 'M', 'S' });
        stacks.Add(new List<char>() { 'H', 'D', 'G', 'W', 'P' });
        stacks.Add(new List<char>() { 'Z', 'L', 'P', 'H', 'S', 'C', 'M', 'V' });
        stacks.Add(new List<char>() { 'R', 'P', 'F', 'L', 'W', 'G', 'Z' });

        input = input.Substring(input.IndexOf("move"));

        var moves = input.Lines();

        foreach (var move in moves)
        {
            var words = move.Words().ToList();

            var count = int.Parse(words[1]);
            var start = int.Parse(words[3]);
            var end = int.Parse(words[5]);

            for (var i = 0; i < count; i++)
            {
                MoveCrate(stacks, start, end);
            }
        }

        var result = string.Empty;

        foreach (var stack in stacks)
        {
            result += stack.Last();
        }

        return result;
    }

    private void MoveCrate(List<List<char>> stacks, int start, int end)
    {
        var crate = stacks[start - 1].Last();
        stacks[start - 1].RemoveAt(stacks[start - 1].Count - 1);
        stacks[end - 1].Add(crate);
    }

    private void MoveCrates(List<List<char>> stacks, int start, int end, int count)
    {
        var crates = stacks[start - 1].Skip(stacks[start - 1].Count - count).Take(count).ToList();
        stacks[start - 1] = stacks[start - 1].Take(stacks[start - 1].Count - count).ToList();
        stacks[end - 1].AddRange(crates);
    }

    public override string PartTwo(string input)
    {
        var stacks = new List<List<char>>();

        stacks.Add(new List<char>() { 'N', 'R', 'G', 'P' });
        stacks.Add(new List<char>() { 'J', 'T', 'B', 'L', 'F', 'G', 'D', 'C' });
        stacks.Add(new List<char>() { 'M', 'S', 'V' });
        stacks.Add(new List<char>() { 'L', 'S', 'R', 'C', 'Z', 'P' });
        stacks.Add(new List<char>() { 'P', 'S', 'L', 'V', 'C', 'W', 'D', 'Q' });
        stacks.Add(new List<char>() { 'C', 'T', 'N', 'W', 'D', 'M', 'S' });
        stacks.Add(new List<char>() { 'H', 'D', 'G', 'W', 'P' });
        stacks.Add(new List<char>() { 'Z', 'L', 'P', 'H', 'S', 'C', 'M', 'V' });
        stacks.Add(new List<char>() { 'R', 'P', 'F', 'L', 'W', 'G', 'Z' });

        input = input.Substring(input.IndexOf("move"));

        var moves = input.Lines();

        foreach (var move in moves)
        {
            var words = move.Words().ToList();

            var count = int.Parse(words[1]);
            var start = int.Parse(words[3]);
            var end = int.Parse(words[5]);

            MoveCrates(stacks, start, end, count);
        }

        var result = string.Empty;

        foreach (var stack in stacks)
        {
            result += stack.Last();
        }

        return result;
    }
}
