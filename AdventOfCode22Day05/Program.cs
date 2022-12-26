using AdventOfCode22Day05.Properties;

string stackInput = Resources.Input1;
string moveInput = Resources.Input2;

var Stack = new List<char>[9];
for (int i = 0; i < 9; i++)
    Stack[i] = new();

foreach (string line in stackInput.Split(Environment.NewLine).Reverse())
    foreach ((char letter, int index) in line.Chunk(4).Select((x, i) => (x[1], i)))
        if (letter != ' ')
            Stack[index].Add(letter);

foreach (string order in moveInput.Split(Environment.NewLine))
{
    string[] parts = order.Split(' ');
    int count = int.Parse(parts[1]);
    int fromI = int.Parse(parts[3]);
    int toI = int.Parse(parts[5]);

    List<char> fromPile = Stack[fromI - 1];
    List<char> toPile = Stack[toI - 1];

    char[] movedChars = fromPile.TakeLast(count).ToArray();
    for (int i = 0; i < count; i++)
        fromPile.RemoveAt(fromPile.Count - 1);

    foreach (char c in movedChars.Reverse())
        toPile.Add(c);
}

foreach (List<char> pile in Stack)
{
    foreach (char c in pile)
        Console.Write($"[{c}] ");
    Console.WriteLine();
}

Console.WriteLine();

Stack = new List<char>[9];
for (int i = 0; i < 9; i++)
    Stack[i] = new();

foreach (string line in stackInput.Split(Environment.NewLine).Reverse())
    foreach ((char letter, int index) in line.Chunk(4).Select((x, i) => (x[1], i)))
        if (letter != ' ')
            Stack[index].Add(letter);

foreach (string order in moveInput.Split(Environment.NewLine))
{
    string[] parts = order.Split(' ');
    int count = int.Parse(parts[1]);
    int fromI = int.Parse(parts[3]);
    int toI = int.Parse(parts[5]);

    List<char> fromPile = Stack[fromI - 1];
    List<char> toPile = Stack[toI - 1];

    char[] movedChars = fromPile.TakeLast(count).ToArray();
    for (int i = 0; i < count; i++)
        fromPile.RemoveAt(fromPile.Count - 1);

    toPile.AddRange(movedChars);
}

foreach (List<char> pile in Stack)
{
    foreach (char c in pile)
        Console.Write($"[{c}] ");
    Console.WriteLine();
}