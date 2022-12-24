namespace AdventOfCode22Day24;
internal class Map
{
    private List<Blizzard>[,] Blizzards { get; }

    public int Height { get; }
    public int Width { get; }

    public Map(char[,] input)
    {
        Width = input.GetLength(0);
        Height = input.GetLength(1);

        Blizzards = new List<Blizzard>[Width, Height];
        foreach (int i in Enumerable.Range(0, Width))
            foreach (int j in Enumerable.Range(0, Height))
            {
                Blizzards[i, j] = new();
                char c = input[i, j];
                if (c == '.')
                    continue;
                Direction direction = c switch
                {
                    '<' => Direction.Left,
                    '>' => Direction.Right,
                    'v' => Direction.Down,
                    '^' => Direction.Up,
                    _ => throw new NotImplementedException(),
                };
                Blizzard bliz = new(direction);
                Blizzards[i, j].Add(bliz);
            }
    }

    public bool BlizzardAtLocation(Location location) => Blizzards[location.x, location.y].Any();

    public void Step()
    {
        List<Blizzard> movedBlizzards = new();

        foreach (int i in Enumerable.Range(0, Width))
            foreach (int j in Enumerable.Range(0, Height))
            {
                foreach (Blizzard bliz in Blizzards[i, j].ToArray())
                {
                    if (movedBlizzards.Contains(bliz)) continue;
                    bliz.Step(Blizzards, new(i, j));
                    movedBlizzards.Add(bliz);
                }
            }
    }
}
