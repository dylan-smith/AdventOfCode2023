namespace AdventOfCode.Days;

public enum CommandType
{
    cd,
    ls
}

[Day(2022, 7)]
public class Day07 : BaseDay
{
    public override string PartOne(string input)
    {
        var commands = ParseCommands(input);
        var fileSystem = BuildFileSystem(commands);
        CalculateDirSizes(fileSystem);
        
        var dirSizes = GetDirSizes(fileSystem);

        return dirSizes.Where(x => x <= 100000).Sum().ToString();
    }

    private Tree<(bool isFile, int size, string name)> BuildFileSystem(IEnumerable<(CommandType type, string command, IEnumerable<string> output)> commands)
    {
        var fileSystem = new Tree<(bool isFile, int size, string name)>((false, 0, "/"));
        var pwd = fileSystem;

        foreach (var command in commands.Skip(1))
        {
            if (command.type == CommandType.cd)
            {
                pwd = ExecuteCD(command, pwd);

            }
            else if (command.type == CommandType.ls)
            {
                ExecuteLS(command, pwd);
            }
        }

        return fileSystem;
    }

    private void ExecuteLS((CommandType type, string command, IEnumerable<string> output) command, Tree<(bool isFile, int size, string name)> pwd)
    {
        foreach (var item in command.output)
        {
            if (item.StartsWith("dir"))
            {
                var dir = item.Words().Last();

                if (!pwd.Children.Any(x => x.Data.name == dir))
                {
                    var newDir = new Tree<(bool isFile, int size, string name)>((false, 0, dir));
                    newDir.Parent = pwd;
                    _ = pwd.Children.AddLast(newDir);
                }
            }
            else
            {
                var words = item.Words();
                var size = int.Parse(words.First());
                var name = words.Last();

                if (!pwd.Children.Any(x => x.Data.name == name))
                {
                    var newFile = new Tree<(bool isFile, int size, string name)>((true, size, name));
                    newFile.Parent = pwd;
                    _ = pwd.Children.AddLast(newFile);
                }
            }
        }
    }

    private Tree<(bool isFile, int size, string name)> ExecuteCD((CommandType type, string command, IEnumerable<string> output) command, Tree<(bool isFile, int size, string name)> pwd)
    {
        var dir = command.command.Words().Last();

        if (dir == "..")
        {
            return pwd.Parent;
        }
        else
        {
            if (pwd.Children.Any(x => x.Data.name == dir))
            {
                return pwd.Children.First(x => x.Data.name == dir);
            }
            else
            {
                var newDir = new Tree<(bool isFile, int size, string name)>((false, 0, dir));
                newDir.Parent = pwd;
                return pwd.Children.AddLast(newDir).Value;
            }
        }
    }

    private IEnumerable<(CommandType type, string command, IEnumerable<string> output)> ParseCommands(string input)
    {
        var commands = input.Split('$', StringSplitOptions.RemoveEmptyEntries);

        foreach (var command in commands)
        {
            var lines = command.Lines().ToList();
            var cmd = lines.First().Trim();
            var output = lines.Skip(1);

            if (cmd.StartsWith("cd"))
            {
                yield return (CommandType.cd, cmd, output);
            }
            else if (cmd.StartsWith("ls"))
            {
                yield return (CommandType.ls, cmd, output);
            }
        }
    }

    private void CalculateDirSizes(Tree<(bool isFile, int size, string name)> fileSystem)
    {
        foreach (var child in fileSystem.Children)
        {
            CalculateDirSizes(child);
            fileSystem.Data = (fileSystem.Data.isFile, fileSystem.Data.size + child.Data.size, fileSystem.Data.name);
        }
    }

    private IEnumerable<int> GetDirSizes(Tree<(bool isFile, int size, string name)> fileSystem)
    {
        var result = new List<int>();
        
        foreach (var child in fileSystem.Children)
        {
            result.AddRange(GetDirSizes(child));
        }

        if (!fileSystem.Data.isFile)
        {
            result.Add(fileSystem.Data.size);
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        var commands = ParseCommands(input);
        var fileSystem = BuildFileSystem(commands);
        CalculateDirSizes(fileSystem);

        var dirSizes = GetDirSizes(fileSystem);

        var totalDisk = 70000000;
        var freeSpace = totalDisk - fileSystem.Data.size;
        var neededSpace = 30000000 - freeSpace;

        return dirSizes.Where(x => x >= neededSpace).OrderBy(x => x).First().ToString();
    }
}
