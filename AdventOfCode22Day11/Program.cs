using AdventOfCode22Day11;

Dictionary<int, Monkey> Monkeys = new();

//_ = new Monkey(Monkeys, 0, new() { 79, 98 }, Operation.Mul, 19, 23, 2, 3);
//_ = new Monkey(Monkeys, 1, new() { 54, 65, 75, 74 }, Operation.Add, 6, 19, 2, 0);
//_ = new Monkey(Monkeys, 2, new() { 79, 60, 97 }, Operation.Square, 0, 13, 1, 3);
//_ = new Monkey(Monkeys, 3, new() { 74 }, Operation.Add, 3, 17, 0, 1);

_ = new Monkey(Monkeys, 0, new() { 54, 89, 94 }, Operation.Mul, 7, 17, 5, 3);
_ = new Monkey(Monkeys, 1, new() { 66, 71 }, Operation.Add, 4, 3, 0, 3);
_ = new Monkey(Monkeys, 2, new() { 76, 55, 80, 55, 55, 96, 78 }, Operation.Add, 2, 5, 7, 4);
_ = new Monkey(Monkeys, 3, new() { 93, 69, 76, 66, 89, 54, 59, 94 }, Operation.Add, 7, 7, 5, 2);
_ = new Monkey(Monkeys, 4, new() { 80, 54, 58, 75, 99 }, Operation.Mul, 17, 11, 1, 6);
_ = new Monkey(Monkeys, 5, new() { 69, 70, 85, 83 }, Operation.Add, 8, 19, 2, 7);
_ = new Monkey(Monkeys, 6, new() { 89 }, Operation.Add, 6, 2, 0, 1);
_ = new Monkey(Monkeys, 7, new() { 62, 80, 58, 57, 93, 56 }, Operation.Square, 0, 13, 6, 4);

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