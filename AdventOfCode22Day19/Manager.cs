namespace AdventOfCode22Day19;
internal class Manager
{
    public int[] OreRobotCost { get; }
    public int[] ClayRobotCost { get; }
    public int[] ObsidianRobotCost { get; }
    public int[] GeodeRobotCost { get; }
    public int[][] AllCosts { get; }

    public int MaxOre { get; }
    public int MaxClay { get; }
    public int MaxObsidian { get; }

    public int MaxOreRobots { get; }
    public int MaxClayRobots { get; }
    public int MaxObsidianRobots { get; }

    private Dictionary<(int, int, int, int, int, int, int), int>[] EntireStateSpace { get; }

    public int MaxGeodesAtTime(int t)
    {
        Dictionary<(int, int, int, int, int, int, int), int> StateSpace = EntireStateSpace[t];
        return StateSpace.Values.Max();
    }

    public Manager(int maxTime, int[] oreCost, int[] clayCost, int[] obsidianCost, int[] geodeCost)
    {
        OreRobotCost = oreCost;
        ClayRobotCost = clayCost;
        ObsidianRobotCost = obsidianCost;
        GeodeRobotCost = geodeCost;
        AllCosts = new int[][] { OreRobotCost, ClayRobotCost, ObsidianRobotCost, GeodeRobotCost };

        MaxOreRobots = Math.Max(Math.Max(OreRobotCost[0], ClayRobotCost[0]), Math.Max(ObsidianRobotCost[0], GeodeRobotCost[0]));
        MaxClayRobots = Math.Max(Math.Max(OreRobotCost[1], ClayRobotCost[1]), Math.Max(ObsidianRobotCost[1], GeodeRobotCost[1]));
        MaxObsidianRobots = Math.Max(Math.Max(OreRobotCost[2], ClayRobotCost[2]), Math.Max(ObsidianRobotCost[2], GeodeRobotCost[2]));

        MaxOre = MaxOreRobots * 2;
        MaxClay = MaxClayRobots * 2;
        MaxObsidian = MaxObsidianRobots * 2;

        //Ore, Clay, Obsidian, OreR, ClayR, ObsidianR
        Dictionary<(int, int, int, int, int, int, int), int> stateSpace = new()
        {
            { (0, 0, 0, 1, 0, 0, 0), 0 }
        };

        EntireStateSpace = new Dictionary<(int, int, int, int, int, int, int), int>[maxTime + 1];
        EntireStateSpace[0] = stateSpace;

        foreach (int timeRemaining in Enumerable.Range(1, maxTime).Reverse())
        {
            stateSpace = NextStateSpace(timeRemaining, stateSpace);
            EntireStateSpace[maxTime - timeRemaining + 1] = stateSpace;
        }
    }

    private Dictionary<(int, int, int, int, int, int, int), int> NextStateSpace(int timeRemaining, Dictionary<(int, int, int, int, int, int, int), int> oldStateSpace)
    {
        Dictionary<(int, int, int, int, int, int, int), int> newStateSpace = new();
        foreach (((int, int, int, int, int, int, int) state, int value) in oldStateSpace.Select(p => (p.Key, p.Value)))
        {
            foreach (int i in Enumerable.Range(0, 5))
            {
                int ore, clay, obsidian, oreRob, clayRob, obsidianRob, geodeRob, currentGeodes;
                (ore, clay, obsidian, oreRob, clayRob, obsidianRob, geodeRob) = state;
                currentGeodes = value;
                bool makeRobot = false;
                if (i != 4)
                {
                    int[] cost = AllCosts[i];
                    if (ore >= cost[0] && clay >= cost[1] && obsidian >= cost[2])
                    {
                        makeRobot = true;
                        ore -= cost[0];
                        clay -= cost[1];
                        obsidian -= cost[2];
                    }
                }
                ore += oreRob;
                clay += clayRob;
                obsidian += obsidianRob;
                currentGeodes += geodeRob;

                if (makeRobot)
                    switch (i)
                    {
                        case 0:
                            oreRob++; break;
                        case 1:
                            clayRob++; break;
                        case 2:
                            obsidianRob++; break;
                        case 3:
                            geodeRob++; break;
                    }

                if (ore > MaxOre) ore = MaxOre;
                if (clay > MaxClay) clay = MaxClay;
                if (obsidian > MaxObsidian) obsidian = MaxObsidian;
                if (oreRob > MaxOreRobots) break;
                if (clayRob > MaxClayRobots) break;
                if (obsidianRob > MaxObsidianRobots) break;

                if (!newStateSpace.TryAdd((ore, clay, obsidian, oreRob, clayRob, obsidianRob, geodeRob), currentGeodes))
                    if (newStateSpace[(ore, clay, obsidian, oreRob, clayRob, obsidianRob, geodeRob)] < currentGeodes)
                        newStateSpace[(ore, clay, obsidian, oreRob, clayRob, obsidianRob, geodeRob)] = currentGeodes;
            }
        }
        return newStateSpace;
    }
}
