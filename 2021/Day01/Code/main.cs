namespace Year2021
{
    public class Day01 : IDay
    {
        public Object Sol1(String input)
        {
            int[] lines = input.Split('\n').Select(i => Int32.Parse(i)).ToArray();
            int l = int.MaxValue;
            int count = 0;
            foreach (int line in lines)
            {
                if (line > l)
                {
                    count++;
                }
                l = line;
            }
            return count;
        }

        public Object Sol2(String input)
        {
            int[] lines = input.Split('\n').Select(i => Int32.Parse(i)).ToArray();
            int[,] lines2 = new int[lines.Count(), 3];
            for (int i = 0; i < lines.Count() - 1; i++)
            {
                lines[i] = lines.Skip(i).Take(3).ToArray().Sum();
            }
            int l = int.MaxValue;
            int count = 0;
            foreach (int line in lines)
            {
                if (line > l)
                {
                    count++;
                }
                l = line;
            }
            return count;
        }
    }
}