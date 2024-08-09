
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
        var direction = new LinkedList<char>(input.Lines().First()).First;
        var nodes = MakeNodesDictionary(input.Lines().Skip(1));

        var curNodes = nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value).ToList();
        var moves = 0L;

        while (curNodes.Any(n => !n.Name.EndsWith('Z')))
        {
            var newNodes = new List<MapNode>();

            foreach (var node in curNodes)
            {
                var newNodeName = UpdatePosition(node, direction.Value);
                newNodes.Add(nodes[newNodeName]);
            }

            curNodes = newNodes;
            direction = direction.NextCircular();
            moves++;

            if (moves % 1000000000 == 0)
            {
                Log(moves.ToString());
            }
        }

        return moves.ToString();
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