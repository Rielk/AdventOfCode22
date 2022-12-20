using AdventOfCode22Day20;
using AdventOfCode22Day20.Properties;

string input = Resources.Input1;

CircularList<IntClass> Sequence1 = new();
CircularList<LongClass> Sequence2 = new();
foreach (string line in input.Split(Environment.NewLine))
{
    int value = int.Parse(line);
    Sequence1.Add(new(value));
    Sequence2.Add(new(value * (long)811589153));
}

foreach (IntClass item in Sequence1.ToArray())
{
    int index = Sequence1.IndexOf(item);
    Sequence1.MoveBy(index, item.Value);
}


int IndexOfZero1 = Sequence1.IndexOf(Sequence1.Where(x => x.Value == 0).First());
int i11000 = Sequence1[1000 + IndexOfZero1].Value;
int i12000 = Sequence1[2000 + IndexOfZero1].Value;
int i13000 = Sequence1[3000 + IndexOfZero1].Value;
int SumOfCoords1 = i11000 + i12000 + i13000;

Console.WriteLine($"Sum of Coordinates: {SumOfCoords1}");
Console.WriteLine();

LongClass[] OriginalSequence2 = Sequence2.ToArray();
foreach (int _ in Enumerable.Range(0, 10))
    foreach (LongClass item in OriginalSequence2)
    {
        int index = Sequence2.IndexOf(item);
        Sequence2.MoveBy(index, item.Value);
    }
int IndexOfZero2 = Sequence2.IndexOf(Sequence2.Where(x => x.Value == 0).First());
long i21000 = Sequence2[1000 + IndexOfZero2].Value;
long i22000 = Sequence2[2000 + IndexOfZero2].Value;
long i23000 = Sequence2[3000 + IndexOfZero2].Value;
long SumOfCoords2 = i21000 + i22000 + i23000;

Console.WriteLine($"Sum of Coordinates after decryption: {SumOfCoords2}");