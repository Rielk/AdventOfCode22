namespace AdventOfCode22Day22;
internal record Location(int x, int y)
{
    public Location Move(Direction direction) => direction switch
    {
        Direction.Right => new(x + 1, y),
        Direction.Down => new(x, y + 1),
        Direction.Left => new(x - 1, y),
        Direction.Up => new(x, y - 1),
        _ => throw new NotImplementedException(),
    };
}
