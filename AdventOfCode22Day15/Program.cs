using AdventOfCode22Day15;
using AdventOfCode22Day15.Properties;

const int CheckRow = 10;
const int SearchRange = 20;
string input = Resources.InputTest;

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

int NotBeacon = 0;
for (int i = minX; i <= maxX; i++)
{
    Location checkLoc = new(i, CheckRow);
    if (InSensorRange(checkLoc))
        NotBeacon++;
}
foreach (Beacon beacon in Sensors.Select(s => s.Beacon).Distinct())
    if (beacon.Location.y == CheckRow)
        NotBeacon--;

Console.WriteLine($"Posiotion not beacon on line {CheckRow}: {NotBeacon}");
Console.WriteLine();

bool InSensorRange(Location loc)
{
    foreach (Sensor sensor in Sensors)
    {
        if (sensor.CheckInRange(loc))
            return true;
    }
    return false;
}


Console.WriteLine($"{CheckLoc.x}");

