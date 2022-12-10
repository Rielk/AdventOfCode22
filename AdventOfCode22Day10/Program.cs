using AdventOfCode22Day10.Properties;

string input = Resources.Input1;

int Buffer = 1;
List<int> BufferHistory = new();

foreach (string item in input.Split(Environment.NewLine))
{
    if (item == "noop")
        BufferHistory.Add(Buffer);
    else
    {
        int n = int.Parse(item.Skip(5).ToArray());
        BufferHistory.Add(Buffer);
        BufferHistory.Add(Buffer);
        Buffer += n;
    }
}
BufferHistory.Add(Buffer);

int[] SignalStrength = BufferHistory.Select((x, i) => x * (i + 1)).ToArray();

Console.WriteLine($"20th:  {SignalStrength[19]}");
Console.WriteLine($"60th:  {SignalStrength[59]}");
Console.WriteLine($"100th: {SignalStrength[99]}");
Console.WriteLine($"140th: {SignalStrength[139]}");
Console.WriteLine($"180th: {SignalStrength[179]}");
Console.WriteLine($"220th: {SignalStrength[219]}");
Console.WriteLine($"Sum:   {SignalStrength[19] + SignalStrength[59] + SignalStrength[99] + SignalStrength[139] + SignalStrength[179] + SignalStrength[219]}");
Console.WriteLine();

