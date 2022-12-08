using AdventOfCode22Day1.Properties;

string input = Resources.Input_D1;

List<int> Totals = new();
int curTotal = 0;
foreach (string item in input.Split(Environment.NewLine))
{
    if (string.IsNullOrEmpty(item))
    {
        Totals.Add(curTotal);
        curTotal = 0;
        continue;
    }
    curTotal += int.Parse(item);
}

IOrderedEnumerable<int> sortedTotals = Totals.OrderByDescending(x => x);
Console.WriteLine($"Highest: {sortedTotals.First()}");
Console.WriteLine();
Console.WriteLine($"Top 3: {sortedTotals.Take(3).Sum()}");

