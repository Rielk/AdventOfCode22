using AdventOfCode22Day20;
using AdventOfCode22Day20.Properties;

string input = Resources.Input1;

CircularList<IntClass> Sequence = new();
foreach (string line in input.Split(Environment.NewLine))
{
    int value = int.Parse(line);
    Sequence.Add(new(value));
}

CircularList<IntClass> TestSequence = new()
{
    new(3),
    new(3)
};
foreach (IntClass? item in TestSequence.ToArray())
{
    int index = TestSequence.IndexOf(item);
}

foreach (IntClass? item in Sequence.ToArray())
{
    int index = Sequence.IndexOf(item);
    Sequence.MoveBy(index, item.Value);
}


int IndexOfZero = Sequence.IndexOf(Sequence.Where(x => x.Value == 0).First());
int i1000 = Sequence[1000 + IndexOfZero].Value;
int i2000 = Sequence[2000 + IndexOfZero].Value;
int i3000 = Sequence[3000 + IndexOfZero].Value;
int SumOfCoords = i1000 + i2000 + i3000;

Console.WriteLine($"Sum of Coordinates: {SumOfCoords}");
Console.WriteLine();
