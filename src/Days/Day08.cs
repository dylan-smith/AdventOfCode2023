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
            pos = nodes[GetNewNodeName(pos, direction.Value)];
            direction = direction.NextCircular();
            moves++;
        }

        return moves.ToString();
    }

    public override string PartTwo(string input)
    {
        var directionList = new LinkedList<char>(input.Lines().First());
        var nodes = MakeNodesDictionary(input.Lines().Skip(1));
        var startNodes = nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value).ToList();
        var zMoves = new List<long>();

        foreach (var startNode in startNodes)
        {
            var curNode = startNode;
            var direction = directionList.First;
            var moves = 0L;

            while (!curNode.Name.EndsWith('Z'))
            {
                curNode = nodes[GetNewNodeName(curNode, direction.Value)];
                direction = direction.NextCircular();
                moves++;
            }

            zMoves.Add(moves);
        }

        // This doesn't solve the general case, but it does solve the specific way the input seemed to be structured
        return zMoves.LeastCommonMultiple().ToString();
    }

    private string GetNewNodeName(MapNode pos, char direction) => direction == 'L' ? pos.Left : pos.Right;

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