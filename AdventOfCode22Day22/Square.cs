using System.Numerics;

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


    private Axis3D? topDirection;
    private Axis3D? leftDirection;

    public Axis3D TopDirection => topDirection ?? throw new Exception();
    public Axis3D LeftDirection => leftDirection ?? throw new Exception();

    public void InitSquare(Square up, Square down, Square left, Square right, Axis3D tDir, Axis3D lDir)
    {
        SetAdjacent(up, down, left, right);
        SetOrientation(tDir, lDir);
    }
    public void SetOrientation(Axis3D tDir, Axis3D lDir)
    {
        if (topDirection != null || leftDirection != null) throw new Exception();
        topDirection = tDir;
        leftDirection = lDir;
    }
    public void SetAdjacent(Square up, Square down, Square left, Square right)
    {
        if (squareUp != null || squareDown != null || squareLeft != null || squareRight != null) throw new Exception();
        squareUp = up;
        squareDown = down;
        squareLeft = left;
        squareRight = right;
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

    internal void Move3D(int count, out Square square)
    {
        square = this;
        foreach (int _ in Enumerable.Range(0, count))
            if (!square.MoveOne3D(out square))
                break;
    }

    private bool MoveOne3D(out Square square)
    {
        if (!Location.Move(LookingDirection, Size, out Location? newLocation))
        {
            Location saveLocation = Location;
            (Square nextSquare, int indent) = LookingDirection switch
            {
                Direction.Right => (SquareRight, Location.y),
                Direction.Down => (SquareDown, Size - Location.x - 1),
                Direction.Left => (SquareLeft, Size - Location.y - 1),
                Direction.Up => (SquareUp, Location.x),
                _ => throw new NotImplementedException(),
            };

            Vector3 targetVector = -Vector3.Cross(TopDirection.ToVector3(), LeftDirection.ToVector3());
            Direction? d = null;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                direction.RotateSquare(nextSquare.TopDirection, nextSquare.LeftDirection, out Axis3D topOut, out Axis3D leftOut);
                if (Vector3.Cross(topOut.ToVector3(), leftOut.ToVector3()) == targetVector)
                {
                    d = direction;
                    break;
                }
            }
            Direction newDirection = d ?? throw new Exception();

            Location currentLocation = newDirection switch
            {
                Direction.Right => new Location(-1, indent),
                Direction.Down => new Location(Size - 1 - indent, -1),
                Direction.Left => new Location(Size, Size - 1 - indent),
                Direction.Up => new Location(indent, Size),
                _ => throw new NotImplementedException(),
            };

            nextSquare.Location = currentLocation;
            nextSquare.LookingDirection = newDirection;
            if (nextSquare.MoveOne3D(out nextSquare))
            {
                square = nextSquare;
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
