namespace AdventOfCode22Day21;
internal abstract class Monkey
{
    public abstract long Value { get; }

    public string Name { get; }

    protected Monkey(string name)
    {
        Name = name;
    }
}
