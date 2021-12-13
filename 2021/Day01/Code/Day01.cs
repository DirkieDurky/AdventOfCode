namespace Year2021
{
    public class Day01 : IDay
    {
        public Object Sol1(String input)
        {
            Int32[] lines = input.Split('\n').Select(i => Int32.Parse(i)).ToArray();
            Int32 l = Int32.MaxValue;
            Int32 count = 0;
            foreach (Int32 line in lines)
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
            Int32[] lines = input.Split('\n').Select(i => Int32.Parse(i)).ToArray();
            Int32[,] lines2 = new Int32[lines.Count(), 3];
            for (Int32 i = 0; i < lines.Count() - 1; i++)
            {
                lines[i] = lines.Skip(i).Take(3).ToArray().Sum();
            }
            Int32 l = Int32.MaxValue;
            Int32 count = 0;
            foreach (Int32 line in lines)
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