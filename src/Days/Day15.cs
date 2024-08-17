namespace AdventOfCode.Days;

[Day(2023, 15)]
public class Day15 : BaseDay
{
    public override string PartOne(string input)
    {
        var commands = input.Words();
        var result = 0L;

        foreach (var command in commands)
        {
            result += CalcHash(command);
        }

        return result.ToString();
    }

    private int CalcHash(string command)
    {
        var hash = 0;

        foreach (var c in command)
        {
            hash += c;
            hash *= 17;
            hash %= 256;
        }

        return hash;
    }

    public override string PartTwo(string input)
    {
        var commands = input.Words();
        var result = 0L;
        var boxes = new List<List<(string Label, int Lens)>>();

        DoMany.Do(256, () => boxes.Add(new List<(string Label, int Lens)>()));

        foreach (var command in commands)
        {
            ExecuteCommand(command, boxes);
        }

        for (var b = 0; b < boxes.Count; b++)
        {
            result += FocusingPower(b, boxes[b]);
        }

        return result.ToString();
    }

    private void ExecuteCommand(string command, List<List<(string Label, int Lens)>> boxes)
    {
        var label = command.Split('=', '-')[0];

        var box = CalcHash(label);
        var slot = boxes[box].FindIndex(b => b.Label == label);

        if (command.Contains('='))
        {
            var focus = int.Parse(command.Split('=', '-')[1]);

            if (slot >= 0)
            {
                boxes[box][slot] = (label, focus);
            }
            else
            {
                boxes[box].Add((label, focus));
            }
        }

        if (command.Contains('-'))
        {
            if (slot >= 0)
            {
                boxes[box].RemoveAt(slot);
            }
        }
    }

    private long FocusingPower(int boxNumber, List<(string Label, int Lens)> box)
    {
        var result = 0L;

        for (var slot = 0; slot < box.Count; slot++)
        {
            var focusingPower = boxNumber + 1;
            focusingPower *= slot + 1;
            focusingPower *= box[slot].Lens;

            result += focusingPower;
        }

        return result;
    }
}
