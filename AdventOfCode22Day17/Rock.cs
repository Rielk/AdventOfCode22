namespace AdventOfCode22Day17;
internal abstract class Rock
{
    public abstract Rock NextRock { get; }
    public abstract Location[] Offsets { get; }

    public Location Position { get; set; } = new(0, 0); //Of Bottom left corner

    internal bool Move(List<Solid[]> space, Direction direction)
    {
        Location startingPos = Position;
        Position = direction switch
        {
            Direction.Left => new(Position.x - 1, Position.y),
            Direction.Right => new(Position.x + 1, Position.y),
            Direction.Down => new(Position.x, Position.y - 1),
            _ => throw new NotImplementedException(),
        };
        if (CheckOverlap(space))
        {
            Position = startingPos;
            return false;
        }
        return true;
    }

    private bool CheckOverlap(List<Solid[]> space)
    {
        foreach (Location offset in Offsets)
        {
            int x = Position.x + offset.x;
            if (x is >= Cavern.Width or < 0) return true;
            if (space[Position.y + offset.y][x] != Solid.Empty)
                return true;
        }
        return false;
    }

    public int CommitToSpace(List<Solid[]> space)
    {
        int highestPoint = 0;
        foreach (Location offset in Offsets)
        {
            int y = Position.y + offset.y;
            space[y][Position.x + offset.x] = Solid.Rock;
            if (y > highestPoint)
                highestPoint = y;
        }
        return highestPoint;
    }
}

internal class HorizontalRock : Rock
{
    private static readonly PlusRock nextRock = new();
    public override Rock NextRock => nextRock;

    private static readonly Location[] offsets = new Location[] { new(0, 0), new(1, 0), new(2, 0), new(3, 0) };
    public override Location[] Offsets => offsets;
}

internal class PlusRock : Rock
{
    private static readonly LRock nextRock = new();
    public override Rock NextRock => nextRock;

    private static readonly Location[] offsets = new Location[] { new(1, 0), new(0, 1), new(1, 1), new(2, 1), new(1, 2) };
    public override Location[] Offsets => offsets;
}

internal class LRock : Rock
{
    private static readonly VerticalRock nextRock = new();
    public override Rock NextRock => nextRock;

    private static readonly Location[] offsets = new Location[] { new(0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2) };
    public override Location[] Offsets => offsets;
}

internal class VerticalRock : Rock
{
    private static readonly SquareRock nextRock = new();
    public override Rock NextRock => nextRock;

    private static readonly Location[] offsets = new Location[] { new(0, 0), new(0, 1), new(0, 2), new(0, 3) };
    public override Location[] Offsets => offsets;
}

internal class SquareRock : Rock
{
    private static readonly HorizontalRock nextRock = new();
    public override Rock NextRock => nextRock;

    private static readonly Location[] offsets = new Location[] { new(0, 0), new(1, 0), new(0, 1), new(1, 1) };
    public override Location[] Offsets => offsets;
}
