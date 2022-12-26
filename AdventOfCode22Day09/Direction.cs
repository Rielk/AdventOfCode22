namespace AdventOfCode22Day09;
internal enum Direction
{
    None,
    Up, Down, Left, Right,
    UL, UR, DL, DR
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
