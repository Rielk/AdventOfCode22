using AdventOfCode22Day14.Properties;
using AnimatedGif;
using System.Drawing;

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
Location AbyssTopLeft = new(Filled.Keys.Select(k => k.x).Min() - 1, 0);
Location AbyssBottomRight = new(Filled.Keys.Select(k => k.x).Max() + 1, AbyssLevel + 1);

ClearSand();

RunSand(out int SandToFill, AbyssLevel + 2);
Location FloorTopLeft = new(Filled.Keys.Select(k => k.x).Min(), 0);
Location FloorBottomRight = new(Filled.Keys.Select(k => k.x).Max(), AbyssLevel + 2);
ClearSand();

Console.WriteLine($"Units of sand at rest: {SandAtRest}");
Console.WriteLine();
Console.WriteLine($"Units of sand to fill cavern: {SandToFill}");
Console.WriteLine();

Bitmap AbyssBitmap = CreateBitmap(AbyssBottomRight.x - AbyssTopLeft.x + 3, AbyssBottomRight.y, 2, AbyssTopLeft.x - 1, 0);
AnimateSand(AbyssBitmap, "AbyssSand.gif", 2, AbyssTopLeft.x - 1, 0, 1);

Console.WriteLine("Abyss Animation Finished");

ClearSand();

Bitmap FloorBitmap = CreateBitmap(FloorBottomRight.x - FloorTopLeft.x + 3, FloorBottomRight.y, 2, FloorTopLeft.x - 1, 0);
AnimateSand(FloorBitmap, "FloorSand.gif", 2, FloorTopLeft.x - 1, 0, 10, AbyssLevel + 2);
ClearSand();

Console.WriteLine("Floor Animation Finished");

void AnimateSand(Bitmap bitmap, string name, int scale, int xOffset, int yOffset, int stepsPerFrame, int? floorLevel = null)
{
    var sandColor = Solid.Sand.ToColor();
    using AnimatedGifCreator gif = new(name, 20);
    gif.AddFrame(bitmap);
    int i = 0;
    RunSand(out int _, floorLevel, loc =>
    {
        AddToBitmap(bitmap, loc, sandColor, scale, xOffset, yOffset);
        if (++i == stepsPerFrame)
        {
            gif.AddFrame(bitmap);
            i = 0;
        }
    });
    gif.AddFrame(bitmap, 1000);
}

void RunSand(out int sandAtRest, int? floorLevel = null, Action<Location>? afterAddAction = null)
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
        Location newLoc = new(x, y);
        Filled.Add(newLoc, Solid.Sand);
        sandAtRest++;
        x = start.x; y = start.y;
        afterAddAction?.Invoke(newLoc);
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

Bitmap CreateBitmap(int x, int y, int scale, int xOffset, int yOffset)
{
    if (!OperatingSystem.IsWindows()) throw new NotImplementedException();

    Bitmap ret = new(scale * x, scale * y);
    foreach (KeyValuePair<Location, Solid> pair in Filled)
        AddToBitmap(ret, pair.Key, pair.Value.ToColor(), scale, xOffset, yOffset);
    return ret;
}

void AddToBitmap(Bitmap bitmap, Location loc, Color color, int scale, int xOffset, int yOffset)
{
    int x1 = (loc.x - xOffset) * scale;
    int y1 = (loc.y - yOffset) * scale;
    for (int i = 0; i < scale; i++)
        for (int j = 0; j < scale; j++)
            bitmap.SetPixel(x1 + i, y1 + j, color);
}

public record Location(int x, int y) { }
public enum Solid { Rock, Sand }
public static class SolidExt
{
    public static Color ToColor(this Solid solid) => solid switch
    {
        Solid.Rock => Color.SandyBrown,
        Solid.Sand => Color.Yellow,
        _ => Color.Black,
    };
}
