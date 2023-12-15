public class Folder
{
    public string Name;
    public int Size = 0;
    public Folder? Parent;
    public List<Folder> SubFolders = new();

    public Folder(string name, Folder? parent)
    {
        Name = name;
        Parent = parent;
    }
}
