namespace AdventOfCode.Days;

[Day(2022, 5)]
public class Day05 : BaseDay
{
    public override string PartOne(string input)
    {
        var stacks = ParseStacks(input[..input.IndexOf("move")]);
        
        input = input[input.IndexOf("move")..];

        var moves = input.ParseLines(ParseMove);
        moves.ForEach(m => m.count.Times(() => MoveCrate(stacks, m.from, m.to)));

        var result = stacks.Aggregate("", (acc, s) => acc + s.Last());

        return result;
    }

    private List<List<char>> ParseStacks(string v)
    {
        var stacks = new List<List<char>>
        {
            new List<char>() { 'N', 'R', 'G', 'P' },
            new List<char>() { 'J', 'T', 'B', 'L', 'F', 'G', 'D', 'C' },
            new List<char>() { 'M', 'S', 'V' },
            new List<char>() { 'L', 'S', 'R', 'C', 'Z', 'P' },
            new List<char>() { 'P', 'S', 'L', 'V', 'C', 'W', 'D', 'Q' },
            new List<char>() { 'C', 'T', 'N', 'W', 'D', 'M', 'S' },
            new List<char>() { 'H', 'D', 'G', 'W', 'P' },
            new List<char>() { 'Z', 'L', 'P', 'H', 'S', 'C', 'M', 'V' },
            new List<char>() { 'R', 'P', 'F', 'L', 'W', 'G', 'Z' }
        };

        return stacks;
    }

    private (int count, int from, int to) ParseMove(string line)
    {
        var words = line.Words().ToList();

        var count = int.Parse(words[1]);
        var from = int.Parse(words[3]);
        var to = int.Parse(words[5]);

        return (count, from, to);
    }

    private void MoveCrate(List<List<char>> stacks, int from, int to)
    {
        var fromStack = stacks[from - 1];
        var toStack = stacks[to - 1];
        
        var crate = fromStack.Pop();
        toStack.Add(crate);
    }

    private void MoveCrates(List<List<char>> stacks, int from, int to, int count)
    {
        var fromStack = stacks[from - 1];
        var toStack = stacks[to - 1];

        var crates = fromStack.Pop(count);
        toStack.AddRange(crates);
    }

    public override string PartTwo(string input)
    {
        var stacks = ParseStacks(input[..input.IndexOf("move")]);

        input = input[input.IndexOf("move")..];

        var moves = input.ParseLines(ParseMove);
        moves.ForEach(m => MoveCrates(stacks, m.from, m.to, m.count));

        var result = stacks.Aggregate("", (acc, s) => acc + s.Last());

        return result;
    }
}
