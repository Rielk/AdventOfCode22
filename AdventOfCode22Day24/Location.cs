namespace AdventOfCode22Day24;
internal record Location(int x, int y)
{
    public Location Offset(int x2, int y2) => new(x + x2, y + y2);

    public IEnumerable<Location> Adjacent()
    {
        yield return this;
        yield return Offset(0, -1);
        yield return Offset(0, 1);
        yield return Offset(-1, 0);
        yield return Offset(1, 0);
    }
}
