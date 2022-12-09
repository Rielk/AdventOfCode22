namespace AdventOfCode22Day9;
internal class Head
{
    public Head()
    {
        Tail = new(this);
    }

    public Tail Tail { get; }
    public Position Position { get; private set; } = new(0, 0);

    public void Move(Direction direction, int n)
    {
        int i = 0;
        while (i < n)
        {
            _ = direction switch
            {
                Direction.Up => Position = new(Position.x, Position.y + 1),
                Direction.Down => Position = new(Position.x, Position.y - 1),
                Direction.Left => Position = new(Position.x - 1, Position.y),
                Direction.Right => Position = new(Position.x + 1, Position.y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), $"{direction} is not a recognised Direction")
            };

            Tail.Update();
            i++;
        }
    }
}
