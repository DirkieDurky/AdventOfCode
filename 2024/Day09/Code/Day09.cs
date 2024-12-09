using System.Numerics;
using System.Text;

namespace Year2024
{
    public class Day09 : IDay
    {
        public object Sol1(string input)
        {
            List<int> decompressedString = new();

            int id = 0;
            bool data = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (data)
                {
                    for (int j = 0; j < (input[i] - '0'); j++)
                    {
                        decompressedString.Add(id);
                    }
                    id++;
                }
                else
                {
                    for (int j = 0; j < (input[i] - '0'); j++)
                    {
                        decompressedString.Add(-1);
                    }
                }

                data = !data;
            }

            //for (int i=0;i< decompressedString.Count; i++)
            //{
            //    if (decompressedString[i] == -1) Console.Write('.');
            //    else Console.Write(decompressedString[i]);
            //}
            //Console.WriteLine();

            decompressedString = Compact(decompressedString);

            for (int i = 0; i < decompressedString.Count; i++)
            {
                if (decompressedString[i] == -1) Console.Write('.');
                else Console.Write(decompressedString[i]);
            }
            Console.WriteLine();

            BigInteger sum = 0;

            for (int i = 0; i < decompressedString.Count; i++)
            {
                if (decompressedString[i] < 0) continue;
                sum += i * decompressedString[i];
            }

            return sum;
        }

        private List<int> Compact(List<int> decompressedString)
        {
            int firstEmptyIndex = 0;
            for (int i = decompressedString.Count - 1; i >= 0; i--)
            {
                while (decompressedString[firstEmptyIndex] > -1)
                {
                    firstEmptyIndex++;
                    if (firstEmptyIndex > i) return decompressedString;
                }
                decompressedString[firstEmptyIndex] = decompressedString[i];
                decompressedString[i] = -1;

                //for (int j = 0; j < decompressedString.Count; j++)
                //{
                //    if (decompressedString[j] == -1) Console.Write('.');
                //    else Console.Write(decompressedString[j]);
                //}
                //Console.WriteLine();
            }

            return decompressedString;
        }

        public object Sol2(string input)
        {
            List<File> files = new();

            for (int i = 0; i < input.Length; i++)
            {
                files.Add(new File(i % 2 == 0 ? i / 2 : -1, input[i] - 48));
            }

            //PrintFiles(files);
            files = Compact2(files);
            //PrintFiles(files);

            List<int> idList = new();

            foreach (File file in files)
            {
                for (int i=0;i<file.Size;i++)
                {
                    idList.Add(file.Id);
                }
            }

            BigInteger sum = 0;

            for (int i = 0; i < idList.Count; i++)
            {
                if (idList[i] < 0) continue;
                sum += i * idList[i];
            }

            return sum;
        }

        private List<File> Compact2(List<File> files)
        {
            for (int i = files.Count - 1; i >= 0; i--)
            {
                File currentFile = files[i];
                if (currentFile.Id < 0) continue;

                var foundFile = files.FirstOrDefault(x => x.Id < 0 && x.Size >= currentFile.Size);
                //Size is only 0 if FirstOrDefault found no results
                if (foundFile.Size == 0) continue;
                File fileToReplace = foundFile;
                int fileToReplaceIndex = files.IndexOf(fileToReplace);

                if (fileToReplaceIndex > i) continue;

                File currentFileCopy = currentFile;
                files[i] = new File(-1, files[i].Size);

                if (fileToReplace.Size == currentFile.Size)
                {
                    files[fileToReplaceIndex] = currentFileCopy;
                }
                else
                {
                    files.Insert(fileToReplaceIndex + 1, new File(-1, fileToReplace.Size - currentFileCopy.Size));
                    i++;
                    files[fileToReplaceIndex] = currentFileCopy;
                }

                //PrintFiles(files);
            }

            return files;
        }

        private void PrintFiles(List<File> files)
        {
            foreach (File file in files)
            {
                if (file.Id < 0)
                {
                    Console.Write(new String('.', file.Size));
                }
                else
                {
                    Console.Write(new String(file.Id.ToString()[0], file.Size));
                }
            }
            Console.WriteLine();
        }

        private struct File
        {
            public int Id;
            public int Size;

            public File(int id, int size)
            {
                Id = id;
                Size = size;
            }
        }

        //public object Sol2(string input)
        //{
        //    List<int> decompressedString = new();

        //    int id = 0;
        //    bool data = true;
        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        if (data)
        //        {
        //            for (int j = 0; j < (input[i] - '0'); j++)
        //            {
        //                decompressedString.Add(id);
        //            }
        //            id++;
        //        }
        //        else
        //        {
        //            for (int j = 0; j < (input[i] - '0'); j++)
        //            {
        //                decompressedString.Add(-1);
        //            }
        //        }

        //        data = !data;
        //    }

        //    //for (int i=0;i< decompressedString.Count; i++)
        //    //{
        //    //    if (decompressedString[i] == -1) Console.Write('.');
        //    //    else Console.Write(decompressedString[i]);
        //    //}
        //    //Console.WriteLine();

        //    decompressedString = Compact2(decompressedString);

        //    for (int i = 0; i < decompressedString.Count; i++)
        //    {
        //        if (decompressedString[i] == -1) Console.Write('.');
        //        else Console.Write(decompressedString[i]);
        //    }
        //    Console.WriteLine();

        //    BigInteger sum = 0;

        //    for (int i = 0; i < decompressedString.Count; i++)
        //    {
        //        if (decompressedString[i] < 0) continue;
        //        sum += i * decompressedString[i];
        //    }

        //    return sum;
        //}

        //private List<int> Compact2(List<int> decompressedString)
        //{
        //    int firstEmptyIndex = 0;
        //    int targetSize;
        //    for (int i = decompressedString.Count - 1; i >= 0;)
        //    {
        //        if (decompressedString[i] < 0) continue;
        //        targetSize = decompressedString.Count(x => x == decompressedString[i]);
        //        int currentSize = 0;
        //        while (currentSize < targetSize)
        //        {
        //            if (decompressedString[firstEmptyIndex] > -1)
        //            {
        //                firstEmptyIndex++;
        //                currentSize = 0;
        //                if (firstEmptyIndex > i) return decompressedString;
        //            }
        //            else
        //            {
        //                currentSize++;
        //            }
        //        }
        //        decompressedString[firstEmptyIndex] = decompressedString[i];
        //        decompressedString[i] = -1;

        //        //for (int j = 0; j < decompressedString.Count; j++)
        //        //{
        //        //    if (decompressedString[j] == -1) Console.Write('.');
        //        //    else Console.Write(decompressedString[j]);
        //        //}
        //        //Console.WriteLine();
        //        i -= targetSize;
        //    }

        //    return decompressedString;
        //}
    }
}