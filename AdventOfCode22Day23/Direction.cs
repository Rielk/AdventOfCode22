namespace AdventOfCode22Day23;
internal enum Direction
{
    North, South, West, East,
    NorthWest, NorthEast, SouthWest, SouthEast,
    None
}

internal static class Directions
{
    private static readonly Direction[] Order = new Direction[] { Direction.North, Direction.South, Direction.West, Direction.East, Direction.North, Direction.South, Direction.West };
    public static IEnumerable<Direction> IterateOrdinal(this Direction firstDirection)
    {
        int first = firstDirection switch
        {
            Direction.North => 0,
            Direction.South => 1,
            Direction.West => 2,
            Direction.East => 3,
            _ => throw new NotImplementedException(),
        };
        foreach (int i in Enumerable.Range(first, 4))
            yield return Order[i];
    }

    public static Direction NextOrdinal(this Direction firstDirection)
    {
        int next = firstDirection switch
        {
            Direction.North => 1,
            Direction.South => 2,
            Direction.West => 3,
            Direction.East => 4,
            _ => throw new NotImplementedException(),
        };
        return Order[next];
    }

    public static IEnumerable<Direction> AllDirections()
    {
        yield return Direction.North;
        yield return Direction.NorthWest;
        yield return Direction.West;
        yield return Direction.SouthWest;
        yield return Direction.South;
        yield return Direction.SouthEast;
        yield return Direction.East;
        yield return Direction.NorthEast;
    }

    public static IEnumerable<Direction> GetAdjacent(this Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                yield return Direction.NorthWest;
                yield return Direction.North;
                yield return Direction.NorthEast;
                break;
            case Direction.South:
                yield return Direction.SouthWest;
                yield return Direction.South;
                yield return Direction.SouthEast;
                break;
            case Direction.West:
                yield return Direction.NorthWest;
                yield return Direction.West;
                yield return Direction.SouthWest;
                break;
            case Direction.East:
                yield return Direction.NorthEast;
                yield return Direction.East;
                yield return Direction.SouthEast;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public static Location GetOffset(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Location.N,
            Direction.South => Location.S,
            Direction.West => Location.W,
            Direction.East => Location.E,
            Direction.NorthWest => Location.NW,
            Direction.NorthEast => Location.NE,
            Direction.SouthWest => Location.SW,
            Direction.SouthEast => Location.SE,
            Direction.None => Location.Here,
            _ => throw new NotImplementedException(),
        };
    }
}
