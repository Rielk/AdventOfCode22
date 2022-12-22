using System.Numerics;

namespace AdventOfCode22Day22;
internal class CubeMap : Map
{
    public CubeMap(string input, int size) : base(input, size)
    {
    }

    internal override void InitSquares()
    {
        Square square = GetSquare(out int x, out int y);
        square.SetOrientation(Axis3D.Up, Axis3D.Left);
        List<Square> setSquares = new() { square };
        SetNeighbourOrientation(square, x, y);

        foreach (Square s in setSquares)
        {
            var upVector = s.TopDirection.ToVector3();
            var leftVector = s.LeftDirection.ToVector3();
            Vector3 downVector = -upVector;
            Vector3 rightVector = -leftVector;
            Square up = setSquares.First(s => Vector3.Cross(s.TopDirection.ToVector3(), s.LeftDirection.ToVector3()) == upVector);
            Square left = setSquares.First(s => Vector3.Cross(s.TopDirection.ToVector3(), s.LeftDirection.ToVector3()) == leftVector);
            Square down = setSquares.First(s => Vector3.Cross(s.TopDirection.ToVector3(), s.LeftDirection.ToVector3()) == downVector);
            Square right = setSquares.First(s => Vector3.Cross(s.TopDirection.ToVector3(), s.LeftDirection.ToVector3()) == rightVector);
            s.SetAdjacent(up, down, left, right);
        }

        Square GetSquare(out int x, out int y)
        {
            foreach (int i in Enumerable.Range(0, Width))
                foreach (int j in Enumerable.Range(0, Height))
                {
                    Square square = Squares[i, j];
                    if (square != null) { x = i; y = j; return square; }
                }
            throw new Exception();
        }

        void SetNeighbourOrientation(Square square, int x, int y)
        {
            Axis3D initialTop = square.TopDirection;
            Axis3D initialLeft = square.LeftDirection;

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                (int i, int j) = direction switch
                {
                    Direction.Right => (x + 1, y),
                    Direction.Down => (x, y + 1),
                    Direction.Left => (x - 1, y),
                    Direction.Up => (x, y - 1),
                    _ => throw new NotImplementedException(),
                };
                if (i >= Width || i < 0) continue;
                if (j >= Height || j < 0) continue;
                Square neighbour = Squares[i, j];
                if (neighbour == null) continue;
                if (setSquares.Contains(neighbour)) continue;
                direction.RotateSquare(initialTop, initialLeft, out Axis3D nTop, out Axis3D nLeft);
                neighbour.SetOrientation(nTop, nLeft);
                setSquares.Add(neighbour);
                SetNeighbourOrientation(neighbour, i, j);
            }
        }
    }

    internal override void Move(int count)
    {
        CurrentSquare.Move3D(count, out Square? square);
        CurrentSquare = square;
    }

    internal override void Turn(bool clockwise) => CurrentSquare.Turn(clockwise);
}
