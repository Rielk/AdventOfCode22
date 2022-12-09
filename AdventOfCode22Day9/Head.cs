namespace AdventOfCode22Day9;
internal class Head
{
    public static Head CreateLength(int n)
    {
        Head head = new();
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
    public virtual Tail? VeryTail => Tail?.VeryTail;

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
                Direction.UL => Position = new(Position.x - 1, Position.y + 1),
                Direction.UR => Position = new(Position.x + 1, Position.y + 1),
                Direction.DL => Position = new(Position.x - 1, Position.y - 1),
                Direction.DR => Position = new(Position.x + 1, Position.y - 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), $"{direction} is not a recognised Direction")
            };

            Tail?.Update();
            i++;
        }
    }
}
