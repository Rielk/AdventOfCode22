using AdventOfCode22Day23;
using AdventOfCode22Day23.Properties;

string input = Resources.Input1;

Dictionary<Location, Elf> ElfLocations = new();
int y = 0;
foreach (string line in input.Split(Environment.NewLine))
{
    int x = 0;
    foreach (char c in line)
    {
        if (c == '#')
        {
            Location loc = new(x, y);
            Elf elf = new(ElfLocations, loc);
        }
        x++;
    }
    y++;
}

Direction StartDirection = Direction.North;
foreach (int _ in Enumerable.Range(0, 10))
{
    bool stillMoving = false;
    List<Elf> toMove = new();
    foreach (Elf elf in ElfLocations.Values)
        if (elf.CheckForMove(StartDirection))
        {
            stillMoving = true;
            toMove.Add(elf);
        }
    StartDirection = StartDirection.NextOrdinal();

    if (!stillMoving)
        break;

    foreach (Elf elf in toMove)
    {
        elf.CommitMove();
    }
}

int EmptyTiles = Utilities.GetEmptyTiles(ElfLocations);

Console.WriteLine($"Empty tiles in smallest rectange: {EmptyTiles}");
Console.WriteLine();