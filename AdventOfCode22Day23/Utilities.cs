namespace AdventOfCode22Day23;
internal static class Utilities
{
    public static int GetEmptyTiles(Dictionary<Location, Elf> ElfLocations)
    {
        int maxX = int.MinValue; int maxY = int.MinValue;
        int minX = int.MaxValue; int minY = int.MaxValue;
        int count = 0;
        foreach (Location loc in ElfLocations.Keys)
        {
            if (loc.x > maxX) maxX = loc.x;
            if (loc.x < minX) minX = loc.x;
            if (loc.y > maxY) maxY = loc.y;
            if (loc.y < minY) minY = loc.y;
            count++;
        }
        int width = maxX - minX + 1;
        int height = maxY - minY + 1;
        int size = width * height;
        int EmptySquares = size - count;
        return EmptySquares;
    }
}
