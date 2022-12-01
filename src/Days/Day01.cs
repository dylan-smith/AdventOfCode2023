namespace AdventOfCode.Days;

[Day(2022, 1)]
public class Day01 : BaseDay
{
    public override string PartOne(string input)
    {
        var calories = input.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

        var elfs = new List<int>();
        var elf = 0;

        foreach (var c in calories)
        {
            if (c != string.Empty)
            {
                elf += int.Parse(c);
            }
            
            if (c == string.Empty)
            {
                elfs.Add(elf);
                elf = 0;
            }
        }

        elfs.Add(elf);

        return elfs.Max().ToString();
    }

    public override string PartTwo(string input)
    {
        var calories = input.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

        var elfs = new List<int>();
        var elf = 0;

        foreach (var c in calories)
        {
            if (c != string.Empty)
            {
                elf += int.Parse(c);
            }

            if (c == string.Empty)
            {
                elfs.Add(elf);
                elf = 0;
            }
        }

        elfs.Add(elf);

        var count = elfs.Count;

        elfs.Sort();

        return (elfs[count - 1] + elfs[count - 2] + elfs[count - 3]).ToString();
    }
}
