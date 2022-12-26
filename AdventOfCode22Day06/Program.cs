using AdventOfCode22Day06.Properties;

string input = Resources.Input1;

Console.WriteLine($"Characters processed before the first packet marker: {FindXUnique(4)}");
Console.WriteLine();
Console.WriteLine($"Characters processed before the first message marker: {FindXUnique(14)}");

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