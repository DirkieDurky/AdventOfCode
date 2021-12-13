namespace Year2021
{
    public class Day07 : IDay
    {
        public Object Sol1(String input)
        {
            Int32[] toIntArray = input.Split(',').Select(Int32.Parse).ToArray();
            Int32 lowest = Int32.MaxValue;
            foreach (Int32 crab in toIntArray)
            {
                Int32 total = 0;
                foreach (Int32 crab2 in toIntArray)
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

        public Object Sol2(String input)
        {
            Int32[] crabPositions = input.Split(',').Select(Int32.Parse).ToArray();
            Int32 lowest = Int32.MaxValue;
            for (Int32 j = crabPositions.Min(); j < crabPositions.Max() + 1; j++)
            {
                Int32 total = 0;
                foreach (Int32 crabPos in crabPositions)
                {
                    Int32 diff = Math.Abs(crabPos - j);
                    Int32 subtotal = 0;
                    for (Int32 i = diff; i > 0; i--)
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