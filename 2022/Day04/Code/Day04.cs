namespace Year2022
{
    public class Day04 : IDay
    {
        public Object Sol1(String input)
        {
            Int32 total = 0;
            String[] lines = input.Split('\n');

            foreach (String line in lines)
            {
                String[] ranges = line.Split(',');

                Int32[] range1 = ranges[0].Split('-').Select(Int32.Parse).ToArray();
                Int32[] range2 = ranges[1].Split('-').Select(Int32.Parse).ToArray();

                if (range1[0] >= range2[0] && range1[1] <= range2[1])
                {
                    total++;
                    continue;
                };
                if (range1[0] <= range2[0] && range1[1] >= range2[1]) total++;
            }

            return total;
        }

        public Object Sol2(String input)
        {
            Int32 total = 0;
            String[] lines = input.Split('\n');

            foreach (String line in lines)
            {
                String[] ranges = line.Split(',');

                Int32[] range1 = ranges[0].Split('-').Select(Int32.Parse).ToArray();
                Int32[] range2 = ranges[1].Split('-').Select(Int32.Parse).ToArray();

                if ((range1[0] >= range2[0] && range1[0] <= range2[1]) ||
                    (range1[1] >= range2[0] && range1[1] <= range2[1]) ||
                    (range2[0] >= range1[0] && range2[0] <= range1[1]) ||
                    (range2[1] >= range1[0] && range2[1] <= range1[1])) total++;
            }

            return total;
        }
    }
}