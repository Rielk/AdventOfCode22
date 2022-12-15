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
}
