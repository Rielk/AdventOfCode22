namespace AdventOfCode22Day22;
internal class Square
{
    public Tile[,] Tiles { get; }
    public int Size { get; }

    public Tile GetTile(Location location) => Tiles[location.x, location.y];

    public Location Location { get; private set; }
    public Direction LookingDirection { get; private set; } = Direction.Right;

    private Location TopLeft { get; }
    public Location FullLocation => new(Location.x + TopLeft.x, Location.y + TopLeft.y);

    public Square(Tile[,] tiles, Location topLeft)
    {
        Tiles = tiles;
        Size = tiles.GetLength(0);
        TopLeft = topLeft;

        int first = -1;
        foreach (int i in Enumerable.Range(0, Size))
            if (Tiles[i, 0] == Tile.Empty)
            {
                first = i;
                break;
            }
        Location = new(first, 0);
    }

    private Square? squareUp;
    private Square? squareDown;
    private Square? squareLeft;
    private Square? squareRight;
    public Square SquareUp => squareUp ?? throw new Exception();
    public Square SquareDown => squareDown ?? throw new Exception();
    public Square SquareLeft => squareLeft ?? throw new Exception();
    public Square SquareRight => squareRight ?? throw new Exception();


    private Axis3D? upDownAxis2D;
    private Axis3D? leftRightAxis2D;

    public Axis3D UpDownAxis2D => upDownAxis2D ?? throw new Exception();
    public Axis3D LeftRightAxis2D => leftRightAxis2D ?? throw new Exception();

    public void InitSquare(Square up, Square down, Square left, Square right, Axis3D ud, Axis3D lr)
    {
        squareUp = up;
        squareDown = down;
        squareLeft = left;
        squareRight = right;
        upDownAxis2D = ud;
        leftRightAxis2D = lr;
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

    internal void Move2D(int count, out Square square)
    {
        square = this;
        foreach (int _ in Enumerable.Range(0, count))
            if (!square.MoveOne2D(out square))
                break;
    }

    private bool MoveOne2D(out Square square)
    {
        if (!Location.Move(LookingDirection, Size, out Location? newLocation))
        {
            Location saveLocation = Location;
            (Square newSquare, Location currentLocation) = LookingDirection switch
            {
                Direction.Right => (SquareRight, new Location(-1, Location.y)),
                Direction.Down => (SquareDown, new Location(Location.x, -1)),
                Direction.Left => (SquareLeft, new Location(Size, Location.y)),
                Direction.Up => (SquareUp, new Location(Location.x, Size)),
                _ => throw new NotImplementedException(),
            };
            newSquare.Location = currentLocation;
            newSquare.LookingDirection = LookingDirection;
            if (newSquare.MoveOne2D(out newSquare))
            {
                square = newSquare;
                return true;
            }
            else
            {
                square = this;
                Location = saveLocation;
                return false;
            }
        }
        else
            square = this;

        switch (GetTile(newLocation))
        {
            case Tile.None:
                throw new NotImplementedException();
            case Tile.Empty:
                Location = newLocation;
                square = this;
                return true;
            case Tile.Wall:
                square = this;
                return false;
            default:
                throw new NotImplementedException();
        }
    }
}
