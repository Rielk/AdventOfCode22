namespace AdventOfCode22Day22;
internal class TurnStepOrder : StepOrder
{
    public TurnStepOrder(bool clockwise)
    {
        Clockwise = clockwise;
    }

    public bool Clockwise { get; }

    public override void Execute(Map map) => map.Turn(Clockwise);
}
