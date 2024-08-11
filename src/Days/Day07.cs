namespace AdventOfCode.Days;

[Day(2023, 7)]
public class Day07 : BaseDay
{
    public override string PartOne(string input)
    {
        var hands = input.ParseLines(line => ParseHand(line, withWildcards: false)).ToList();

        hands.Sort((a, b) => b.strength.CompareTo(a.strength));

        var n = hands.Count;
        var result = 0L;

        foreach (var hand in hands)
        {
            result += hand.bid * n;
            n--;
        }

        return result.ToString();
    }

    private (string cards, long bid, long strength) ParseHand(string line, bool withWildcards)
    {
        var cards = line.Words().First();
        var bid = long.Parse(line.Words().Last());
        var strength = GetHandStrength(cards, withWildcards);

        return (cards, bid, strength);
    }

    private long GetHandStrength(string cards, bool withWildcards)
    {
        var n = 10000000000L;

        var cardCounts = GetCardCounts(cards);
        var handType = 0L;

        if (withWildcards && cardCounts.ContainsKey('J') && cardCounts['J'] > 0)
        {
            var wildcards = "23456789TQKA".GetCombinations2(cardCounts['J']);

            foreach (var wildcard in wildcards)
            {
                var i = 0;
                var newCards = cards;

                for (var x = 0; x < cards.Length; x++)
                {
                    if (cards[x] == 'J')
                    {
                        newCards = newCards[0..x] + wildcard.ElementAt(i++) + newCards[(x + 1)..];
                    }
                }

                var newHandType = GetHandType(newCards);

                if (newHandType > handType)
                {
                    handType = newHandType;
                }
            }
        }
        else
        {
            handType = GetHandType(cards);
        }

        var result = handType * n;
        n /= 100;

        foreach (var card in cards)
        {
            result += GetCardValue(card) * n;
            n /= 100;
        }

        return result;
    }

    private long GetCardValue(char card)
    {
        return card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 1,
            'T' => 10,
            _ => long.Parse(card.ToString())
        };
    }

    private long GetHandType(string cards)
    {
        if (IsFiveOfAKind(cards))
        {
            return 7;
        }

        if (IsFourOfAKind(cards))
        {
            return 6;
        }

        if (IsFullHouse(cards))
        {
            return 5;
        }

        if (IsThreeOfAKind(cards))
        {
            return 4;
        }

        if (IsTwoPairs(cards))
        {
            return 3;
        }

        if (IsOnePair(cards))
        {
            return 2;
        }

        return 1;
    }

    private bool IsOnePair(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Any(x => x.Value == 2);
    }

    private bool IsTwoPairs(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Count(x => x.Value == 2) == 2;
    }

    private bool IsThreeOfAKind(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Any(x => x.Value == 3);
    }

    private bool IsFullHouse(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Any(x => x.Value == 3) && cardCounts.Any(x => x.Value == 2);
    }

    private bool IsFourOfAKind(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Any(x => x.Value == 4);
    }

    private bool IsFiveOfAKind(string cards)
    {
        var cardCounts = GetCardCounts(cards);

        return cardCounts.Any(x => x.Value == 5);
    }

    private Dictionary<char, int> GetCardCounts(string cards)
    {
        var result = new Dictionary<char, int>();

        foreach (var c in cards)
        {
            if (result.ContainsKey(c))
            {
                result[c]++;
            }
            else
            {
                result[c] = 1;
            }
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        var hands = input.ParseLines(line => ParseHand(line, withWildcards: true)).ToList();

        hands.Sort((a, b) => b.strength.CompareTo(a.strength));

        var n = hands.Count;
        var result = 0L;

        foreach (var hand in hands)
        {
            result += hand.bid * n;
            n--;
        }

        return result.ToString();
    }
}
