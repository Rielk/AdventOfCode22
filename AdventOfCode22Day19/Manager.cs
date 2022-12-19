namespace AdventOfCode22Day19;
internal class Manager
{
    public int[] OreRobotCost { get; }
    public int[] ClayRobotCost { get; }
    public int[] ObsidianRobotCost { get; }
    public int[] GeodeRobotCost { get; }
    private int[] GetCost(Robot robot) => robot switch
    {
        Robot.Ore => OreRobotCost,
        Robot.Clay => ClayRobotCost,
        Robot.Obsidian => ObsidianRobotCost,
        Robot.Geode => GeodeRobotCost,
        _ => throw new NotImplementedException(),
    };
    public static int GetRobotIndex(Robot robot) => robot switch
    {
        Robot.Ore => 0,
        Robot.Clay => 1,
        Robot.Obsidian => 2,
        Robot.Geode => 3,
        _ => throw new NotImplementedException(),
    };
    private int[] MaxCosts { get; }

    public Manager(int[] oreCost, int[] clayCost, int[] obsidianCost, int[] geodeCost)
    {
        OreRobotCost = oreCost;
        ClayRobotCost = clayCost;
        ObsidianRobotCost = obsidianCost;
        GeodeRobotCost = geodeCost;
        MaxCosts = new int[3]
        {
            Math.Max(Math.Max( OreRobotCost[0], ClayRobotCost[0]), Math.Max(ObsidianRobotCost[0], GeodeRobotCost[0])),
            Math.Max(Math.Max( OreRobotCost[1], ClayRobotCost[1]), Math.Max(ObsidianRobotCost[1], GeodeRobotCost[1])),
            Math.Max(Math.Max( OreRobotCost[2], ClayRobotCost[2]), Math.Max(ObsidianRobotCost[2], GeodeRobotCost[2]))
        };
    }

    public int CalculateMaxGeode(int initialTime)
    {
        IEnumerable<Robot> discard = new List<Robot>();
        return CalculateMaxGeode(initialTime, new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 }, ref discard);
    }
    private int CalculateMaxGeode(int initialTime, int[] initialResources, int[] initialRobots, ref IEnumerable<Robot> buildOrder)
    {
        IEnumerable<Robot>? improvedBuildOrder = null;
        int bestGeodes = initialResources[3] + (initialTime * initialRobots[3]);
        foreach (Robot potentialRobot in Enum.GetValues(typeof(Robot)))
        {
            if (potentialRobot != Robot.Geode && initialRobots[GetRobotIndex(potentialRobot)] >= MaxCosts[GetRobotIndex(potentialRobot)]) continue;
            int time = initialTime;
            int[] resources = initialResources.ToArray();
            int[] robots = initialRobots.ToArray();

            if (CreateRobot(potentialRobot, ref time, ref resources, ref robots))
            {
                IEnumerable<Robot> potentialBuildOrder = buildOrder.Append(potentialRobot);
                int thisGeodes = CalculateMaxGeode(time, resources, robots, ref potentialBuildOrder);

                if (thisGeodes > bestGeodes)
                {
                    bestGeodes = thisGeodes;
                    improvedBuildOrder = potentialBuildOrder;
                }
            }
        }
        buildOrder = improvedBuildOrder ?? buildOrder;
        return bestGeodes;
    }

    private bool CreateRobot(Robot robot, ref int time, ref int[] resources, ref int[] robots)
    {
        bool produced = false;
        while (time > 0)
        {
            int[] cost = GetCost(robot);
            if (resources[0] >= cost[0] && resources[1] >= cost[1] && resources[2] >= cost[2])
                produced = true;
            time--;
            StepTime(resources, robots);
            if (produced)
            {
                robots[GetRobotIndex(robot)]++;
                resources[0] -= cost[0];
                resources[1] -= cost[1];
                resources[2] -= cost[2];
                return true;
            }
        }
        return false;
    }

    private static void StepTime(int[] resources, int[] robots)
    {
        resources[0] += robots[0];
        resources[1] += robots[1];
        resources[2] += robots[2];
        resources[3] += robots[3];
    }
}
