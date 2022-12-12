namespace AdventOfCode22Day12;
internal class Location
{
    public Location(int height, int positionX, int positionY, Map map, bool isStart = false, bool isEnd = false)
    {
        Height = height;
        PositionX = positionX;
        PositionY = positionY;
        Map = map;
        IsStart = isStart;
        if (isStart)
            Height = 0;
        IsEnd = isEnd;
        if (isEnd)
        {
            Height = 25;
            DistanceFromEnd = 0;
        }
    }

    public int Height { get; }
    public int PositionX { get; }
    public int PositionY { get; }
    public Map Map { get; }
    public bool IsStart { get; }
    public bool IsEnd { get; }

    public bool Visited { get; private set; } = false;
    public int DistanceFromEnd { get; private set; } = int.MaxValue / 2;

    public void UpdateSurroundings()
    {
        if (Visited) throw new Exception();

        int x = PositionX;
        int y = PositionY;

        int nextDistance = DistanceFromEnd + 1;
        if (y - 1 >= 0)
        {
            Location above = Map.LocationGrid[y - 1][x];
            if (!above.Visited && above.Height + 1 >= Height && nextDistance < above.DistanceFromEnd)
                above.DistanceFromEnd = nextDistance;
        }
        if (y + 1 < Map.Height)
        {
            Location below = Map.LocationGrid[y + 1][x];
            if (!below.Visited && below.Height + 1 >= Height && nextDistance < below.DistanceFromEnd)
                below.DistanceFromEnd = DistanceFromEnd + 1;
        }
        if (x - 1 >= 0)
        {
            Location left = Map.LocationGrid[y][x - 1];
            if (!left.Visited && left.Height + 1 >= Height && nextDistance < left.DistanceFromEnd)
                left.DistanceFromEnd = DistanceFromEnd + 1;
        }
        if (x + 1 < Map.Width)
        {
            Location right = Map.LocationGrid[y][x + 1];
            if (!right.Visited && right.Height + 1 >= Height && nextDistance < right.DistanceFromEnd)
                right.DistanceFromEnd = DistanceFromEnd + 1;
        }

        Visited = true;
        _ = Map.UnvisitedLocations.Remove(this);
    }
}
