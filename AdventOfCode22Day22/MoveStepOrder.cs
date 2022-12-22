namespace AdventOfCode22Day22;
internal class MoveStepOrder : StepOrder
{
    public MoveStepOrder(int count)
    {
        Count = count;
    }

    public int Count { get; }

    public override void Execute(Map map) => map.Move(Count);
}
