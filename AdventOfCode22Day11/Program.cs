using AdventOfCode22Day11;
using AdventOfCode22Day11.Properties;

Dictionary<int, Monkey> Monkeys = CreateMonkeys();
RunForNRounds(20, true);
WriteOutput();

Console.WriteLine();
Console.WriteLine();

Monkeys = CreateMonkeys();
RunForNRounds(10000, false);
WriteOutput();

static Dictionary<int, Monkey> CreateMonkeys()
{
    string input = Resources.Input1;

    Dictionary<int, Monkey> Monkeys = new();

    string[] monkeyStringArray = input.Split(string.Join("", new string[] { Environment.NewLine, Environment.NewLine }));
    foreach (string monkeyString in monkeyStringArray)
    {
        string[] line = monkeyString.Split(Environment.NewLine);
        int n = int.Parse(new string(line[0].Where(c => char.IsDigit(c)).ToArray()));
        var startingList = line[1].Split(':')[1].Split(',').Select(s => long.Parse(s.Trim())).ToList();
        Operation op = line[2].Trim()[21] switch { '*' => Operation.Mul, '+' => Operation.Add, _ => throw new NotImplementedException() };
        string opFactorS = new(line[2].Trim().Skip(22).ToArray());
        int opFactor = 0;
        if (opFactorS.Trim() == "old") op = Operation.Square;
        else opFactor = int.Parse(opFactorS);
        int div = int.Parse(new string(line[3].Where(c => char.IsDigit(c)).ToArray()));
        int tMonk = int.Parse(new string(line[4].Where(c => char.IsDigit(c)).ToArray()));
        int fMonk = int.Parse(new string(line[5].Where(c => char.IsDigit(c)).ToArray()));

        _ = new Monkey(Monkeys, n, startingList, op, opFactor, div, tMonk, fMonk);
    }
    return Monkeys;
}

void RunForNRounds(int count, bool worryReduction)
{
    int? common = worryReduction ? null : Monkeys.Values.Select(m => m.Test).Aggregate(1, (x, y) => x * y);

    for (int n = 0; n < count; n++)
        for (int i = 0; i < Monkeys.Count; i++)
        {
            Monkey monkey = Monkeys[i];
            monkey.InspectAll(common);
        }
}

void WriteOutput()
{
    for (int i = 0; i < Monkeys.Count; i++)
    {
        Console.WriteLine($"Monkey {i} inspected items {Monkeys[i].InspectionCount} times.");
    }

    long[] largest = Monkeys.Values.Select(m => (long)m.InspectionCount).OrderByDescending(x => x).Take(2).ToArray();
    long MonkeyBusiness = largest[0] * largest[1];

    Console.WriteLine();
    Console.WriteLine($"Monkey Business is: {MonkeyBusiness}");
}