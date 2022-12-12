namespace AdventOfCode.Days;

[Day(2022, 10)]
public class Day10 : BaseDay
{
    public override string PartOne(string input)
    {
        var instructions = input.ParseLines(line => new Instruction(line));
        var vm = new VirtualMachine();
        var result = 0;

        vm.OnCycle = cycle =>
        {
            if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
            {
                result += cycle * vm.Register;
            }
        };
        
        vm.RunProgram(instructions);

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        var instructions = input.ParseLines(line => new Instruction(line));
        var vm = new VirtualMachine();
        var result = 0;

        vm.OnCycle = cycle =>
        {
            
        };

        vm.RunProgram(instructions);

        return $"{Environment.NewLine}{vm.Display.ToString2()}";
    }
}

public class Instruction
{
    private readonly string _command;
    private readonly int _arg;
    
    public Instruction(string input)
    {
        _command = input.Words().First();
        
        if (input.Words().Count() > 1)
        {
            _arg = int.Parse(input.Words().Last());
        }
    }

    public void Execute(VirtualMachine vm)
    {
        switch (_command)
        {
            case "noop":
                vm.IncreaseCycle(1);
                break;
            case "addx":
                vm.IncreaseCycle(2);
                vm.Register += _arg;
                break;
            default:
                throw new Exception();
        }
    }
}

public class VirtualMachine
{
    public int Register = 1;
    public int Cycle = 1;
    public char[,] Display;
    
    public void RunProgram(IEnumerable<Instruction> program)
    {
        Display = new char[40, 6];
        Display.Initialize('.');
        
        foreach (var instruction in program)
        {
            instruction.Execute(this);
        }
    }

    public Action<int> OnCycle { get; set; }

    public void IncreaseCycle(int count)
    {
        for (var i = 0; i < count; i++)
        {
            OnCycle(Cycle);

            DrawPixel();
            
            Cycle++;
        }
    }

    public void DrawPixel()
    {
        var x = (Cycle - 1) % 40;
        var y = (Cycle - 1) / 40;

        if (Register >= x - 1 && Register <= x + 1)
        {
            Display[x, y] = '#';
        }
        else
        {
            Display[x, y] = '.';
        }
    }
}