namespace AdventOfCode22Day16;
internal class BaseNetwork
{
	public int TimeLimit { get; }
	public Valve StartNode { get; }
	public Valve[] Valves { get; }

	public BaseNetwork(string input, int timeLimit)
	{
		TimeLimit = timeLimit;
		Dictionary<string, Valve> NodeDict = new();
		foreach (string line in input.Split(Environment.NewLine))
		{
			string name = line.Split(';')[0].Split(' ')[1];
			int flowRate = int.Parse(line.Split(';')[0].Split(' ')[4].Split('=')[1]);
			string[] nextValves = line.Split(';')[1].Split(' ').Skip(5).Select(s => s.Trim(',')).ToArray();
			NodeDict.Add(name, new(name, flowRate, nextValves));
		}

		foreach (Valve valve in NodeDict.Values)
			valve.FindValves(NodeDict);
		foreach (Valve valve in NodeDict.Values)
			valve.FindShortestDistances();

		StartNode = NodeDict["AA"];

		Valves = NodeDict.Values.Where(v => v.FlowRate > 0).ToArray();
	}

	protected BaseNetwork(int timeLimit, Valve startNode, Valve[] valves)
	{
		TimeLimit = timeLimit;
		StartNode = startNode;
		Valves = valves;
	}

	protected static int CalculateProduction(IEnumerable<Valve> stack, int totalTime)
	{
		int produced = 0;
		int production = 0;
		int timeRemaining = totalTime;
		Valve current = stack.First();
		foreach (Valve valve in stack.Skip(1))
		{
			int timePassed = current.DistanceTo(valve) + 1;
			timeRemaining -= timePassed;
			produced += production * timePassed;
			production += valve.FlowRate;
			current = valve;
		}
		produced += production * timeRemaining;
		return produced;
	}
}
