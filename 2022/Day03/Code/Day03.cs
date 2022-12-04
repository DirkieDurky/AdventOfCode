namespace Year2022
{
    public class Day03 : IDay
    {
        public Object Sol1(String input)
        {
            String lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Int32 total = 0;
            String[] lines = input.Split('\n');

            foreach (String line in lines)
            {
                String firstHalf = line.Substring(0, line.Length / 2);
                String lastHalf = line.Substring((line.Length / 2));

                Char commonCharacter = default;
                foreach (Char chr in firstHalf)
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

        public Object Sol2(String input)
        {
            const Int32 groupSize = 3;
            String lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Int32 total = 0;
            String[] lines = input.Split('\n');
            String[][] groups = new String[(int)Math.Ceiling(lines.Length / (float)groupSize)][];
            for (Int32 i = 0; i < lines.Length; i += groupSize)
            {
                String[] group = new String[groupSize];
                for (Int32 j = 0; j < groupSize; j++)
                {
                    if (i + j % groupSize >= lines.Length) break;
                    group[j] = lines[i + j % groupSize];
                }
                groups[i / groupSize] = group;
            }

            foreach (String[] group in groups)
            {
                // foreach (String elf in group.Where(str => str != null))
                // {
                //     Console.WriteLine(elf);
                // }

                Char commonCharacter = default;

                foreach (Char chr in group[0].Replace("\n", "").Replace("\r", ""))
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

            Boolean StringsContain(String[] strings, Char chr)
            {
                foreach (String str in strings)
                {
                    if (!str.Contains(chr)) return false;
                }

                return true;
            }

            return total;
        }
    }
}