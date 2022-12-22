namespace AdventOfCode22Day22;
internal class Map
{
    public int Height { get; }
    public int Width { get; }

    public Tile[,] Tiles { get; }
    public Tile GetTile(Location location) => Tiles[location.x, location.y];

    public Location Location { get; private set; }
    public Direction LookingDirection { get; private set; } = Direction.Right;

    public Map(string input)
    {
        Height = input.Split(Environment.NewLine).Length + 2;
        Width = input.Split(Environment.NewLine).Select(x => x.Length).Max() + 2;
        Tiles = new Tile[Width, Height];
        foreach ((string line, int yIndex) in input.Split(Environment.NewLine).Select((x, i) => (x, i)))
            foreach ((char c, int xIndex) in line.Select((x, i) => (x, i)))
            {
                Tiles[xIndex + 1, yIndex + 1] = c switch
                {
                    ' ' => Tile.None,
                    '.' => Tile.Empty,
                    '#' => Tile.Wall,
                    _ => throw new NotImplementedException()
                };
            }

        int first = -1;
        foreach (int i in Enumerable.Range(0, Width))
            if (Tiles[i, 1] == Tile.Empty)
                first = i;
        Location = new(first, 1);
    }

    public void Turn(bool clockwise)
    {
        LookingDirection = (LookingDirection, clockwise) switch
        {
            (Direction.Right, true) => Direction.Down,
            (Direction.Right, false) => Direction.Up,

            (Direction.Left, true) => Direction.Up,
            (Direction.Left, false) => Direction.Down,

            (Direction.Up, true) => Direction.Right,
            (Direction.Up, false) => Direction.Left,

            (Direction.Down, true) => Direction.Left,
            (Direction.Down, false) => Direction.Right,

            (_, _) => throw new NotImplementedException()
        };
    }

    internal void Move(int count)
    {
        foreach (int _ in Enumerable.Range(0, count))
            if (!MoveOne())
                break;
    }

    private bool MoveOne()
    {
        Location newLocation = Location.Move(LookingDirection);
        switch (GetTile(newLocation))
        {
            case Tile.None:
                Tile tile = WrapLocation(ref newLocation);
                if (tile == Tile.Empty)
                    goto case Tile.Empty;
                else if (tile == Tile.Wall)
                    goto case Tile.Wall;
                else
                    goto case default;
            case Tile.Empty:
                Location = newLocation;
                return true;
            case Tile.Wall:
                return false;
            default:
                throw new NotImplementedException();
        }


        Tile WrapLocation(ref Location location)
        {
            location = LookingDirection switch
            {
                Direction.Right => new(0, location.y),
                Direction.Down => new(location.x, 0),
                Direction.Left => new(Width - 1, location.y),
                Direction.Up => new(location.x, Height - 1),
                _ => throw new NotImplementedException()
            };
            Tile tile = GetTile(location);
            while (tile == Tile.None)
            {
                location = location.Move(LookingDirection);
                tile = GetTile(location);
            }
            return tile;
        }
    }
}
