namespace AdventOfCode22Day16;
internal class Valve
{
    public string Name { get; }
    public int FlowRate { get; }
    private string[] NextValvesNames { get; }
    public Valve[] NextValves = Array.Empty<Valve>();

    private Dictionary<Valve, int> DistancesDict = new();

    public Valve(string name, int flowRate, string[] nextValves)
    {
        Name = name;
        FlowRate = flowRate;
        NextValvesNames = nextValves;
    }

    public void FindValves(Dictionary<string, Valve> dict)
    {
        List<Valve> res = new();
        foreach (string name in NextValvesNames)
            res.Add(dict[name]);
        NextValves = res.ToArray();

        foreach (Valve valve in dict.Values)
            DistancesDict.Add(valve, -1);
    }

    public void FindShortestDistances()
    {
        Dictionary<Valve, bool> Visited = new();
        foreach (Valve valve in DistancesDict.Keys)
            Visited.Add(valve, false);

        int i = 0;
        List<Valve> currents = new() { this };
        while (currents.Count > 0)
        {
            foreach (Valve current in currents.ToArray())
            {
                _ = currents.Remove(current);
                DistancesDict[current] = i;
                Visited[current] = true;
                foreach (Valve next in current.NextValves)
                    if (!Visited[next])
                        currents.Add(next);
            }
            i++;
        }
    }

    public int DistanceTo(Valve valve)
    {
        int ret = DistancesDict[valve];
        if (ret < 0) throw new Exception();
        return ret;
    }
}
