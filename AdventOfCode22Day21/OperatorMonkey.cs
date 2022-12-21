namespace AdventOfCode22Day21;
internal abstract class OperatorMonkey : Monkey
{
    public Monkey Monkey1 { get; private set; } = NullMonkey.Default;
    public Monkey Monkey2 { get; private set; } = NullMonkey.Default;

    private long? value = null;
    public override long Value { get { value ??= DoOperation(); return value.Value; } }

    protected OperatorMonkey(string name) : base(name)
    {
    }

    public Action<Dictionary<string, Monkey>> GetSetupAction(string monkey1S, string monkey2S)
    {
        void AssignMonkeys(Dictionary<string, Monkey> dict)
        {
            Monkey1 = dict[monkey1S];
            Monkey2 = dict[monkey2S];
        }
        return AssignMonkeys;
    }

    protected abstract long DoOperation();
}

internal class AdditionMonkey : OperatorMonkey
{
    public AdditionMonkey(string name) : base(name)
    { }

    protected override long DoOperation() => Monkey1.Value + Monkey2.Value;
}

internal class SubtractionMonkey : OperatorMonkey
{
    public SubtractionMonkey(string name) : base(name)
    { }

    protected override long DoOperation() => Monkey1.Value - Monkey2.Value;
}

internal class MultiplicationMonkey : OperatorMonkey
{
    public MultiplicationMonkey(string name) : base(name)
    { }

    protected override long DoOperation() => Monkey1.Value * Monkey2.Value;
}

internal class DivisionMonkey : OperatorMonkey
{
    public DivisionMonkey(string name) : base(name)
    { }

    protected override long DoOperation() => Monkey1.Value / Monkey2.Value;
}
