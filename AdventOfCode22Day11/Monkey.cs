using System.Numerics;

namespace AdventOfCode22Day11;
internal class Monkey
{
    public Monkey(Dictionary<int, Monkey> monkeys, int thisMonkey, List<BigInteger> items, Operation operation, int operationFactor, int test, int trueTarget, int falseTarget)
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

    public List<BigInteger> Items { get; } = new();
    public Operation Operation { get; }
    public int OperationFactor { get; }
    public int Test { get; }
    public int TrueTarget { get; }
    public int FalseTarget { get; }

    public Dictionary<int, Monkey> Monkeys { get; }

    public int InspectionCount { get; private set; }

    public void InspectAll(int? commonMultiple)
    {
        foreach (BigInteger item in Items.ToArray())
        {
            InspectionCount++;
            Items.RemoveAt(0);
            BigInteger worry = Operation switch
            {
                Operation.Add => item + OperationFactor,
                Operation.Mul => item * OperationFactor,
                Operation.Square => item * item,
                _ => throw new NotImplementedException(),
            };
            if (commonMultiple.HasValue)
                worry %= commonMultiple.Value;
            else
                worry /= 3;

            Monkeys[worry % Test == 0 ? TrueTarget : FalseTarget].Items.Add(worry);
        }
    }
}

internal enum Operation
{
    Add, Mul, Square
}
