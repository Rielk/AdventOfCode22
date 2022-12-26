using AdventOfCode22Day07;
using AdventOfCode22Day07.Properties;

string input = Resources.Input1;

Folder ActiveFolder = new("/", null);
Folder RootFolder = ActiveFolder;

//Create Structure
foreach (string line in input.Split(Environment.NewLine))
{
    if (line.StartsWith("$ ls"))
        continue;

    else if (line.StartsWith("$ cd "))
    {
        string folderName = line.Substring(5);
        if (folderName == "..")
            ActiveFolder = ActiveFolder.Parent ?? throw new NotImplementedException();
        else if (folderName == "/")
            ActiveFolder = RootFolder;
        else
            ActiveFolder = ActiveFolder.GetFolder(folderName);
        continue;
    }

    else if (line.StartsWith("dir"))
    {
        string folderName = line.Substring(3);
        ActiveFolder.AddFolder(folderName);
        continue;
    }

    else
    {
        string[] part = line.Split(' ');
        if (part.Length > 2) throw new NotImplementedException();

        int size = int.Parse(part[0]);

        ActiveFolder.AddFile(part[1], size);
    }
}

int SumOfSmalls = RootFolder.GetFoldersAtMost(100000).Select(x => x.FileSize).Sum();

const int TotalSize = 70000000;
const int TargetSpace = 30000000;

int FreeSpace = TotalSize - RootFolder.FileSize;
int NeededSpace = TargetSpace - FreeSpace;

int SmallestFolderLargeEnough = RootFolder.GetFoldersAtLeast(NeededSpace).Select(x => x.FileSize).Min();

Console.WriteLine($"Sum of sizes of folders less than 100000: {SumOfSmalls}");
Console.WriteLine();
Console.WriteLine($"Smallest folder to free up enough space: {SmallestFolderLargeEnough}");