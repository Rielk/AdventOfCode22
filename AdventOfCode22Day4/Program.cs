using AdventOfCode22Day4.Properties;

string input = Resources.Input1;

int containedCount = 0;
int overlapCount = 0;

foreach (string items in input.Split(Environment.NewLine))
{
    string[] pair = items.Split(',');
    Range range1 = new(pair[0]);
    Range range2 = new(pair[1]);

    if (range1.FullyOverlaps(range2))
        containedCount++;
    if (range1.PartialOverlaps(range2))
        overlapCount++;
}

Console.WriteLine($"Sum of pairs with contained ranges: {containedCount}");
Console.WriteLine();
Console.WriteLine($"Sum of pairs with overlapping ranges: {overlapCount}");

internal class Range
{
    public int Start { get; }
    public int End { get; }

    public Range(string input)
    {
        string[] chars = input.Split('-');
        Start = int.Parse(chars[0]);
        End = int.Parse(chars[1]);
        if (Start > End)
            (Start, End) = (End, Start);
    }

    public bool FullyOverlaps(Range compare)
    {
        if (Start >= compare.Start && End <= compare.End) return true;
        if (compare.Start >= Start && compare.End <= End) return true;
        return false;
    }

    public bool PartialOverlaps(Range compare)
    {
        if (Start > compare.End || End < compare.Start) return false;
        return true;
    }
}