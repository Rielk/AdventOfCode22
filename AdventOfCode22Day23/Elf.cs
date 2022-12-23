namespace AdventOfCode22Day23;
internal class Elf
{
    public Direction MoveIn { get; set; } = Direction.None;

    private Dictionary<Location, Elf> ElfLocations { get; }

    public Location Location { get; private set; }

    public Elf(Dictionary<Location, Elf> elfLocations, Location location)
    {
        ElfLocations = elfLocations;
        Location = location;

        ElfLocations.Add(location, this);
    }

    public bool CheckForMove(Direction firstDirection)
    {
        Dictionary<Direction, bool> elfAt = new();
        foreach (Direction direction in Directions.AllDirections())
        {
            Location offset = direction.GetOffset();
            Location checkLocation = Location.Add(offset);
            elfAt.Add(direction, ElfLocations.ContainsKey(checkLocation));
        }
        if (elfAt.Values.All(v => v == false)) return false;

        foreach (Direction moveDirection in Directions.IterateOrdinal(firstDirection))
        {
            if (DirectionClear(moveDirection))
            {
                MoveIn = moveDirection;
                break;
            }
        }

        return true;


        bool DirectionClear(Direction direction)
        {
            foreach (Direction adjacentDir in direction.GetAdjacent())
                if (elfAt[adjacentDir] == true)
                    return false;
            return true;
        }
    }

    public void CommitMove()
    {
        if (MoveIn == Direction.None) return;

        Location moveOffset = MoveIn.GetOffset();
        Location moveTo = Location.Add(moveOffset);
        if (ElfLocations.TryGetValue(moveTo, out Elf? blockingElf))
        {
            Location blockElfMoveTo = blockingElf.Location.Add(moveOffset);
            blockingElf.MoveTo(blockElfMoveTo);
        }
        else
            MoveTo(moveTo);

        MoveIn = Direction.None;
    }

    private void MoveTo(Location newLocation)
    {
        ElfLocations.Remove(Location);
        ElfLocations.Add(newLocation, this);
        Location = newLocation;
    }
}
