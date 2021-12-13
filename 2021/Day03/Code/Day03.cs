namespace Year2021
{
    public class Day03 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split('\n');
            String output = "";
            for (Int32 i = 0; i < lines[0].Length - 1;i++)
            {
                Int32 zeroCount = 0;
                Int32 oneCount = 0;
                foreach (String line in lines)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                output += zeroCount > oneCount ? 0 : 1;
            }

            String epsilonOutput = "";
            
            foreach (Char c in output)
            {
                epsilonOutput += c == '0' ? "1" : "0";
            }
            
            Int32 gamma = Convert.ToInt32(output, 2);
            Int32 epsilon = Convert.ToInt32(epsilonOutput, 2);

            return gamma * epsilon;
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split('\n');

            String[] ogr = lines;

            for (Int32 i=0;ogr.Length > 1;i++)
            {
                Int32 zeroCount = 0;
                Int32 oneCount = 0;
                foreach (String line in ogr)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                Int32 mostCommon = zeroCount > oneCount ? 0 : 1;

                String[] tmp = ogr.Where(x => x[i]-48 == mostCommon).ToArray();
                if (tmp.Length > 0)
                {
                    ogr = tmp;
                }
            }

            String[] co2 = lines;

            for (Int32 i = 0; co2.Length > 1; i++)
            {
                Int32 zeroCount = 0;
                Int32 oneCount = 0;
                foreach (String line in co2)
                {
                    if (line[i] == '0') zeroCount++;
                    else if (line[i] == '1') oneCount++;
                }

                Int32 leastCommon = zeroCount > oneCount ? 1 : 0;

                String[] tmp = co2.Where(x => x[i] - 48 == leastCommon).ToArray();
                if (tmp.Length > 0)
                {
                    co2 = tmp;
                }
            }
            
            return Convert.ToInt32(ogr[0].Trim(), 2) * Convert.ToInt32(co2[0].Trim(), 2);
        }
    }
}