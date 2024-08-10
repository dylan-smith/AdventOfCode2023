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

    public override string PartTwo(string input)
    {
        var directionList = new LinkedList<char>(input.Lines().First());
        var nodes = MakeNodesDictionary(input.Lines().Skip(1));
        var curNodes = nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value).ToList();
        var zNodes = new List<long>();

        for (var n = 0; n < curNodes.Count; n++)
        {
            var direction = directionList.First;
            var moves = 0L;

            while (!curNodes[n].Name.EndsWith('Z'))
            {
                var newNodeName = UpdatePosition(curNodes[n], direction.Value);
                curNodes[n] = nodes[newNodeName];
                direction = direction.NextCircular();
                moves++;
            }

            zNodes.Add(moves);
        }

        // This doesn't solve the general case, but it does solve the specific way the input seemed to be structured
        return zNodes.LeastCommonMultiple().ToString();
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