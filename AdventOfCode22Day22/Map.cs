namespace AdventOfCode22Day22;
internal abstract class Map
{
    public int Height { get; }
    public int Width { get; }

    public Square[,] Squares { get; }
    public Square CurrentSquare { get; protected set; }

    public Location Location => CurrentSquare.FullLocation;
    public Direction LookingDirection => CurrentSquare.LookingDirection;

    public Map(string input, int size)
    {
        int height = input.Split(Environment.NewLine).Length;
        int width = input.Split(Environment.NewLine).Select(x => x.Length).Max();

        var Tiles = new Tile[width, height];
        foreach ((string line, int yIndex) in input.Split(Environment.NewLine).Select((x, i) => (x, i)))
            foreach ((char c, int xIndex) in line.Select((x, i) => (x, i)))
            {
                Tiles[xIndex, yIndex] = c switch
                {
                    ' ' => Tile.None,
                    '.' => Tile.Empty,
                    '#' => Tile.Wall,
                    _ => throw new NotImplementedException()
                };
            }

        Width = width / size;
        Height = height / size;
        Squares = new Square[Width, Height];
        foreach (int i in Enumerable.Range(0, Width))
            foreach (int j in Enumerable.Range(0, Height))
            {
                int xOff = i * size;
                int yOff = j * size;
                if (Tiles[xOff, yOff] == Tile.None)
                    continue;
                var squareArray = new Tile[size, size];
                foreach (int x in Enumerable.Range(0, size))
                    foreach (int y in Enumerable.Range(0, size))
                        squareArray[x, y] = Tiles[x + xOff, y + yOff];
                Squares[i, j] = new(squareArray, new(xOff + 1, yOff + 1));
            }

        Square? first = null;
        foreach (int i in Enumerable.Range(0, Width))
            if (Squares[i, 0] != null)
            {
                first = Squares[i, 0];
                break;
            }
        if (first == null) throw new NotImplementedException();
        CurrentSquare = first;

        InitSquares();
    }

    internal abstract void InitSquares();

    internal abstract void Move(int count);
    internal abstract void Turn(bool clockwise);
}
