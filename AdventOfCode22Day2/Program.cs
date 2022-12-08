using AdventOfCode22Day2.Properties;

string input = Resources.Input1;

int Score = 0;
foreach (string item in input.Split(Environment.NewLine))
{
    string[] split = item.Split(' ');
    Score += (split[0][0], split[1][0]) switch
    {
        ('A', 'X') => 3 + 1, //R, R
        ('B', 'X') => 0 + 1, //P, R
        ('C', 'X') => 6 + 1, //S, R

        ('A', 'Y') => 6 + 2, //R, P
        ('B', 'Y') => 3 + 2, //P, P
        ('C', 'Y') => 0 + 2, //S, P

        ('A', 'Z') => 0 + 3, //R, S
        ('B', 'Z') => 6 + 3, //P, S
        ('C', 'Z') => 3 + 3, //S, S

        (_, 'X' or 'Y' or 'Z') => throw new ArgumentException(nameof(split), "Unrecognised characters is first"),
        ('A' or 'B' or 'C', _) => throw new ArgumentException(nameof(split), "Unrecognised characters is second"),
        _ => throw new ArgumentException(nameof(split), "Unrecognised characters in both")
    };
}

Console.WriteLine($"Score by strategy guide: {Score}");
Console.WriteLine();

Score = 0;
foreach (string item in input.Split(Environment.NewLine))
{
    string[] split = item.Split(' ');
    Score += (split[0][0], split[1][0]) switch
    {
        ('A', 'X') => 0 + 3, //R, L = S
        ('B', 'X') => 0 + 1, //P, L = R
        ('C', 'X') => 0 + 2, //S, L = P

        ('A', 'Y') => 3 + 1, //R, D = R
        ('B', 'Y') => 3 + 2, //P, D = P
        ('C', 'Y') => 3 + 3, //S, D = S

        ('A', 'Z') => 6 + 2, //R, W = P
        ('B', 'Z') => 6 + 3, //P, W = S
        ('C', 'Z') => 6 + 1, //S, W = R

        (_, 'X' or 'Y' or 'Z') => throw new ArgumentException(nameof(split), "Unrecognised characters is first"),
        ('A' or 'B' or 'C', _) => throw new ArgumentException(nameof(split), "Unrecognised characters is second"),
        _ => throw new ArgumentException(nameof(split), "Unrecognised characters in both")
    };
}

Console.WriteLine($"Score by revised guide: {Score}");

