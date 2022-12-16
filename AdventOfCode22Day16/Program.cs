using AdventOfCode22Day16;
using AdventOfCode22Day16.Properties;

string input = Resources.Input1;

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

Valve[] ValvesWithFR = Valves.Values.Where(v => v.FlowRate > 0).ToArray();

Valve Start = Valves["AA"];
List<Valve> Stack = new() { Start };
int BestProduction = FindBestRoute(Stack, 30, out List<Valve> _);
Console.WriteLine($"Maximium possible production: {BestProduction}");
Console.WriteLine();

int FindBestRoute(IEnumerable<Valve> initialStack, int totalTime, out List<Valve> bestRoute)
{
    int remainingTime = totalTime - CalculateTimeUsed(initialStack);
    Valve current = initialStack.Last();
    int maxProduction = CalculateProduction(initialStack, totalTime);
    List<Valve>? bestStack = null;
    foreach (Valve valve in ValvesWithFR.Where(v => !initialStack.Contains(v)).Where(v => current.DistanceTo(v) + 1 <= remainingTime))
    {
        IEnumerable<Valve> nextStack = initialStack.Append(valve);
        int nextProduction = FindBestRoute(nextStack, totalTime, out List<Valve> nextRoute);
        if (nextProduction > maxProduction)
        {
            bestStack = nextRoute;
            maxProduction = nextProduction;
        }
    }
    bestRoute = bestStack ?? initialStack.ToList();
    return maxProduction;
}

int CalculateTimeUsed(IEnumerable<Valve> stack)
{
    int time = 0;
    Valve current = stack.First();
    foreach (Valve? valve in stack.Skip(1))
    {
        time += current.DistanceTo(valve) + 1;
        current = valve;
    }
    return time;
}

int CalculateProduction(IEnumerable<Valve> stack, int totalTime)
{
    List<Valve> opened = new();
    int produced = 0;
    int production = 0;
    int timeRemaining = totalTime;
    Valve current = stack.First();
    foreach (Valve valve in stack.Skip(1))
    {
        int timePassed = current.DistanceTo(valve) + 1;
        timeRemaining -= timePassed;
        produced += production * timePassed;
        if (!opened.Contains(valve))
        {
            production += valve.FlowRate;
            opened.Add(valve);
        }
        current = valve;
    }
    produced += production * timeRemaining;
    return produced;
}
