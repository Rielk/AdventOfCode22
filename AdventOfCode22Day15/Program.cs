using AdventOfCode22Day15;
using AdventOfCode22Day15.Properties;

const int CheckRow = 2000000;//10;
const int SearchRange = 4000000;//20;
string input = Resources.Input1;

int minX = int.MaxValue;
int maxX = int.MinValue;

List<Sensor> Sensors = new();
Dictionary<Location, Beacon> Beacons = new();

foreach (string line in input.Split(Environment.NewLine))
{
    string[] pairs = line.Split(':');

    string[] sensorLocS = pairs[0].Split(',');
    string[] beaconLocS = pairs[1].Split(',');
    Location sensorLoc = new(int.Parse(sensorLocS[0].Split('=')[1]),
                             int.Parse(sensorLocS[1].Split('=')[1]));

    Location beaconLoc = new(int.Parse(beaconLocS[0].Split('=')[1]),
                             int.Parse(beaconLocS[1].Split('=')[1]));
    if (!Beacons.TryGetValue(beaconLoc, out Beacon? beacon))
        Beacons.Add(beaconLoc, beacon = new(beaconLoc));
    Sensor sensor = new(sensorLoc, beacon);
    Sensors.Add(sensor);

    int distance = sensorLoc.DistanceTo(beaconLoc);
    if (sensorLoc.x - distance < minX) minX = sensorLoc.x - distance;
    if (sensorLoc.x + distance > maxX) maxX = sensorLoc.x + distance;
}

int NotBeacon = Sensors.SelectMany(s => s.PointsAtY(CheckRow).Select(p => p.x)).Distinct().Count();
NotBeacon -= Sensors.Select(s => s.Beacon).Distinct().Where(b => b.Location.y == CheckRow).Count();

Console.WriteLine($"Posiotion not beacon on line {CheckRow}: {NotBeacon}");
Console.WriteLine();

Location BeaconLoc = Sensors.SelectMany(s => s.PointsNextToRegion()).Where(l => l.x is >= 0 and <= SearchRange && l.y is >= 0 and <= SearchRange).Where(l => !InSensorRange(l)).First();

long TuningFrequency = (long)4000000 * BeaconLoc.x + BeaconLoc.y;
Console.WriteLine($"Tuning Frequency: {TuningFrequency}");



bool InSensorRange(Location loc)
{
    foreach (Sensor sensor in Sensors)
        if (sensor.CheckInRange(loc))
            return true;
    return false;
}

