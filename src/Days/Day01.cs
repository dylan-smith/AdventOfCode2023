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

        elfs.Sort();

        var result = elfs.Window(3).Last().Sum();

        return result.ToString();
    }
}
