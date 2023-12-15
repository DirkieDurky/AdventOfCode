using System.Text.RegularExpressions;
namespace Year2022
{
    public class Day07 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');
            List<Folder> folderList = new();
            Folder? currentFolder = null;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("$ cd "))
                {
                    string newFolderName = line.Replace("$ cd ", "");
                    if (newFolderName == "..")
                    {
                        currentFolder = currentFolder!.Parent;
                    }
                    else
                    {
                        if (!folderList.Any(folder => folder.Name == newFolderName))
                        {
                            Folder newFolder = new Folder(newFolderName, currentFolder);
                            folderList.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            IEnumerable<Folder> foundFolders = currentFolder!.SubFolders.Where(folder => folder.Name == newFolderName);
                            if (foundFolders.Count() < 1)
                            {
                                string exceptionMessage = $"Line {i}: Specified folder {newFolderName} not found in directory {currentFolder.Name}: Folder only has following subfolders:";
                                foreach (Folder folder in currentFolder!.SubFolders)
                                {
                                    exceptionMessage += $"\n{folder.Name}";
                                }
                                throw new Exception(exceptionMessage);
                            }
                            else
                            {
                                currentFolder = foundFolders.First();
                            }
                        }
                    }
                }
                else if (line.StartsWith("$ ls")) { }
                else if (line.StartsWith("dir "))
                {
                    string newFolderName = line.Replace("dir ", "");
                    Folder newFolder = new Folder(newFolderName, currentFolder);
                    folderList.Add(newFolder);
                    currentFolder!.SubFolders.Add(newFolder);
                }
                else
                {
                    MatchCollection matches = Regex.Matches(line, @"\d+");
                    int fileSize = int.Parse(matches[0].Value);
                    Folder current = currentFolder!;
                    current.Size += fileSize;

                    while (current.Parent != null)
                    {
                        current = current.Parent;
                        current.Size += fileSize;
                    }
                }
            }

            // foreach (Folder folder in folderList)
            // {
            //     Console.WriteLine($"{folder.Name}: {folder.Size}");
            // }

            return folderList.Where(folder => folder.Size <= 100000).Sum(folder => folder.Size);
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');
            List<Folder> folderList = new();
            Folder? currentFolder = null;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("$ cd "))
                {
                    string newFolderName = line.Replace("$ cd ", "");
                    if (newFolderName == "..")
                    {
                        currentFolder = currentFolder!.Parent;
                    }
                    else
                    {
                        if (!folderList.Any(folder => folder.Name == newFolderName))
                        {
                            Folder newFolder = new Folder(newFolderName, currentFolder);
                            folderList.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            IEnumerable<Folder> foundFolders = currentFolder!.SubFolders.Where(folder => folder.Name == newFolderName);
                            if (foundFolders.Count() < 1)
                            {
                                string exceptionMessage = $"Line {i}: Specified folder {newFolderName} not found in directory {currentFolder.Name}: Folder only has following subfolders:";
                                foreach (Folder folder in currentFolder!.SubFolders)
                                {
                                    exceptionMessage += $"\n{folder.Name}";
                                }
                                throw new Exception(exceptionMessage);
                            }
                            else
                            {
                                currentFolder = foundFolders.First();
                            }
                        }
                    }
                }
                else if (line.StartsWith("$ ls")) { }
                else if (line.StartsWith("dir "))
                {
                    string newFolderName = line.Replace("dir ", "");
                    Folder newFolder = new Folder(newFolderName, currentFolder);
                    folderList.Add(newFolder);
                    currentFolder!.SubFolders.Add(newFolder);
                }
                else
                {
                    MatchCollection matches = Regex.Matches(line, @"\d+");
                    int fileSize = int.Parse(matches[0].Value);
                    Folder current = currentFolder!;
                    current.Size += fileSize;

                    while (current.Parent != null)
                    {
                        current = current.Parent;
                        current.Size += fileSize;
                    }
                }
            }

            const int TotalAvailableSpace = 70000000;
            const int TotalRequiredSpace = 30000000;
            int usedSpace = folderList.Where(folder => folder.Name == "/").First().Size;
            int freeSpace = TotalAvailableSpace - usedSpace;
            int spaceToFreeUp = TotalRequiredSpace - freeSpace;

            Folder folderToDelete = folderList.Where(folder => folder.Size >= spaceToFreeUp).MinBy(folder => folder.Size)!;

            return folderToDelete.Size;
        }
    }
}
