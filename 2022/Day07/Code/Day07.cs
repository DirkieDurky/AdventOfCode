using System.Text.RegularExpressions;
namespace Year2022
{
    public class Day07 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split('\n');
            List<Folder> folderList = new();
            Folder? currentFolder = null;

            for (Int32 i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (line.StartsWith("$ cd "))
                {
                    String newFolderName = line.Replace("$ cd ", "");
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
                                String exceptionMessage = $"Line {i}: Specified folder {newFolderName} not found in directory {currentFolder.Name}: Folder only has following subfolders:";
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
                    String newFolderName = line.Replace("dir ", "");
                    Folder newFolder = new Folder(newFolderName, currentFolder);
                    folderList.Add(newFolder);
                    currentFolder!.SubFolders.Add(newFolder);
                }
                else
                {
                    MatchCollection matches = Regex.Matches(line, @"\d+");
                    Int32 fileSize = Int32.Parse(matches[0].Value);
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

        public Object Sol2(String input)
        {
            String[] lines = input.Split('\n');
            List<Folder> folderList = new();
            Folder? currentFolder = null;

            for (Int32 i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (line.StartsWith("$ cd "))
                {
                    String newFolderName = line.Replace("$ cd ", "");
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
                                String exceptionMessage = $"Line {i}: Specified folder {newFolderName} not found in directory {currentFolder.Name}: Folder only has following subfolders:";
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
                    String newFolderName = line.Replace("dir ", "");
                    Folder newFolder = new Folder(newFolderName, currentFolder);
                    folderList.Add(newFolder);
                    currentFolder!.SubFolders.Add(newFolder);
                }
                else
                {
                    MatchCollection matches = Regex.Matches(line, @"\d+");
                    Int32 fileSize = Int32.Parse(matches[0].Value);
                    Folder current = currentFolder!;
                    current.Size += fileSize;

                    while (current.Parent != null)
                    {
                        current = current.Parent;
                        current.Size += fileSize;
                    }
                }
            }

            const Int32 TotalAvailableSpace = 70000000;
            const Int32 TotalRequiredSpace = 30000000;
            Int32 usedSpace = folderList.Where(folder => folder.Name == "/").First().Size;
            Int32 freeSpace = TotalAvailableSpace - usedSpace;
            Int32 spaceToFreeUp = TotalRequiredSpace - freeSpace;

            Folder folderToDelete = folderList.Where(folder => folder.Size >= spaceToFreeUp).MinBy(folder => folder.Size)!;

            return folderToDelete.Size;
        }
    }
}