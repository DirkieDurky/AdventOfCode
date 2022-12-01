namespace Year2022
{
    public class Day01 : IDay
    {
        public Object Sol1(String input)
        {
            String[] elves = input.Split("\n\n");
            List<Int32> totalElveCaloryCount = new List<Int32>();

            foreach (String elve in elves)
            {
                Int32[] caloryCount = elve.Split("\n").Select(i => Int32.Parse(i)).ToArray();
                totalElveCaloryCount.Add(caloryCount.Sum());
            }

            return totalElveCaloryCount.Max();
        }

        public Object Sol2(String input)
        {

            String[] elves = input.Split("\n\n");
            List<Int32> totalElveCaloryCount = new List<Int32>();

            foreach (String elve in elves)
            {
                Int32[] caloryCount = elve.Split("\n").Select(i => Int32.Parse(i)).ToArray();
                totalElveCaloryCount.Add(caloryCount.Sum());
            }

            totalElveCaloryCount.Sort();
            totalElveCaloryCount.Reverse();

            return totalElveCaloryCount[0] + totalElveCaloryCount[1] + totalElveCaloryCount[2];
        }
    }
}