namespace AdventOfCode22Day21;
internal class ValueMonkey : Monkey
{
    public ValueMonkey(string name, int value) : base(name)
    {
        Value = value;
    }

    public override long Value { get; }
}
