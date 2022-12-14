using AdventOfCode22Day14.Properties;

string input = Resources.Input1;

Dictionary<Location, Solid> Filled = new();
int AbyssLevel = -1;

foreach (string line in input.Split(Environment.NewLine))
{
    List<Location> corners = new();
    foreach (string cornerString in line.Split("->"))
    {
        int[] coords = cornerString.Trim().Split(",").Select(s => int.Parse(s)).ToArray();
        corners.Add(new(coords[0], coords[1]));
    }

    foreach ((Location First, Location Second) in corners.Zip(corners.Skip(1)))
    {
        if (First.x == Second.x)
        {
            int x = First.x;
            MaxMin(First.y, Second.y, out int bottom, out int top);
            for (int j = top; j <= bottom; j++)
            {
                Location loc = new(x, j);
                Filled.TryAdd(loc, Solid.Rock);
            }
            if (bottom > AbyssLevel) AbyssLevel = bottom;
        }
        else if (First.y == Second.y)
        {
            int y = First.y;
            MaxMin(First.x, Second.x, out int right, out int left);
            for (int i = left; i <= right; i++)
            {
                Location loc = new(i, y);
                Filled.TryAdd(loc, Solid.Rock);
            }
        }
        else
            throw new NotImplementedException();
    }
}
void MaxMin(int a, int b, out int max, out int min) => (max, min) = a > b ? (a, b) : (b, a);

RunSand(out int SandAtRest);
ClearSand();
RunSand(out int SandToFill, AbyssLevel + 2);


Console.WriteLine($"Units of sand at rest: {SandAtRest}");
Console.WriteLine();
Console.WriteLine($"Units of sand to fill cavern: {SandToFill}");

void RunSand(out int sandAtRest, int? floorLevel = null)
{
    sandAtRest = 0;
    Location start = new(500, 0);
    int x = start.x;
    int y = start.y;
    while ((floorLevel == null && y < AbyssLevel) || (floorLevel != null && !Filled.ContainsKey(start)))
    {
        if (floorLevel != null && y + 1 == floorLevel)
        { }
        else if (!Filled.ContainsKey(new(x, y + 1)))
        {
            y++;
            continue;
        }
        else if (!Filled.ContainsKey(new(x - 1, y + 1)))
        {
            y++;
            x -= 1;
            continue;
        }
        else if (!Filled.ContainsKey(new(x + 1, y + 1)))
        {
            y++;
            x += 1;
            continue;
        }
        Filled.Add(new(x, y), Solid.Sand);
        sandAtRest++;
        x = start.x; y = start.y;
    }
}

void ClearSand()
{
    foreach (KeyValuePair<Location, Solid> pair in Filled.ToArray())
    {
        if (pair.Value == Solid.Sand)
            Filled.Remove(pair.Key);
    }
}

public record Location(int x, int y) { }
public enum Solid { Rock, Sand }
