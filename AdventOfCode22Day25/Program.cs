using AdventOfCode22Day25;
using AdventOfCode22Day25.Properties;

string input = Resources.Input1;

long Sum = 0;
foreach (string line in input.Split(Environment.NewLine))
    Sum += line.FromSNAFU();

Console.WriteLine($"Sum of requirements in SNAFU: {Sum.ToSNAFU()}");
Console.WriteLine();
