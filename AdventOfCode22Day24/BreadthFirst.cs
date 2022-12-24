namespace AdventOfCode22Day24;
internal class BreadthFirst
{
    public Map Map { get; }

    public BreadthFirst(Map map)
    {
        Map = map;
    }

    public int DoSearch(Location startLocation, Location targetLocation)
    {
        int CompletedSteps = 0;
        List<Location> StatesForPreviousStep = new() { startLocation };
        while (true)
        {
            Map.Step();

            List<Location> StatesForCurrentStep = new() { startLocation };

            foreach (Location prevLoc in StatesForPreviousStep)
            {
                foreach (Location newLoc in prevLoc.Adjacent())
                {
                    if (newLoc == targetLocation) return CompletedSteps + 1;

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
        }
    }
}
