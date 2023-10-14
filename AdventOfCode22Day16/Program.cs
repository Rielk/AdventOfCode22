using AdventOfCode22Day16;
using AdventOfCode22Day16.Properties;

string input = Resources.Input1;

Network Network30 = new(input, 30);

Valve Start = Network30.StartNode;
List<Valve> Stack = new() { Start };
Network.Route Best = Network30.FindBestRoute();
Console.WriteLine($"Maximium possible production: {Best.Value}");
Console.Write($"With Route:");
Best.Stack.Select(v => v.Name).ToList().ForEach(s => Console.Write(s + " "));
Console.WriteLine();
Console.WriteLine();

DoubleNetwork Network26 = new(input, 26);

int BestDoubleProduction = Network26.FindBestRoute(out IEnumerable<Valve> BestRoute1, out IEnumerable<Valve> BestRoute2);
Console.WriteLine($"Maximium possible production: {BestDoubleProduction}");
Console.Write($"With Route:");
BestRoute1.Select(v => v.Name).ToList().ForEach(s => Console.Write(s + " "));
Console.WriteLine();
Console.Write($"and:");
BestRoute2.Select(v => v.Name).ToList().ForEach(s => Console.Write(s + " "));
Console.WriteLine();

