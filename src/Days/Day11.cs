using System.Numerics;
using static AdventOfCode.Days.Day11;

namespace AdventOfCode.Days;

[Day(2022, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var monkey1 = new Monkey(new List<long> { 56, 56, 92, 65, 71, 61, 79 }, x => x * 7, 3, 3, 7);
        var monkey2 = new Monkey(new List<long> { 61, 85 }, x => x + 5, 11, 6, 4);
        var monkey3 = new Monkey(new List<long> { 54, 96, 82, 78, 69 }, x => x * x, 7, 0, 7);
        var monkey4 = new Monkey(new List<long> { 57, 59, 65, 95 }, x => x + 4, 2, 5, 1);
        var monkey5 = new Monkey(new List<long> { 62, 67, 80 }, x => x * 17, 19, 2, 6);
        var monkey6 = new Monkey(new List<long> { 91 }, x => x + 7, 5, 1, 4);
        var monkey7 = new Monkey(new List<long> { 79, 83, 64, 52, 77, 56, 63, 92 }, x => x + 6, 17, 2, 0);
        var monkey8 = new Monkey(new List<long> { 50, 97, 76, 96, 80, 56 }, x => x + 3, 13, 3, 5);

        var monkeys = new List<Monkey> { monkey1, monkey2, monkey3, monkey4, monkey5, monkey6, monkey7, monkey8 };

        for (var round = 0; round < 20; round++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey._items.Any())
                {
                    monkey.InspectionCount++;
                    var newItem = monkey._worryFunc(monkey._items.First());
                    newItem /= 3;
                    
                    if (newItem % monkey._divisible == 0)
                    {
                        monkeys[monkey._trueMonkey]._items.Add(newItem);
                    }
                    else
                    {
                        monkeys[monkey._falseMonkey]._items.Add(newItem);
                    }

                    monkey._items.RemoveFirst();
                }
            }
        }

        return monkeys.OrderByDescending(x => x.InspectionCount)
                      .Take(2)
                      .Multiply(x => x.InspectionCount)
                      .ToString();
    }

    public override string PartTwo(string input)
    {
        var monkey1 = new Monkey(new List<long> { 56, 56, 92, 65, 71, 61, 79 }, x => x * 7, 3, 3, 7);
        var monkey2 = new Monkey(new List<long> { 61, 85 }, x => x + 5, 11, 6, 4);
        var monkey3 = new Monkey(new List<long> { 54, 96, 82, 78, 69 }, x => x * x, 7, 0, 7);
        var monkey4 = new Monkey(new List<long> { 57, 59, 65, 95 }, x => x + 4, 2, 5, 1);
        var monkey5 = new Monkey(new List<long> { 62, 67, 80 }, x => x * 17, 19, 2, 6);
        var monkey6 = new Monkey(new List<long> { 91 }, x => x + 7, 5, 1, 4);
        var monkey7 = new Monkey(new List<long> { 79, 83, 64, 52, 77, 56, 63, 92 }, x => x + 6, 17, 2, 0);
        var monkey8 = new Monkey(new List<long> { 50, 97, 76, 96, 80, 56 }, x => x + 3, 13, 3, 5);

        var monkeys = new List<Monkey> { monkey1, monkey2, monkey3, monkey4, monkey5, monkey6, monkey7, monkey8 };

        var modulo = monkeys.Multiply(x => x._divisible);

        for (var round = 0; round < 10000; round++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey._items.Any())
                {
                    monkey.InspectionCount++;
                    var newItem = monkey._worryFunc(monkey._items.First());
                    newItem %= modulo;

                    if (newItem % monkey._divisible == 0)
                    {
                        monkeys[monkey._trueMonkey]._items.Add(newItem);
                    }
                    else
                    {
                        monkeys[monkey._falseMonkey]._items.Add(newItem);
                    }

                    monkey._items.RemoveFirst();
                }
            }
        }

        return monkeys.OrderByDescending(x => x.InspectionCount)
                      .Take(2)
                      .Multiply(x => x.InspectionCount)
                      .ToString();
    }
    
    public class Monkey
    {
        public readonly List<long> _items;
        public readonly Func<long, long> _worryFunc;
        public readonly long _divisible;
        public readonly int _trueMonkey;
        public readonly int _falseMonkey;
        public long InspectionCount = 0;
        
        public Monkey(List<long> items, Func<long, long> worryFunc, long divisible, int trueMonkey, int falseMonkey)
        {
            _items = items;
            _worryFunc = worryFunc;
            _divisible = divisible;
            _trueMonkey = trueMonkey;
            _falseMonkey = falseMonkey;
        }
    }
}
