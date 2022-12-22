namespace AdventOfCode22Day22;
internal enum Direction
{
    Right, Down, Left, Up
}

internal static class DirectionExtensions
{
    public static int ToScore(this Direction direction) => direction switch
    {
        Direction.Right => 0,
        Direction.Down => 1,
        Direction.Left => 2,
        Direction.Up => 3,
        _ => throw new NotImplementedException(),
    };
}
