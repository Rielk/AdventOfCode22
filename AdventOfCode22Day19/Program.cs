using AdventOfCode22Day19;
using AdventOfCode22Day19.Properties;

string input = Resources.Input1;

List<Manager> Managers = new();
foreach (string line in input.Split(Environment.NewLine))
{
    string[][] costStrings = line.Split(':').Skip(1).First().Split('.').Take(4).Select(s => s.Split("costs").Skip(1).First().Split("and").Select(s => s.Trim()).ToArray()).ToArray();
    int[] oreCost = new int[3], clayCost = new int[3], obsidianCost = new int[3], geodeCost = new int[3];
    int[][] costs = new int[][] { oreCost, clayCost, obsidianCost, geodeCost };
    foreach (int i in Enumerable.Range(0, 4))
    {
        int[] cost = costs[i];
        string[] costString = costStrings[i];
        cost[0] = int.Parse(costString.Where(s => s.Contains("ore")).FirstOrDefault("0").Where(c => char.IsDigit(c)).ToArray());
        cost[1] = int.Parse(costString.Where(s => s.Contains("clay")).FirstOrDefault("0").Where(c => char.IsDigit(c)).ToArray());
        cost[2] = int.Parse(costString.Where(s => s.Contains("obsidian")).FirstOrDefault("0").Where(c => char.IsDigit(c)).ToArray());
    }
    Managers.Add(new(oreCost, clayCost, obsidianCost, geodeCost));
}

int[] MaxGeodes = Managers.Select(m => m.CalculateMaxGeode(24)).ToArray();
int QualityLevelSum = MaxGeodes.Select((x, i) => x * (i + 1)).Sum();

Console.WriteLine($"Sum of Quality Levels: {QualityLevelSum}");
Console.WriteLine();
