using AdventOfCode22Day6.Properties;

string input = Resources.Input1;

int countToMarker = FindXUnique(4);

Console.WriteLine($"Characters processed before the first marker: {countToMarker}");
Console.WriteLine();

int FindXUnique(int n)
{
    int countToMarker = 0;
    List<char> buffer = new();

    foreach (char c in input)
    {
        countToMarker++;
        buffer.Add(c);
        while (buffer.Count > n)
            buffer.RemoveAt(0);
        if (buffer.Distinct().Count() == n)
            break;
    }

    return countToMarker;
}