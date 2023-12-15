namespace Year2022
{
    public class Day01 : IDay
    {
        public object Sol1(string input)
        {
            string[] elves = input.Split("\n\n");
            List<int> totalElveCaloryCount = new List<int>();

            foreach (string elf in elves)
            {
                int[] caloryCount = elf.Split("\n").Select(i => int.Parse(i)).ToArray();
                totalElveCaloryCount.Add(caloryCount.Sum());
            }

            return totalElveCaloryCount.Max();
        }

        public object Sol2(string input)
        {

            string[] elves = input.Split("\n\n");
            List<int> totalElveCaloryCount = new List<int>();

            foreach (string elf in elves)
            {
                int[] caloryCount = elf.Split("\n").Select(i => int.Parse(i)).ToArray();
                totalElveCaloryCount.Add(caloryCount.Sum());
            }

            totalElveCaloryCount.Sort();
            totalElveCaloryCount.Reverse();

            return totalElveCaloryCount[0] + totalElveCaloryCount[1] + totalElveCaloryCount[2];
        }
    }
}
