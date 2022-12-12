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

int CountToEnd = Map.Start.DistanceFromEnd;
Console.WriteLine($"Shortest path takes: {CountToEnd} steps");

Console.WriteLine();

//foreach (Location[] row in Map.LocationGrid)
//{
//    char[] chars = row.Select(l => (char)(l.Height + 97)).ToArray();
//    Console.WriteLine(new string(chars));
//}
