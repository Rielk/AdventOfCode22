namespace AdventOfCode22Day22;
internal class FlatMap : Map
{
    public FlatMap(string input, int size) : base(input, size)
    {
    }

    internal override void InitSquares()
    {
        foreach (int i in Enumerable.Range(0, Width))
            foreach (int j in Enumerable.Range(0, Height))
            {
                Square? square = Squares[i, j];
                if (square == null) continue;
                Square up = Squares[i, j - 1 >= 0 ? j - 1 : Height - 1] ?? ArrayColumn(Squares, i).Reverse().First(x => x != null);
                Square down = Squares[i, j + 1 < Height ? j + 1 : 0] ?? ArrayColumn(Squares, i).First(x => x != null);
                Square left = Squares[i - 1 >= 0 ? i - 1 : Width - 1, j] ?? ArrayRow(Squares, j).Reverse().First(x => x != null);
                Square right = Squares[i + 1 < Width ? i + 1 : 0, j] ?? ArrayRow(Squares, j).First(x => x != null);
                square.InitSquare(up, down, left, right, Axis3D.UpDown, Axis3D.LeftRight);
            }

        IEnumerable<Square> ArrayRow(Square[,] array, int row)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                yield return array[i, row];
        }
        IEnumerable<Square> ArrayColumn(Square[,] array, int column)
        {
            for (int i = 0; i < array.GetLength(1); i++)
                yield return array[column, i];
        }
    }

    internal override void Move(int count)
    {
        CurrentSquare.Move2D(count, out Square? square);
        CurrentSquare = square;
    }

    internal override void Turn(bool clockwise) => CurrentSquare.Turn(clockwise);
}
