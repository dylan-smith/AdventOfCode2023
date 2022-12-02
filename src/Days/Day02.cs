using System.ComponentModel;
using System.Threading.Tasks.Sources;

namespace AdventOfCode.Days;

public enum RPSMove
{
    Rock,
    Paper,
    Scissors
}

public enum RPSResult
{
    Win,
    Lose,
    Draw
}

[Day(2022, 2)]
public class Day02 : BaseDay
{
    private readonly Dictionary<(RPSMove opponent, RPSMove me), RPSResult> RPS = new()
    {
            { (RPSMove.Rock, RPSMove.Rock), RPSResult.Draw },
            { (RPSMove.Rock, RPSMove.Paper), RPSResult.Win },
            { (RPSMove.Rock, RPSMove.Scissors), RPSResult.Lose },
            { (RPSMove.Paper, RPSMove.Rock), RPSResult.Lose },
            { (RPSMove.Paper, RPSMove.Paper), RPSResult.Draw },
            { (RPSMove.Paper, RPSMove.Scissors), RPSResult.Win },
            { (RPSMove.Scissors, RPSMove.Rock), RPSResult.Win },
            { (RPSMove.Scissors, RPSMove.Paper), RPSResult.Lose },
            { (RPSMove.Scissors, RPSMove.Scissors), RPSResult.Draw },
    };
    
    public override string PartOne(string input)
    {
        var games = input.ParseLines(ParseGame);
        var results = games.Select(g => RPS[(g.opponent, g.me)]);
        var score = results.Count(r => r == RPSResult.Win) * 6;
        score += results.Count(r => r == RPSResult.Draw) * 3;

        score += games.Count(g => g.me == RPSMove.Rock);
        score += games.Count(g => g.me == RPSMove.Paper) * 2;
        score += games.Count(g => g.me == RPSMove.Scissors) * 3;

        return score.ToString();
    }

    private (RPSMove opponent, RPSMove me) ParseGame(string line) => (ParseRPSMove(line.Words().First()), ParseRPSMove(line.Words().Last()));

    private RPSMove ParseRPSMove(string move)
    {
        return move switch
        {
            "A" => RPSMove.Rock,
            "B" => RPSMove.Paper,
            "C" => RPSMove.Scissors,
            "X" => RPSMove.Rock,
            "Y" => RPSMove.Paper,
            "Z" => RPSMove.Scissors,
            _ => throw new Exception($"Unknown move: {move}")
        };
    }

    public override string PartTwo(string input)
    {
        var games = input.ParseLines(ParseGame2);
        var score = games.Count(g => g.result == RPSResult.Win) * 6;
        score += games.Count(g => g.result == RPSResult.Draw) * 3;

        var moves = games.Select(g => GetMove(g.opponent, g.result));

        score += moves.Count(m => m == RPSMove.Rock);
        score += moves.Count(m => m == RPSMove.Paper) * 2;
        score += moves.Count(m => m == RPSMove.Scissors) * 3;

        return score.ToString();
    }

    private RPSMove GetMove(RPSMove opponent, RPSResult result)
    {
        return RPS.Where(x => x.Key.opponent == opponent && x.Value == result)
                  .Select(x => x.Key.me)
                  .Single();
    }

    private (RPSMove opponent, RPSResult result) ParseGame2(string line) => (ParseRPSMove(line.Words().First()), ParseRPSResult(line.Words().Last()));
    
    private RPSResult ParseRPSResult(string result)
    {
        return result switch
        {
            "X" => RPSResult.Lose,
            "Y" => RPSResult.Draw,
            "Z" => RPSResult.Win,
            _ => throw new Exception($"Unknown result: {result}")
        };
    }
}
