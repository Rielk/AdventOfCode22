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
