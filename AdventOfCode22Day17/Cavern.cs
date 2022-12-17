namespace AdventOfCode22Day17;
internal class Cavern
{
    public const int Width = 7;

    public List<Solid[]> Space { get; }
    public Rock CurrentRock { get; private set; } = new HorizontalRock();

    private int currentJet = 0;
    private int CurrentJet
    {
        get => currentJet;
        set => currentJet = value % JetDirections.Length;
    }
    private Direction[] JetDirections { get; }
    public int HighestRock { get; private set; } = 0;

    public Cavern(Direction[] jetDirections)
    {
        var floor = new Solid[Width];
        foreach (int i in Enumerable.Range(0, Width))
            floor[i] = Solid.Floor;
        Space = new() { floor };
        ExpandUpTo(7);
        JetDirections = jetDirections;
    }

    public void ExpandUpTo(int target)
    {
        while (Space.Count <= target)
        {
            var level = new Solid[Width];
            foreach (int i in Enumerable.Range(0, Width))
                level[i] = Solid.Empty;
            Space.Add(level);
        }
    }

    public void AddRocks(int i)
    {
        foreach (int _ in Enumerable.Range(0, i))
            AddRock();
    }
    public void AddRock()
    {
        CurrentRock.Position = new(2, HighestRock + 4);
        while (true)
        {
            _ = CurrentRock.Move(Space, JetDirections[CurrentJet++]);
            if (!CurrentRock.Move(Space, Direction.Down))
                break;
        }
        int highestPoint = CurrentRock.CommitToSpace(Space);
        ExpandUpTo(highestPoint + 7);
        if (highestPoint > HighestRock)
            HighestRock = highestPoint;
        CurrentRock = CurrentRock.NextRock;
    }
}
