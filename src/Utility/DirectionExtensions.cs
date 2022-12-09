namespace AdventOfCode;

public static class DirectionExtensions
{
    public static Direction TurnLeft(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => throw new ArgumentException("Unexpected value for Direction")
        };
    }

    public static Direction TurnRight(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentException("Unexpected value for Direction")
        };
    }

    public static Direction ToDirection(this string input)
    {
        return input.ToUpper() switch
        {
            "U" => Direction.Up,
            "D" => Direction.Down,
            "L" => Direction.Left,
            "R" => Direction.Right,
            "UP" => Direction.Up,
            "DOWN" => Direction.Down,
            "LEFT" => Direction.Left,
            "RIGHT" => Direction.Right,
            _ => throw new ArgumentException("Unexpected value for Direction")
        };
    }
}