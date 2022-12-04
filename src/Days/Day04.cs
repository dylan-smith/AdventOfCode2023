namespace AdventOfCode.Days;

[Day(2022, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var lines = input.Lines().ToList();
        var score = 0;
        
        foreach (var line in lines)
        {
            var words = line.Words().ToList();
            var firstElf = words.First().Split('-');
            var secondElf = words.Last().Split('-');

            if (int.Parse(firstElf[0]) <= int.Parse(secondElf[0]) && int.Parse(firstElf[1]) >= int.Parse(secondElf[1]))
            {
                score++;
            }
            else
            {
                if (int.Parse(secondElf[0]) <= int.Parse(firstElf[0]) && int.Parse(secondElf[1]) >= int.Parse(firstElf[1]))
                {
                    score++;
                }
            }    
        }

        return score.ToString();
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines().ToList();
        var score = 0;

        foreach (var line in lines)
        {
            var words = line.Words().ToList();
            var firstElf = words.First().Split('-');
            var secondElf = words.Last().Split('-');

            var firstElfNums = firstElf.Select(int.Parse);
            var secondElfNums = secondElf.Select(int.Parse);

            if ((firstElfNums.Last() >= secondElfNums.First() && firstElfNums.First() <= secondElfNums.Last()) || 
                (secondElfNums.Last() >= firstElfNums.First() && secondElfNums.First() <= firstElfNums.First()))
            {
                score++;
            }
        }

        return score.ToString();
    }
}
