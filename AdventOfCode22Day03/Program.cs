using AdventOfCode22Day03.Properties;

string input = Resources.Input1;

int prioritySum = 0;

foreach (char[] Items in input.Split(Environment.NewLine).Select(x => x.ToCharArray()))
{
    int n = Items.Length / 2;
    if (n * 2 != Items.Length) throw new NotImplementedException("Odd number of items");

    IEnumerable<char> firHalf = Items.Take(n);
    IEnumerable<char> secHalf = Items.Skip(n);

    IEnumerable<char> matches = firHalf.Intersect(secHalf);

    foreach (char item in matches)
        prioritySum += CharToPriority(item);
}

Console.WriteLine($"Sum of individual priorities: {prioritySum}");
Console.WriteLine();

prioritySum = 0;

foreach (char[][] Group in input.Split(Environment.NewLine).Select(x => x.ToCharArray()).Chunk(3).ToArray())
{
    char[] items1 = Group[0];
    char[] items2 = Group[1];
    char[] items3 = Group[2];

    IEnumerable<char> matches = items1.Intersect(items2).Intersect(items3);

    foreach (char item in matches)
        prioritySum += CharToPriority(item);
}

Console.WriteLine($"Sum of group priorities: {prioritySum}");

static int CharToPriority(char item)
{
    if (char.IsUpper(item))
        return item - 38;
    else if (char.IsLower(item))
        return item - 96;
    else
        throw new NotImplementedException();
}