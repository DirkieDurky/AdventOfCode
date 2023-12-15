namespace Year2022
{
    public class Day05 : IDay
    {
        public object Sol1(string input)
        {
            string[] split = input.Split("\n\n");
            string[] crateLines = split[0].Split('\n');

            int stackAmount = crateLines[^1].Trim().Split().Select(int.Parse).Last();
            crateLines = crateLines.Take(crateLines.Count() - 1).ToArray();
            List<string>[] stacks = new List<string>[stackAmount];
            for (int i = 0; i < stackAmount; i++)
            {
                stacks[i] = new List<string>();
            }

            foreach (string crateLine in crateLines.Reverse())
            {
                string[] crates = new string[(int)Math.Ceiling(crateLine.Length / 4f)];
                for (int i = 0; i < crateLine.Length; i += 4)
                {
                    crates[i / 4] = crateLine.Substring(i, 3);
                }
                for (int i = 0; i < crates.Length; i++)
                {
                    if (crates[i] != "   ") stacks[i].Add(crates[i]);
                }
            }

            // for (Int32 i = 0; i < stacks.Length; i++)
            // {
            //     List<String> stack = stacks[i];
            //     Console.WriteLine(i + 1);
            //     foreach (String item in stack)
            //     {
            //         Console.WriteLine(item);
            //     }
            // }

            string[] instructions = split[1].Split('\n');

            foreach (string instruction in instructions)
            {
                split = instruction.Split();
                int amount = int.Parse(split[1]);
                int from = int.Parse(split[3]) - 1;
                int to = int.Parse(split[5]) - 1;

                List<string> temp = stacks[from].TakeLast(amount).Reverse().ToList();
                for (int i = 0; i < amount; i++)
                {
                    stacks[from].RemoveAt(stacks[from].Count - 1);
                }
                stacks[to].AddRange(temp);
            }

            // for (Int32 i = 0; i < stacks.Length; i++)
            // {
            // List<String> stack = stacks[i];
            // Console.WriteLine(i + 1);
            // foreach (String item in stack)
            // {
            //     Console.WriteLine(item);
            // }
            // }

            string result = "";

            foreach (List<string> stack in stacks)
            {
                result += stack.Last().Replace("[", "").Replace("]", "");
            }

            return result;
        }

        public object Sol2(string input)
        {
            string[] split = input.Split("\n\n");
            string[] crateLines = split[0].Split('\n');

            int stackAmount = crateLines[^1].Trim().Split().Select(int.Parse).Last();
            crateLines = crateLines.Take(crateLines.Count() - 1).ToArray();
            List<string>[] stacks = new List<string>[stackAmount];
            for (int i = 0; i < stackAmount; i++)
            {
                stacks[i] = new List<string>();
            }

            foreach (string crateLine in crateLines.Reverse())
            {
                string[] crates = new string[(int)Math.Ceiling(crateLine.Length / 4f)];
                for (int i = 0; i < crateLine.Length; i += 4)
                {
                    crates[i / 4] = crateLine.Substring(i, 3);
                }
                for (int i = 0; i < crates.Length; i++)
                {
                    if (crates[i] != "   ") stacks[i].Add(crates[i]);
                }
            }

            // for (Int32 i = 0; i < stacks.Length; i++)
            // {
            //     List<String> stack = stacks[i];
            //     Console.WriteLine(i + 1);
            //     foreach (String item in stack)
            //     {
            //         Console.WriteLine(item);
            //     }
            // }

            string[] instructions = split[1].Split('\n');

            foreach (string instruction in instructions)
            {
                split = instruction.Split();
                int amount = int.Parse(split[1]);
                int from = int.Parse(split[3]) - 1;
                int to = int.Parse(split[5]) - 1;

                List<string> temp = stacks[from].TakeLast(amount).ToList();
                for (int i = 0; i < amount; i++)
                {
                    stacks[from].RemoveAt(stacks[from].Count - 1);
                }
                stacks[to].AddRange(temp);
            }

            // for (Int32 i = 0; i < stacks.Length; i++)
            // {
            // List<String> stack = stacks[i];
            // Console.WriteLine(i + 1);
            // foreach (String item in stack)
            // {
            //     Console.WriteLine(item);
            // }
            // }

            string result = "";

            foreach (List<string> stack in stacks)
            {
                result += stack.Last().Replace("[", "").Replace("]", "");
            }

            return result;
        }
    }
}
