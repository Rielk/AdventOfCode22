namespace AdventOfCode22Day16;
internal class Network : BaseNetwork
{
	public Network(string input, int timeLimit) : base(input, timeLimit)
	{
	}

	public Network(int timeLimit, Valve startNode, Valve[] valves) : base(timeLimit, startNode, valves)
	{
	}

	private IEnumerable<Route> AllPermutations(IEnumerable<Valve> initialStack, int timeUsed)
	{
		bool deadEnd = true;
		Valve currValve = initialStack.Last();
		foreach (Valve nextValve in Valves.Where(v => !initialStack.Contains(v)))
		{
			int timeForStep = currValve.DistanceTo(nextValve) + 1;
			if (timeUsed + timeForStep < TimeLimit)
			{
				deadEnd = false;
				foreach (Route route in AllPermutations(initialStack.Append(nextValve), timeUsed + timeForStep))
					yield return route;
			}
		}

		if (deadEnd)
			yield return new(initialStack, CalculateProduction(initialStack, TimeLimit));
	}

	public Route FindBestRoute() => AllPermutations(new List<Valve>() { StartNode }, 0).Aggregate((x, y) => x.Value > y.Value ? x : y);

	public record Route(IEnumerable<Valve> Stack, int Value);
}
