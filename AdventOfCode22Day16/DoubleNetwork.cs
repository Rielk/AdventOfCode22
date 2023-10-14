namespace AdventOfCode22Day16;
internal class DoubleNetwork : BaseNetwork
{
	private Dictionary<Valve, int> ValveKeyTable { get; } = new();

	public DoubleNetwork(string input, int timeLimit) : base(input, timeLimit)
	{
		int i = 0;
		foreach (Valve valve in Valves)
			ValveKeyTable.Add(valve, (int)Math.Pow(2, i++));
	}

	private IEnumerable<DoubleRoute> AllPermutations(IEnumerable<Valve> initialStack, int timeUsed, Dictionary<int, Network.Route>? previousExcludes = null)
	{
		previousExcludes ??= new();

		Valve currValve = initialStack.Last();
		foreach (Valve nextValve in Valves.Where(v => !initialStack.Contains(v)))
		{
			int timeForStep = currValve.DistanceTo(nextValve) + 1;
			if (timeUsed + timeForStep < TimeLimit)
				foreach (DoubleRoute route in AllPermutations(initialStack.Append(nextValve), timeUsed + timeForStep, previousExcludes))
					yield return route;
		}

		Network.Route altBest = GetBestSingleRoute(initialStack, previousExcludes);

		yield return new(initialStack, altBest.Stack, CalculateProduction(initialStack, TimeLimit) + altBest.Value);

		Network.Route GetBestSingleRoute(IEnumerable<Valve> excludeValves, Dictionary<int, Network.Route> previousExcludes)
		{
			int key = ValvesToKey(excludeValves);
			if (!previousExcludes.TryGetValue(key, out Network.Route? best))
			{
				Network altNetwork = new(TimeLimit, StartNode, Valves.Where(v => !excludeValves.Contains(v)).ToArray());
				best = altNetwork.FindBestRoute();
				previousExcludes.Add(key, best);
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
	}
	public int FindBestRoute(out IEnumerable<Valve> route1, out IEnumerable<Valve> route2)
	{
		DoubleRoute best = AllPermutations(new List<Valve>() { StartNode }, 0).Aggregate((x, y) => x.Value > y.Value ? x : y);
		route1 = best.Stack1;
		route2 = best.Stack2;
		return best.Value;
	}

	internal record DoubleRoute(IEnumerable<Valve> Stack1, IEnumerable<Valve> Stack2, int Value);
}
