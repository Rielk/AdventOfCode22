namespace AdventOfCode22Day11;
internal class Monkey
{
    public Monkey(Dictionary<int, Monkey> monkeys, int thisMonkey, List<int> items, Operation operation, int operationFactor, int test, int trueTarget, int falseTarget)
    {
        Monkeys = monkeys;
        if (Monkeys.ContainsKey(thisMonkey)) throw new ArgumentException(nameof(thisMonkey), "A monkey of this number already exists");
        Monkeys[thisMonkey] = this;

        Items = items;
        Operation = operation;
        OperationFactor = operationFactor;
        Test = test;
        TrueTarget = trueTarget;
        FalseTarget = falseTarget;
    }

    public List<int> Items { get; } = new();
    public Operation Operation { get; }
    public int OperationFactor { get; }
    public int Test { get; }
    public int TrueTarget { get; }
    public int FalseTarget { get; }

    public Dictionary<int, Monkey> Monkeys { get; }

    public int InspectionCount { get; private set; }

    public void InspectAll()
    {
        foreach (int item in Items.ToArray())
        {
            InspectionCount++;
            Items.RemoveAt(0);
            int start = Operation switch
            {
                Operation.Add => item + OperationFactor,
                Operation.Mul => item * OperationFactor,
                Operation.Square => item * item,
                _ => throw new NotImplementedException(),
            };
            start /= 3;

            Monkeys[start % Test == 0 ? TrueTarget : FalseTarget].Items.Add(start);
        }
    }
}

internal enum Operation
{
    Add, Mul, Square
}
