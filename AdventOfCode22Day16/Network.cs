namespace AdventOfCode22Day16;
internal class Network : BaseNetwork
{
	private List<Route> Permutations { get; } = new();

	public Network(string input, int timeLimit) : base(input, timeLimit)
	{
		FillPermutations(new List<Valve>() { StartNode }, 0);
	}

	public Network(int timeLimit, Valve startNode, Valve[] valves) : base(timeLimit, startNode, valves)
	{
		FillPermutations(new List<Valve>() { StartNode }, 0);
	}

	private void FillPermutations(IEnumerable<Valve> initialStack, int timeUsed)
	{
		bool deadEnd = true;
		Valve currValve = initialStack.Last();
		foreach (Valve nextValve in Valves.Where(v => !initialStack.Contains(v)))
		{
			int timeForStep = currValve.DistanceTo(nextValve) + 1;
			if (timeUsed + timeForStep < TimeLimit)
			{
				deadEnd = false;
				FillPermutations(initialStack.Append(nextValve), timeUsed + timeForStep);
			}
		}

		if (deadEnd)
			Permutations.Add(new(initialStack, CalculateProduction(initialStack, TimeLimit)));
	}

	public Route FindBestRoute() => Permutations.Aggregate((x, y) => x.Value > y.Value ? x : y);

	public record Route(IEnumerable<Valve> Stack, int Value);
}
