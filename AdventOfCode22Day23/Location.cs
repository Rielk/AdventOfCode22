namespace AdventOfCode22Day23;
internal record Location(int x, int y)
{
    public static readonly Location N = new(0, -1);
    public static readonly Location NW = new(-1, -1);
    public static readonly Location W = new(-1, 0);
    public static readonly Location SW = new(-1, 1);
    public static readonly Location S = new(0, 1);
    public static readonly Location SE = new(1, 1);
    public static readonly Location E = new(1, 0);
    public static readonly Location NE = new(1, -1);
    public static readonly Location Here = new(0, 0);

    public Location Add(Location other) => new(x + other.x, y + other.y);
}
