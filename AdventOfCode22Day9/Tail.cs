namespace AdventOfCode22Day9;
internal class Tail
{
    public Tail(Head head)
    {
        Head = head;
    }

    public Head Head { get; }
    public Position Position { get; private set; } = new(0, 0);
    public Position HeadPosition => Head.Position;

    public List<Position> PositionHistory { get; } = new() { new(0, 0) };

    public void Update()
    {
        if (HeadPosition.x < Position.x - 1) //If a gap to the left
            Position = new(HeadPosition.x + 1, HeadPosition.y);
        else if (HeadPosition.x > Position.x + 1) //If a gap to the right
            Position = new(HeadPosition.x - 1, HeadPosition.y);
        else if (HeadPosition.y < Position.y - 1) //If a gap to the down
            Position = new(HeadPosition.x, HeadPosition.y + 1);
        else if (HeadPosition.y > Position.y + 1) //If a gap to the up
            Position = new(HeadPosition.x, HeadPosition.y - 1);
        else //Don't Move
            return;

        //Will only be called if it doesn't return, ie. if position is changed.
        PositionHistory.Add(Position);
    }
}
