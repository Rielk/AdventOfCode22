using AdventOfCode22Day16;
using AdventOfCode22Day16.Properties;

string input = Resources.InputTest;

Dictionary<string, Valve> Valves = new();

foreach (string line in input.Split(Environment.NewLine))
{
    string name = line.Split(';')[0].Split(' ')[1];
    int flowRate = int.Parse(line.Split(';')[0].Split(' ')[4].Split('=')[1]);
    string[] nextValves = line.Split(';')[1].Split(' ').Skip(5).Select(s => s.Trim(',')).ToArray();
    Valves.Add(name, new(name, flowRate, nextValves));
}
foreach (Valve valve in Valves.Values)
    valve.FindValves(Valves);
foreach (Valve valve in Valves.Values)
    valve.FindShortestDistances();

Console.WriteLine();
