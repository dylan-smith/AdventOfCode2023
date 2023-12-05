namespace AdventOfCode.Days;

[Day(2023, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var cards = input.Lines().Select(ParseCard).ToList();

        var result = cards.Sum(x => ScoreCard(x));

        return result.ToString();
    }

    private long ScoreCard((List<long> winningNumbers, List<long> myNumbers) card)
    {
        var winnerCount = CountWinningNumbers(card.winningNumbers, card.myNumbers);

        if (winnerCount == 0)
        {
            return 0;
        }

        return (long)Math.Pow(2, winnerCount - 1);
    }

    private int CountWinningNumbers(List<long> winningNumbers, List<long> myNumbers)
    {
        return myNumbers.Count(x => winningNumbers.Contains(x));
    }

    private (List<long> winningNumbers, List<long> myNumbers) ParseCard(string line)
    {
        var numbers = line.Split(":")[1].Split("|");
        var winningNumbers = numbers[0].Longs().ToList();
        var myNumbers = numbers[1].Longs().ToList();

        return (winningNumbers, myNumbers);
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
