namespace AdventOfCode22Day18;
internal record Location(int x, int y, int z)
{
    internal Location Add(Location other) => new(x + other.x, y + other.y, z + other.z);
}
