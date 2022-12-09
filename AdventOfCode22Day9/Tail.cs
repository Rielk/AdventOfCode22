namespace AdventOfCode22Day9;
internal class Tail : Head
{
    internal Tail(Head head)
    {
        Head = head;
    }

    public Head Head { get; }
    public Position HeadPosition => Head.Position;
    public override Tail? VeryTail => Tail?.VeryTail ?? this;

    public List<Position> PositionHistory { get; } = new() { new(0, 0) };

    public void Update()
    {
        Position headOfset = new(HeadPosition.x - Position.x, HeadPosition.y - Position.y);

        bool ToFarU = headOfset.y >= 2;
        bool ToFarD = headOfset.y <= -2;
        bool ToFarL = headOfset.x <= -2;
        bool ToFarR = headOfset.x >= 2;

        bool IsU = headOfset.y > 0;
        bool IsD = headOfset.y < 0;
        bool IsL = headOfset.x < 0;
        bool IsR = headOfset.x > 0;

        Direction direction = (ToFarU, ToFarD, ToFarL, ToFarR, IsU, IsD, IsL, IsR) switch
        {
            (true, _, true, _, _, _, _, _) => Direction.UL,
            (true, _, _, true, _, _, _, _) => Direction.UR,

            (_, true, true, _, _, _, _, _) => Direction.DL,
            (_, true, _, true, _, _, _, _) => Direction.DR,

            (true, _, false, false, _, _, false, false) => Direction.Up,
            (true, _, false, false, _, _, true, _) => Direction.UL,
            (true, _, false, false, _, _, _, true) => Direction.UR,

            (_, true, false, false, _, _, false, false) => Direction.Down,
            (_, true, false, false, _, _, true, _) => Direction.DL,
            (_, true, false, false, _, _, _, true) => Direction.DR,

            (false, false, true, _, false, false, _, _) => Direction.Left,
            (false, false, true, _, true, _, _, _) => Direction.UL,
            (false, false, true, _, _, true, _, _) => Direction.DL,

            (false, false, _, true, false, false, _, _) => Direction.Right,
            (false, false, _, true, true, _, _, _) => Direction.UR,
            (false, false, _, true, _, true, _, _) => Direction.DR,

            (false, false, false, false, _, _, _, _) => Direction.None
        };

        if (direction != Direction.None)
        {
            Move(direction, 1);
            PositionHistory.Add(Position);
            Tail?.Update();
        }
    }
}
