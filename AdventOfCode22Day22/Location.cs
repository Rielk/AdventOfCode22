using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode22Day22;
internal record Location(int x, int y)
{
    public bool Move(Direction direction, int max, [NotNullWhen(true)] out Location? newLocation)
    {
        Location location = direction switch
        {
            Direction.Right => new(x + 1, y),
            Direction.Down => new(x, y + 1),
            Direction.Left => new(x - 1, y),
            Direction.Up => new(x, y - 1),
            _ => throw new NotImplementedException(),
        };
        if (location.x >= max || location.x < 0) { newLocation = null; return false; }
        if (location.y >= max || location.y < 0) { newLocation = null; return false; }
        newLocation = location;
        return true;
    }
}
