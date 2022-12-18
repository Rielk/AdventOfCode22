using AdventOfCode22Day18;
using AdventOfCode22Day18.Properties;

string input = Resources.Input1;

List<Location> DropletRocks = new();
foreach (string line in input.Split(Environment.NewLine))
{
    int[] coords = line.Split(',').Select(s => int.Parse(s)).ToArray();
    DropletRocks.Add(new(coords[0], coords[1], coords[2]));
}

int MaxX = DropletRocks.Select(l => l.x).Max() + 1;
int MaxY = DropletRocks.Select(l => l.y).Max() + 1;
int MaxZ = DropletRocks.Select(l => l.z).Max() + 1;
var Droplet = new State[MaxX, MaxY, MaxZ];
List<Location> DropletAir = new();
foreach (int i in Enumerable.Range(0, MaxX))
    foreach (int j in Enumerable.Range(0, MaxY))
        foreach (int k in Enumerable.Range(0, MaxZ))
        {
            Location location = new(i, j, k);
            if (DropletRocks.Contains(location))
                Droplet[i, j, k] = State.Rock;
            else
            {
                Droplet[i, j, k] = State.Air;
                DropletAir.Add(location);
            }

        }


var CheckOffsets = new Location[]
{
    new(1, 0, 0),
    new(0, 1, 0),
    new(0, 0, 1),
    new(-1, 0, 0),
    new(0, -1, 0),
    new(0, 0, -1)
};

int TotalSurfaceArea = SearchFor(State.Air);

Console.WriteLine($"Total Surface Area: {TotalSurfaceArea}");
Console.WriteLine();

if (Droplet[0, 0, 0] == State.Rock) throw new Exception("Assumption that this is never a rock in input");
Droplet[0, 0, 0] = State.Water;
bool Changing = true;
while (Changing)
{
    Changing = false;
    foreach (Location location in DropletAir.ToArray())
    {
        foreach (Location offset in CheckOffsets)
        {
            Location l = location.Add(offset);
            if (l.x < 0 || l.x >= MaxX) continue;
            if (l.y < 0 || l.y >= MaxY) continue;
            if (l.z < 0 || l.z >= MaxZ) continue;
            if (Droplet[l.x, l.y, l.z] == State.Water)
            {
                Droplet[location.x, location.y, location.z] = State.Water;
                DropletAir.Remove(location);
                Changing = true;
            }
        }
    }
}

int TotalSurfaceAreaExcludePockets = SearchFor(State.Water);
Console.WriteLine($"Total Surface Area Excluding Pockets: {TotalSurfaceAreaExcludePockets}");

int SearchFor(State borderState)
{
    int ret = 0;
    foreach (Location location in DropletRocks)
    {
        int surfaceArea = 6;
        foreach (Location offset in CheckOffsets)
        {
            Location l = location.Add(offset);
            if (l.x < 0 || l.x >= MaxX) continue;
            if (l.y < 0 || l.y >= MaxY) continue;
            if (l.z < 0 || l.z >= MaxZ) continue;
            if (Droplet[l.x, l.y, l.z] != borderState)
                surfaceArea--;
        }
        ret += surfaceArea;
    }

    return ret;
}