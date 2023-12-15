namespace Year2021
{
    public class Day07 : IDay
    {
        public object Sol1(string input)
        {
            int[] toIntArray = input.Split(',').Select(int.Parse).ToArray();
            int lowest = int.MaxValue;
            foreach (int crab in toIntArray)
            {
                int total = 0;
                foreach (int crab2 in toIntArray)
                {
                    total += Math.Abs(crab2 - crab);
                }
                if (total < lowest)
                {
                    lowest = total;
                }
            }
            return lowest;
        }

        public object Sol2(string input)
        {
            int[] crabPositions = input.Split(',').Select(int.Parse).ToArray();
            int lowest = int.MaxValue;
            for (int j = crabPositions.Min(); j < crabPositions.Max() + 1; j++)
            {
                int total = 0;
                foreach (int crabPos in crabPositions)
                {
                    int diff = Math.Abs(crabPos - j);
                    int subtotal = 0;
                    for (int i = diff; i > 0; i--)
                    {
                        subtotal += i;
                    }
                    total += subtotal;
                }
                if (total < lowest)
                {
                    lowest = total;
                }
            }
            return lowest;
        }
    }
}
