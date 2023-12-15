namespace Year2022
{
    public class Day04 : IDay
    {
        public object Sol1(string input)
        {
            int total = 0;
            string[] lines = input.Split('\n');

            foreach (string line in lines)
            {
                string[] ranges = line.Split(',');

                int[] range1 = ranges[0].Split('-').Select(int.Parse).ToArray();
                int[] range2 = ranges[1].Split('-').Select(int.Parse).ToArray();

                if (range1[0] >= range2[0] && range1[1] <= range2[1])
                {
                    total++;
                    continue;
                };
                if (range1[0] <= range2[0] && range1[1] >= range2[1]) total++;
            }

            return total;
        }

        public object Sol2(string input)
        {
            int total = 0;
            string[] lines = input.Split('\n');

            foreach (string line in lines)
            {
                string[] ranges = line.Split(',');

                int[] range1 = ranges[0].Split('-').Select(int.Parse).ToArray();
                int[] range2 = ranges[1].Split('-').Select(int.Parse).ToArray();

                if ((range1[0] >= range2[0] && range1[0] <= range2[1]) ||
                    (range1[1] >= range2[0] && range1[1] <= range2[1]) ||
                    (range2[0] >= range1[0] && range2[0] <= range1[1]) ||
                    (range2[1] >= range1[0] && range2[1] <= range1[1])) total++;
            }

            return total;
        }
    }
}
