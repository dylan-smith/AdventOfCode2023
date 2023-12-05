namespace AdventOfCode.Days;

[Day(2023, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var cards = input.ParseLines(ParseCard).ToList();

        var result = cards.Sum(x => ScoreCard(x));

        return result.ToString();
    }

    private long ScoreCard((int cardNumber, long copies, List<long> winningNumbers, List<long> myNumbers) card)
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

    private (int cardNumber, long copies, List<long> winningNumbers, List<long> myNumbers) ParseCard(string line)
    {
        var cardNumber = line.Split(":")[0].Words().Last().ParseInt();

        var numbers = line.Split(":")[1].Split("|");
        var winningNumbers = numbers[0].Longs().ToList();
        var myNumbers = numbers[1].Longs().ToList();

        return (cardNumber, 1, winningNumbers, myNumbers);
    }

    public override string PartTwo(string input)
    {
        var cards = input.ParseLines(ParseCard).ToList();

        for (var cardNumber = 1; cardNumber <= cards.Count; cardNumber++)
        {
            var card = cards[cardNumber - 1];
            var score = CountWinningNumbers(card.winningNumbers, card.myNumbers);

            for (var j = cardNumber + 1; j <= cardNumber + score; j++)
            {
                cards[j - 1] = (cards[j - 1].cardNumber, cards[j - 1].copies + card.copies, cards[j - 1].winningNumbers, cards[j - 1].myNumbers);
            }
        }

        var cardCount = cards.Sum(x => x.copies);

        return cardCount.ToString();
    }
}
