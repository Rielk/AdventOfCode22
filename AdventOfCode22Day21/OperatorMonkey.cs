namespace AdventOfCode22Day21;
internal abstract class OperatorMonkey : Monkey
{
    public Monkey Monkey1 { get; private set; } = NullMonkey.Default;
    public Monkey Monkey2 { get; private set; } = NullMonkey.Default;

    private long? value = null;
    public override long? Value => value ??= DoOperation();

    public void ClearCachedValues()
    {
        value = null;
        if (Monkey1 is OperatorMonkey om1)
            om1.ClearCachedValues();
        if (Monkey2 is OperatorMonkey om2)
            om2.ClearCachedValues();
    }

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

    protected abstract long? DoOperation();

    internal override void AssertEqual(long target)
    {
        if ((Monkey1.Value == null && Monkey2.Value == null) || (Monkey1.Value != null && Monkey2.Value != null))
            throw new ArgumentException($"Exactly one {nameof(Monkey)} must have a {nameof(Value)} of \"null\"");
        if (Monkey1.Value == null)
            Monkey1.AssertEqual(InvertForMonkey1(target));
        else if (Monkey2.Value == null)
            Monkey2.AssertEqual(InvertForMonkey2(target));
        value = target;
    }

    protected abstract long InvertForMonkey1(long target);
    protected abstract long InvertForMonkey2(long target);
}

internal class AdditionMonkey : OperatorMonkey
{
    public AdditionMonkey(string name) : base(name)
    { }

    protected override long? DoOperation() => Monkey1.Value + Monkey2.Value;
    protected override long InvertForMonkey1(long target) => target - Monkey2.Value!.Value;
    protected override long InvertForMonkey2(long target) => target - Monkey1.Value!.Value;
}

internal class SubtractionMonkey : OperatorMonkey
{
    public SubtractionMonkey(string name) : base(name)
    { }

    protected override long? DoOperation() => Monkey1.Value - Monkey2.Value;
    protected override long InvertForMonkey1(long target) => target + Monkey2.Value!.Value;
    protected override long InvertForMonkey2(long target) => Monkey1.Value!.Value - target;
}

internal class MultiplicationMonkey : OperatorMonkey
{
    public MultiplicationMonkey(string name) : base(name)
    { }

    protected override long? DoOperation() => Monkey1.Value * Monkey2.Value;
    protected override long InvertForMonkey1(long target) => target / Monkey2.Value!.Value;
    protected override long InvertForMonkey2(long target) => target / Monkey1.Value!.Value;
}

internal class DivisionMonkey : OperatorMonkey
{
    public DivisionMonkey(string name) : base(name)
    { }

    protected override long? DoOperation() => Monkey1.Value / Monkey2.Value;
    protected override long InvertForMonkey1(long target) => target * Monkey2.Value!.Value;
    protected override long InvertForMonkey2(long target) => Monkey1.Value!.Value / target;
}
