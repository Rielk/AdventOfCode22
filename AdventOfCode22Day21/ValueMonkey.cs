namespace AdventOfCode22Day21;
internal class ValueMonkey : Monkey
{
    public ValueMonkey(string name, int value) : base(name)
    {
        this.value = value;
    }

    private long? value;
    public override long? Value { get => value; }

    internal void NullifyValue() => value = null;

    internal override void AssertEqual(long target)
    {
        if (Value != null) throw new ArgumentException($"{nameof(ValueMonkey)} must have a {nameof(Value)} of \"null\"");
        value = target;
    }
}
