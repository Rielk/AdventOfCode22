namespace AdventOfCode22Day12;
internal class Location
{
    public Location(int height, int positionX, int positionY, Map map, bool isStart = false, bool isEnd = false)
    {
        Height = height;
        PositionX = positionX;
        PositionY = positionY;
        Map = map;
        IsStart = isStart;
        if (isStart)
            Height = 0;
        IsEnd = isEnd;
        if (isEnd)
        {
            Height = 26;
            CurrentDirection = CachedDirection = Direction.Target;
        }
    }

    public int Height { get; }
    public int PositionX { get; }
    public int PositionY { get; }
    public Map Map { get; }
    public bool IsStart { get; }
    public bool IsEnd { get; }

    public Direction? CurrentDirection { get; private set; } = null;
    public Direction? CachedDirection { get; set; } = null;
    public bool HasUpdatedSurroundings { get; private set; } = false;

    public void UpdateSurroundings()
    {
        if (HasUpdatedSurroundings) return;
        if (CurrentDirection == null) return;

        int x = PositionX;
        int y = PositionY;

        if (y - 1 >= 0)
        {
            Location above = Map.LocationGrid[y - 1][x];
            if (above.CachedDirection == null && above.Height + 1 >= Height)
                above.CachedDirection = Direction.Do;
        }
        if (y + 1 < Map.Height)
        {
            Location below = Map.LocationGrid[y + 1][x];
            if (below.CachedDirection == null && below.Height + 1 >= Height)
                below.CachedDirection = Direction.Up;
        }
        if (x - 1 >= 0)
        {
            Location left = Map.LocationGrid[y][x - 1];
            if (left.CachedDirection == null && left.Height + 1 >= Height)
                left.CachedDirection = Direction.Ri;
        }
        if (x + 1 < Map.Width)
        {
            Location right = Map.LocationGrid[y][x + 1];
            if (right.CachedDirection == null && right.Height + 1 >= Height)
                right.CachedDirection = Direction.Le;
        }
    }

    public void CommitCache() => CurrentDirection = CachedDirection;

    public bool CountToEnd(out int count)
    {
        if (CurrentDirection == null) { count = -1; return false; }
        if (CurrentDirection == Direction.Target) { count = 0; return true; }
        Location location = CurrentDirection switch
        {
            Direction.Up => Map.LocationGrid[PositionY - 1][PositionX],
            Direction.Do => Map.LocationGrid[PositionY + 1][PositionX],
            Direction.Le => Map.LocationGrid[PositionY][PositionX - 1],
            Direction.Ri => Map.LocationGrid[PositionY][PositionX + 1],
            _ => throw new NotImplementedException(),
        };
        if (!location.CountToEnd(out count))
            return false;
        count++;
        return true;
    }
}

public enum Direction
{
    Up, Do, Le, Ri, Target
}

public static class DirectionExtensions
{
    public static char ToChar(this Direction direction) => ToChar((Direction?)direction);
    public static char ToChar(this Direction? direction)
    {
        return direction switch
        {
            Direction.Up => '^',
            Direction.Do => 'V',
            Direction.Le => '<',
            Direction.Ri => '>',
            Direction.Target => 'E',
            _ => '.',
        };
    }

}
