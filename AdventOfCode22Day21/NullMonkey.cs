namespace AdventOfCode22Day21;
internal class NullMonkey : Monkey
{
    public static NullMonkey Default { get; } = new();
    private NullMonkey() : base("NULL")
    {
    }

    public override long Value => throw new Exception($"Cannot get {nameof(Value)} from a {nameof(NullMonkey)}");
}
