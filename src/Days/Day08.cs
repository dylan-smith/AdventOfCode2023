
namespace AdventOfCode.Days;

[Day(2023, 8)]
public class Day08 : BaseDay
{
    public override string PartOne(string input)
    {
        var direction = new LinkedList<char>(input.Lines().First()).First;
        var nodes = MakeNodesDictionary(input.Lines().Skip(1));

        var pos = nodes["AAA"];
        var moves = 0;

        while (pos.Name != "ZZZ")
        {
            var newNodeName = UpdatePosition(pos, direction.Value);
            pos = nodes[newNodeName];
            direction = direction.NextCircular();
            moves++;
        }

        return moves.ToString();
    }

    private string UpdatePosition(MapNode pos, char value)
    {
        return value == 'L' ? pos.Left : pos.Right;
    }

    private (string Name, string Left, string Right) ParseNode(string input)
    {
        var pieces = input.Split(new string[] { " ", "=", "(", ")", "," }, StringSplitOptions.RemoveEmptyEntries);

        return (pieces[0], pieces[1], pieces[2]);
    }

    public override string PartTwo(string input)
    {
        var directions = input.Lines().First();
        var nodes = MakeNodesDictionary(input.Lines().Skip(1));

        var curNodes = nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value).ToList();

        var cycles = new List<(MapNode node, int direction, long length)>();
        var zNodes = new List<List<long>>();

        for (var n = 0; n < curNodes.Count; n++)
        {
            var direction = 0;
            var seen = new Dictionary<(MapNode node, int direction), long>();
            var moves = 0L;
            zNodes.Add(new List<long>());

            while (!seen.ContainsKey((curNodes[n], direction)))
            {
                seen.Add((curNodes[n], direction), moves);

                if (curNodes[n].Name.EndsWith('Z'))
                {
                    zNodes[n].Add(moves);
                }

                var newNodeName = UpdatePosition(curNodes[n], directions[direction]);
                curNodes[n] = nodes[newNodeName];
                direction = (direction + 1) % directions.Length;
                moves++;
            }

            var firstHit = seen[(curNodes[n], direction)];
            var cycleLength = moves - firstHit;
            cycles.Add((curNodes[n], direction, cycleLength));

            Log($"Start Cycle: {firstHit}, Cycle Length: {cycleLength}, Node: {curNodes[n].Name}, Direction: {direction}");
            foreach (var z in zNodes[n])
            {
                Log($"  Z Node: {z}");
            }
        }

        return string.Empty;
    }

    private IDictionary<string, MapNode> MakeNodesDictionary(IEnumerable<string> lines)
    {
        var result = new Dictionary<string, MapNode>();

        foreach (var line in lines)
        {
            var pieces = line.Split(new string[] { " ", "=", "(", ")", "," }, StringSplitOptions.RemoveEmptyEntries);
            result.Add(pieces[0], new(pieces[0], pieces[1], pieces[2]));
        }

        return result;
    }

    private record MapNode(string Name, string Left, string Right);
}