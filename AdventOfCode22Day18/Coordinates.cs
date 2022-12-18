namespace AdventOfCode22Day18;
internal record Coordinates(int x, int y, int z)
{
    internal Coordinates Add(Coordinates other) => new(x + other.x, y + other.y, z + other.z);
}
