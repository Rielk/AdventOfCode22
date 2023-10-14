namespace AdventOfCode22Day16;
internal class DoubleNetwork : BaseNetwork
{
	private List<DoubleRoute> Permutations { get; } = new();

	private Dictionary<int, Network.Route> PreviousExcludes { get; } = new();
	private Dictionary<Valve, int> ValveKeyTable { get; } = new();

	public DoubleNetwork(string input, int timeLimit) : base(input, timeLimit)
	{
		int i = 0;
		foreach (Valve valve in Valves)
			ValveKeyTable.Add(valve, (int)Math.Pow(2, i++));

		FillPermutations(new List<Valve>() { StartNode }, 0);
	}

	private void FillPermutations(IEnumerable<Valve> initialStack, int timeUsed)
	{
		Valve currValve = initialStack.Last();
		foreach (Valve nextValve in Valves.Where(v => !initialStack.Contains(v)))
		{
			int timeForStep = currValve.DistanceTo(nextValve) + 1;
			if (timeUsed + timeForStep < TimeLimit)
				FillPermutations(initialStack.Append(nextValve), timeUsed + timeForStep);
		}

		Network.Route altBest = GetBestSingleRoute(initialStack);

		Permutations.Add(new(initialStack, altBest.Stack, CalculateProduction(initialStack, TimeLimit) + altBest.Value));
	}

	private Network.Route GetBestSingleRoute(IEnumerable<Valve> excludeValves)
	{
		int key = ValvesToKey(excludeValves);
		if (!PreviousExcludes.TryGetValue(key, out Network.Route? best))
		{
			Network altNetwork = new(TimeLimit, StartNode, Valves.Where(v => !excludeValves.Contains(v)).ToArray());
			best = altNetwork.FindBestRoute();
			PreviousExcludes.Add(key, best);
		}
		return best;

		int ValvesToKey(IEnumerable<Valve> valves)
		{
			int total = 0;
			foreach (Valve valve in valves)
				if (valve != StartNode)
					total += ValveKeyTable[valve];
			return total;
		}
	}

	public int FindBestRoute(out IEnumerable<Valve> route1, out IEnumerable<Valve> route2)
	{
		DoubleRoute best = Permutations.Aggregate((x, y) => x.Value > y.Value ? x : y);
		route1 = best.Stack1;
		route2 = best.Stack2;
		return best.Value;
	}

	internal record DoubleRoute(IEnumerable<Valve> Stack1, IEnumerable<Valve> Stack2, int Value);
}
