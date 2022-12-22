using AdventOfCode22Day22;
using AdventOfCode22Day22.Properties;

string inputMap = Resources.Input1;
string inputDirection = Resources.Input2;

Map Map = new(inputMap);

List<StepOrder> StepOrders = new();
{
    string buffer = string.Empty;
    foreach (char c in inputDirection)
    {
        if (char.IsDigit(c))
            buffer += c;
        else
        {
            if (!string.IsNullOrEmpty(buffer))
            {
                StepOrders.Add(new MoveStepOrder(int.Parse(buffer)));
                buffer = string.Empty;
            }
            StepOrders.Add(new TurnStepOrder(c switch
            {
                'R' => true,
                'L' => false,
                _ => throw new NotImplementedException()
            }));
        }
    }
    if (!string.IsNullOrEmpty(buffer))
        StepOrders.Add(new MoveStepOrder(int.Parse(buffer)));
}

foreach (StepOrder order in StepOrders)
    order.Execute(Map);

int FinalPassword = (1000 * Map.Location.y) + (4 * Map.Location.x) + Map.LookingDirection.ToScore();
Console.WriteLine($"Final Password: {FinalPassword}");
Console.WriteLine();
