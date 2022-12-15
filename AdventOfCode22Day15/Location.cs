namespace AdventOfCode22Day15;
public record Location(int x, int y)
{
    public int DistanceTo(Location other) => Math.Abs(x - other.x) + Math.Abs(y - other.y);
}
