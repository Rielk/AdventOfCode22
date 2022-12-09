namespace AdventOfCode22Day9;
internal enum Direction
{
    Up, Down, Left, Right
}

internal static class ToDirectionExtensions
{
    internal static Direction ToDirection(this char input)
    {
        return input switch
        {
            'U' => Direction.Up,
            'D' => Direction.Down,
            'L' => Direction.Left,
            'R' => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(input), $"Must be \'U\', 'D', 'D' or 'R', not {input}"),
        };
    }
}
