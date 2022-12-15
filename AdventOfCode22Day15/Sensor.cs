namespace AdventOfCode22Day15;
internal class Sensor
{
    public Sensor(Location location, Beacon beacon)
    {
        Location = location;
        Beacon = beacon;
        Radius = Location.DistanceTo(Beacon.Location);
    }

    public Location Location { get; }
    public Beacon Beacon { get; }
    public int Radius { get; }

    public bool CheckInRange(Location loc) => Location.DistanceTo(loc) <= Radius;

    public IEnumerable<Location> PointsNextToRegion()
    {
        foreach (int yOff in Enumerable.Range(-Radius - 1, 2 * Radius + 2))
        {
            int xOff = Radius - Math.Abs(yOff) + 1;
            yield return new(Location.x + xOff, Location.y + yOff);
            if (xOff != 0)
                yield return new(Location.x - xOff, Location.y + yOff);
        }
    }

    public IEnumerable<Location> PointsAtY(int y)
    {
        int yOff = Math.Abs(y - Location.y);
        if (yOff > Radius) yield break;

        int xOff = Radius - yOff;
        int xOffSave = xOff;

        while (xOff >= -xOffSave)
        {
            yield return new(Location.x - xOff, y);
            xOff--;
        }
    }
}
