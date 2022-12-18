using AdventOfCode22Day18;
using AdventOfCode22Day18.Properties;

string input = Resources.Input1;

List<Coordinates> DropletRocks = new();
foreach (string line in input.Split(Environment.NewLine))
{
    int[] coords = line.Split(',').Select(s => int.Parse(s)).ToArray();
    DropletRocks.Add(new(coords[0], coords[1], coords[2]));
}

int MaxX = DropletRocks.Select(c => c.x).Max() + 1;
int MaxY = DropletRocks.Select(c => c.y).Max() + 1;
int MaxZ = DropletRocks.Select(c => c.z).Max() + 1;
var Droplet = new State[MaxX, MaxY, MaxZ];
foreach (int i in Enumerable.Range(0, MaxX))
    foreach (int j in Enumerable.Range(0, MaxY))
        foreach (int k in Enumerable.Range(0, MaxZ))
            Droplet[i, j, k] = DropletRocks.Contains(new(i, j, k)) switch
            {
                true => State.Rock,
                false => State.Air,
            };


var CheckOffsets = new Coordinates[]
{
    new(1, 0, 0),
    new(0, 1, 0),
    new(0, 0, 1),
    new(-1, 0, 0),
    new(0, -1, 0),
    new(0, 0, -1)
};

int TotalSurfaceArea = 0;
foreach (Coordinates location in DropletRocks)
{
    int surfaceArea = 6;
    foreach (Coordinates offset in CheckOffsets)
    {
        Coordinates l = location.Add(offset);
        if (l.x < 0 || l.x >= MaxX) continue;
        if (l.y < 0 || l.y >= MaxY) continue;
        if (l.z < 0 || l.z >= MaxZ) continue;
        if (Droplet[l.x, l.y, l.z] == State.Rock)
            surfaceArea--;
    }
    TotalSurfaceArea += surfaceArea;
}

Console.WriteLine($"Total Surface Area: {TotalSurfaceArea}");
Console.WriteLine();
