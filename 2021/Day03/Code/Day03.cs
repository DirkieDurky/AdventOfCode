namespace Year2021
{
    public class Day03 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');
            string output = "";
            for (int i = 0; i < lines[0].Length - 1; i++)
            {
                int zeroCount = 0;
                int oneCount = 0;
                foreach (string line in lines)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                output += zeroCount > oneCount ? 0 : 1;
            }

            string epsilonOutput = "";

            foreach (char c in output)
            {
                epsilonOutput += c == '0' ? "1" : "0";
            }

            int gamma = Convert.ToInt32(output, 2);
            int epsilon = Convert.ToInt32(epsilonOutput, 2);

            return gamma * epsilon;
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');

            string[] ogr = lines;

            for (int i = 0; ogr.Length > 1; i++)
            {
                int zeroCount = 0;
                int oneCount = 0;
                foreach (string line in ogr)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                int mostCommon = zeroCount > oneCount ? 0 : 1;

                string[] tmp = ogr.Where(x => x[i] - 48 == mostCommon).ToArray();
                if (tmp.Length > 0)
                {
                    ogr = tmp;
                }
            }

            string[] co2 = lines;

            for (int i = 0; co2.Length > 1; i++)
            {
                int zeroCount = 0;
                int oneCount = 0;
                foreach (string line in co2)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                int leastCommon = zeroCount > oneCount ? 1 : 0;

                string[] tmp = co2.Where(x => x[i] - 48 == leastCommon).ToArray();
                if (tmp.Length > 0)
                {
                    co2 = tmp;
                }
            }

            return Convert.ToInt32(ogr[0].Trim(), 2) * Convert.ToInt32(co2[0].Trim(), 2);
        }
    }
}
