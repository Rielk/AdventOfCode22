namespace AdventOfCode22Day12;
internal class Map
{
    public int Height { get; }
    public int Width { get; }

    public Location[][] LocationGrid;

    public List<Location> UnvisitedLocations { get; }

    public Location Start { get; }
    public Location End { get; }

    public Map(List<List<int>> locationGrid)
    {
        Height = locationGrid.Count;
        Width = locationGrid.Select(e => e.Count).Max();
        LocationGrid = new Location[Height][];
        for (int i = 0; i < Height; i++)
        {
            LocationGrid[i] = new Location[Width];
            for (int j = 0; j < Width; j++)
            {
                int h = locationGrid[i][j];
                if (h == int.MaxValue)
                    End = LocationGrid[i][j] = new Location(h, j, i, this, isEnd: true);
                else if (h == int.MinValue)
                    Start = LocationGrid[i][j] = new Location(h, j, i, this, isStart: true);
                else
                    LocationGrid[i][j] = new Location(h, j, i, this);
            }
        }
        if (Start == null || End == null) throw new ArgumentException(nameof(locationGrid), "Must contain \'S\' and \'E\' characters");

        UnvisitedLocations = new(LocationGrid.SelectMany(x => x));
    }

    public void RunDijkstra(Location? Target = null)
    {
        Target ??= Start;

        while (!Target.Visited)
        {
            int min = UnvisitedLocations.Select(y => y.DistanceFromEnd).Min();
            Location Current = UnvisitedLocations.Where(x => x.DistanceFromEnd == min).First();

            Current.UpdateSurroundings();
        }
    }
}
