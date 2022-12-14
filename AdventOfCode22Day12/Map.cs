using AnimatedGif;
using System.Drawing;

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
                    End = LocationGrid[i][j] = new Location('z', j, i, this, isEnd: true);
                else if (h == int.MinValue)
                    Start = LocationGrid[i][j] = new Location('a', j, i, this, isStart: true);
                else
                    LocationGrid[i][j] = new Location(h, j, i, this);
            }
        }
        if (Start == null || End == null) throw new ArgumentException(nameof(locationGrid), "Must contain \'S\' and \'E\' characters");

        UnvisitedLocations = new(LocationGrid.SelectMany(x => x));
    }

    public void AnimateDijkstra(int scale)
    {
        using AnimatedGifCreator gif = new("Dijkstra.gif", 20);
        Bitmap Bitmap = CreateBitmap(scale);
        gif.AddFrame(Bitmap, 200);
        RunDijkstra(Start, (oldLoc, newLoc) =>
        {
            if (oldLoc != null)
                AddToBitmap(Bitmap, oldLoc.PositionX, oldLoc.PositionY, Color.FromArgb(0, 0, 255 - (oldLoc.DistanceFromEnd / 2)), scale);
            AddToBitmap(Bitmap, newLoc.PositionX, newLoc.PositionY, Color.FromArgb(0, 255, 0), scale);
            gif.AddFrame(Bitmap);
        });
        gif.AddFrame(Bitmap, 1000);
    }

    public void RunDijkstra(Location Target, Action<Location?, Location>? afterUpdateAction = null)
    {
        Location? Current = null;
        while (!Target.Visited)
        {
            Location? Previous = Current;

            int min = UnvisitedLocations.Select(y => y.DistanceFromEnd).Min();
            Current = UnvisitedLocations.Where(x => x.DistanceFromEnd == min).First();

            Current.UpdateSurroundings();

            afterUpdateAction?.Invoke(Previous, Current);
        }
    }

    public Bitmap CreateBitmap(int scale)
    {
        Bitmap bm = new(Width * scale, Height * scale);
        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
                AddToBitmap(bm, i, j, Color.FromArgb((LocationGrid[j][i].Height - 'a') * 10, 0, 0), scale);
        return bm;
    }

    private static void AddToBitmap(Bitmap bitmap, int x, int y, Color color, int scale)
    {
        int x1 = x * scale;
        int y1 = y * scale;
        for (int i = 0; i < scale; i++)
            for (int j = 0; j < scale; j++)
                bitmap.SetPixel(x1 + i, y1 + j, color);
    }
}
