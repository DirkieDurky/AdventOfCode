namespace Year2022
{
    public class Day05 : IDay
    {
        public Object Sol1(String input)
        {
            String[] split = input.Split("\n\n");
            String[] crateLines = split[0].Split('\n');

            Int32 stackAmount = crateLines[^1].Trim().Split().Select(Int32.Parse).Last();
            crateLines = crateLines.Take(crateLines.Count() - 1).ToArray();
            List<String>[] stacks = new List<String>[stackAmount];
            for (Int32 i=0;i<stackAmount;i++){
                stacks[i] = new List<String>();
            }
            
            foreach (String crateLine in crateLines.Reverse())
            {
                String[] crates = new String[(int)Math.Ceiling(crateLine.Length/4f)];
                for (Int32 i=0;i<crateLine.Length;i+=4){
                    crates[i/4] = crateLine.Substring(i,3);
                }
                for (Int32 i=0;i<crates.Length;i++){
                    if (crates[i] != "   ") stacks[i].Add(crates[i]);
                }
            }

            for (Int32 i = 0; i < stacks.Length; i++)
            {
                List<String> stack = stacks[i];
                Console.WriteLine(i + 1);
                foreach (String item in stack)
                {
                    Console.WriteLine(item);
                }
            }

            String[] instructions = split[1].Split('\n');

            foreach (String instruction in instructions)
            {
                split = instruction.Split();
                Int32 amount = Int32.Parse(split[1]);
                Int32 from = Int32.Parse(split[3])-1;
                Int32 to = Int32.Parse(split[5])-1;

                List<String> temp = stacks[from].TakeLast(amount).ToList();
                for (Int32 i=0;i<amount;i++){
                    stacks[from].RemoveAt(stacks[from].Count -1);
                }
                stacks[to].AddRange(temp);
            }

            for (Int32 i = 0; i < stacks.Length; i++)
            {
                List<String> stack = stacks[i];
                Console.WriteLine(i + 1);
                foreach (String item in stack)
                {
                    Console.WriteLine(item);
                }
            }

            String result = "";

            foreach (List<String> stack in stacks){
                result += stack.Last().Replace("[","").Replace("]","");
            }

            return result;
        }

        public Object Sol2(String input)
        {      


            return "";
        }
    }
}