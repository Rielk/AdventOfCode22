using AdventOfCode22Day9;
using AdventOfCode22Day9.Properties;

string input = Resources.Input1;

Head Head = new();
Tail Tail = Head.Tail;

foreach (string item in input.Split(Environment.NewLine))
{
    var Direction = item[0].ToDirection();
    int n = int.Parse(item.Skip(2).ToArray());
    Console.WriteLine($"{Direction}, {n}");
    Head.Move(Direction, n);
}

int UniquePositions = Tail.PositionHistory.Distinct().Count();

Console.WriteLine($"Unique Positions: {UniquePositions}");
Console.WriteLine();