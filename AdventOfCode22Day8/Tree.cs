namespace AdventOfCode22Day8;
internal class Tree
{
    public int Height { get; }
    public bool Visible { get; private set; } = false;

    internal void SetVisibility(bool value) => Visible = value;

    public Tree(int height)
    {
        Height = height;
    }
}
