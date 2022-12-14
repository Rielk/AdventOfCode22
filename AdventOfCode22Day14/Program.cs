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
void RunSand(out int sandAtRest)
{
    sandAtRest = 0;
    const int startX = 500;
    const int startY = 0;
    int x = startX;
    int y = startY;
    while (y < AbyssLevel)
    {
        if (!Filled.ContainsKey(new(x, y + 1)))
        {
            y++;
        }
        else if (!Filled.ContainsKey(new(x - 1, y + 1)))
        {
            y++;
            x -= 1;
        }
        else if (!Filled.ContainsKey(new(x + 1, y + 1)))
        {
            y++;
            x += 1;
        }
        else
        {
            Filled.Add(new(x, y), Solid.Sand);
            sandAtRest++;
            x = startX; y = startY;
        }
    }
}
Console.WriteLine($"Units of sand at rest: {SandAtRest}");

public record Location(int x, int y) { }
public enum Solid { Rock, Sand }
