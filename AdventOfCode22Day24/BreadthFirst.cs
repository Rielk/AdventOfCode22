namespace AdventOfCode22Day24;
internal class BreadthFirst
{
    public Map Map { get; }
    public int StartColumn { get; }
    public int EndColumn { get; }

    public int CompletedSteps { get; private set; } = 0;

    public List<List<Location>> StateSpace { get; } = new();

    private bool Completed = false;

    public BreadthFirst(Map map, int startColumn, int endColumn)
    {
        Map = map;
        StartColumn = startColumn;
        EndColumn = endColumn;
    }

    public int DoSearch()
    {
        if (Completed) throw new Exception("Already completed");
        Completed = true;

        List<Location> StatesForPreviousStep = new() { new(StartColumn, -1) };
        while (true)
        {
            Map.Step();

            StateSpace.Add(new() { new(StartColumn, -1) });
            List<Location> StatesForCurrentStep = StateSpace[CompletedSteps];

            foreach (Location prevLoc in StatesForPreviousStep)
            {
                foreach (Location newLoc in prevLoc.Adjacent())
                {
                    if (newLoc.x < 0) continue;
                    if (newLoc.y < 0) continue;
                    if (newLoc.x >= Map.Width) continue;
                    if (newLoc.y >= Map.Height) continue;
                    if (Map.BlizzardAtLocation(newLoc)) continue;
                    if (!StatesForCurrentStep.Contains(newLoc))
                        StatesForCurrentStep.Add(newLoc);
                }
            }

            CompletedSteps++;
            StatesForPreviousStep = StatesForCurrentStep;

            foreach (Location loc in StatesForCurrentStep)
                if (loc.y == Map.Height - 1 && loc.x == EndColumn)
                    return CompletedSteps + 1;
        }
    }
}
