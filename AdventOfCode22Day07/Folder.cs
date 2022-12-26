namespace AdventOfCode22Day07;
internal class Folder
{
    public Folder(string name, Folder? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; }
    public Folder? Parent { get; }
    public List<Folder> Folders { get; } = new();
    public void AddFolder(string name) => Folders.Add(new Folder(name.Trim(), this));
    public Folder GetFolder(string name) => Folders.First(x => x.Name == name.Trim());

    public List<File> Files { get; } = new();
    public void AddFile(string name, int size)
    {
        Files.Add(new File(name.Trim(), size));
        IncreaseFileSize(size);
    }

    public int FileSize { get; private set; }
    private void IncreaseFileSize(int size)
    {
        FileSize += size;
        Parent?.IncreaseFileSize(size);
    }

    public List<Folder> GetFoldersAtMost(int x)
    {
        List<Folder> ret = new();
        foreach (Folder folder in Folders)
            ret.AddRange(folder.GetFoldersAtMost(x));

        if (FileSize <= x)
            ret.Add(this);

        return ret;
    }

    public List<Folder> GetFoldersAtLeast(int x)
    {
        List<Folder> ret = new();
        foreach (Folder folder in Folders)
            ret.AddRange(folder.GetFoldersAtLeast(x));

        if (FileSize >= x)
            ret.Add(this);

        return ret;
    }
}
