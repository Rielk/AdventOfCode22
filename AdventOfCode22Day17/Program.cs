using AdventOfCode22Day17;
using AdventOfCode22Day17.Properties;

string input = Resources.Input1;

var Directions = new Direction[input.Length];
foreach ((char x, int i) in input.Select((x, i) => (x, i)))
{
    Directions[i] = x switch
    {
        '>' => Direction.Right,
        '<' => Direction.Left,
        _ => throw new NotImplementedException()
    };
}

Cavern Cavern = new(Directions);

Cavern.AddRocks(2022);

Console.WriteLine($"Highest Rock after 2022: {Cavern.HighestRock}");
Console.WriteLine();

int StartHeight = Cavern.HighestRock;
Cavern.AddUntilLoop(out int LoopLength, out int LoopHeight);

long RocksNeeded = 1000000000000 - 2022 - LoopLength;
long AdditionalHeight = (RocksNeeded / LoopLength) * LoopHeight;
RocksNeeded %= LoopLength;
Cavern.AddRocks((int)RocksNeeded);
long FinalHeight = Cavern.HighestRock + AdditionalHeight;
Console.WriteLine($"Highest Rock after 1000000000000: {FinalHeight}");
