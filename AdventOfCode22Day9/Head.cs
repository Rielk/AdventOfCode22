namespace AdventOfCode22Day9;
internal class Head
{
    public static Head CreateLength(int n)
    {
        var head = new Head();
        Head firstHead = head;
        int i = 1;
        while (i < n)
        {
            head = head.Tail = new(head);
            i++;
        }
        return firstHead;
    }

    protected Head() { }

    public Tail? Tail { get; private set; }
    public Position Position { get; protected set; } = new(0, 0);

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

            Tail?.Update();
            i++;
        }
    }
}
