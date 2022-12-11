using AdventOfCode22Day11;

Dictionary<int, Monkey> Monkeys = new();

_ = new Monkey(Monkeys, 0, new() { 79, 98 }, Operation.Mul, 19, 23, 2, 3);
_ = new Monkey(Monkeys, 1, new() { 54, 65, 75, 74 }, Operation.Add, 6, 19, 2, 0);
_ = new Monkey(Monkeys, 2, new() { 79, 60, 97 }, Operation.Square, 0, 13, 1, 3);
_ = new Monkey(Monkeys, 3, new() { 74 }, Operation.Add, 3, 17, 0, 1);

for (int n = 0; n < 20; n++)
    for (int i = 0; i < Monkeys.Count; i++)
    {
        Monkey monkey = Monkeys[i];
        monkey.InspectAll();
    }

for (int i = 0; i < Monkeys.Count; i++)
{
    Console.WriteLine($"Monkey {i} inspected items {Monkeys[i].InspectionCount} times.");
}

int MonkeyBusiness = Monkeys.Values.Select(m => m.InspectionCount).OrderByDescending(x => x).Take(2).Aggregate(1, (x, y) => x * y);

Console.WriteLine();
Console.WriteLine($"Monkey Business is: {MonkeyBusiness}");