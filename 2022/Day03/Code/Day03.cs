namespace Year2022
{
    public class Day03 : IDay
    {
        public object Sol1(string input)
        {
            string lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int total = 0;
            string[] lines = input.Split('\n');

            foreach (string line in lines)
            {
                string firstHalf = line.Substring(0, line.Length / 2);
                string lastHalf = line.Substring((line.Length / 2));

                char commonCharacter = default;
                foreach (char chr in firstHalf)
                {
                    if (lastHalf.IndexOf(chr) != -1)
                    {
                        commonCharacter = chr;
                        break;
                    }
                }

                if (commonCharacter == default) throw new Exception("No commonCharacter found");
                total += lookup.IndexOf(commonCharacter) + 1;
            }

            return total;
        }

        public object Sol2(string input)
        {
            const int groupSize = 3;
            string lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int total = 0;
            string[] lines = input.Split('\n');
            string[][] groups = new string[(int)Math.Ceiling(lines.Length / (float)groupSize)][];
            for (int i = 0; i < lines.Length; i += groupSize)
            {
                string[] group = new string[groupSize];
                for (int j = 0; j < groupSize; j++)
                {
                    if (i + j % groupSize >= lines.Length) break;
                    group[j] = lines[i + j % groupSize];
                }
                groups[i / groupSize] = group;
            }

            foreach (string[] group in groups)
            {
                // foreach (String elf in group.Where(str => str != null))
                // {
                //     Console.WriteLine(elf);
                // }

                char commonCharacter = default;

                foreach (char chr in group[0].Replace("\n", "").Replace("\r", ""))
                {
                    if (!StringsContain(group.Where(str => str != null).Skip(1).ToArray(), chr)) continue;
                    commonCharacter = chr;
                    break;
                }

                if (commonCharacter == default)
                {
                    // Console.WriteLine("No common character found");
                    // Console.WriteLine();
                    continue;
                }
                // Console.WriteLine(commonCharacter);
                // Console.WriteLine();
                total += lookup.IndexOf(commonCharacter) + 1;
            }

            bool StringsContain(string[] strings, char chr)
            {
                foreach (string str in strings)
                {
                    if (!str.Contains(chr)) return false;
                }

                return true;
            }

            return total;
        }
    }
}
