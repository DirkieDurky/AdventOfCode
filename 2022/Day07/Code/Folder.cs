public class Folder
{
    public String Name;
    public Int32 Size = 0;
    public Folder? Parent;
    public List<Folder> SubFolders = new();

    public Folder(String name, Folder? parent)
    {
        Name = name;
        Parent = parent;
    }
}