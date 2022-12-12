using AdventOfCode22Day12;
using AdventOfCode22Day12.Properties;

string input = Resources.Input1;

List<List<int>> Heights = new();
foreach (string line in input.Split(Environment.NewLine))
{
    List<int> rowHeights = new();
    foreach (char c in line)
    {
        int h;
        if (c == 'S')
            h = int.MinValue;
        else if (c == 'E')
            h = int.MaxValue;
        else
            h = c - 97;
        rowHeights.Add(h);
    }
    Heights.Add(rowHeights);
}

Map Map = new(Heights);

Map.RunDijkstra();

int CountFromStart = Map.Start.DistanceFromEnd;
Console.WriteLine($"Shortest path from start takes: {CountFromStart} steps");

Console.WriteLine();

int SmallestCount = int.MaxValue;
foreach (Location? potentialStart in Map.LocationGrid.SelectMany(x => x).Where(l => l.Height == 0))
{
    Map.RunDijkstra(potentialStart);
    if (potentialStart.DistanceFromEnd < SmallestCount)
        SmallestCount = potentialStart.DistanceFromEnd;
}
Console.WriteLine($"Shortest path from any a takes: {SmallestCount} steps");
