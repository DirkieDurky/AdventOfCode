namespace Year2023
{
    public class Day08 : IDay
    {
        public object Sol1(string input)
        {
            string[] split = input.Split("\n\n");
            string directions = split[0];
            string[] networkNodeStrings = split[1].Split("\n");

            Dictionary<string, (string, string)> networkNodes = new();

            foreach (string networkNodeString in networkNodeStrings)
            {
                split = networkNodeString.Split(" = ");
                string[] split2 = split[1].Split(", ");
                networkNodes.Add(split[0], (split2[0].Replace("(", ""), split2[1].Replace(")", "")));
            }

            string currentNode = "AAA";
            int stepCount = 0;

            for (int i = 0; currentNode != "ZZZ"; i = (i + 1) % directions.Length)
            {
                currentNode = directions[i] == 'L' ? networkNodes[currentNode].Item1 : networkNodes[currentNode].Item2;
                stepCount++;
            }

            return stepCount;
        }

        public object Sol2(string input)
        {
            string[] split = input.Split("\n\n");
            string directions = split[0];
            string[] networkNodeStrings = split[1].Split("\n");

            Dictionary<string, (string, string)> networkNodes = new();

            foreach (string networkNodeString in networkNodeStrings)
            {
                split = networkNodeString.Split(" = ");
                string[] split2 = split[1].Split(", ");
                networkNodes.Add(split[0], (split2[0].Replace("(", ""), split2[1].Replace(")", "")));
            }

            List<string> currentNodes = new();
            foreach (string networkNode in networkNodes.Keys)
            {
                if (networkNode[networkNode.Length - 1] == 'A') currentNodes.Add(networkNode);
            }

            List<long> stepCounts = new();

            //Find amount of steps until the first node that ends with a z
            for (int i = 0; i < currentNodes.Count; i++)
            {
                bool started = false;

                long stepCount = 0;
                for (int j = 0; currentNodes[i][currentNodes[i].Length - 1] != 'Z' || !started; j = (j + 1) % directions.Length)
                {
                    started = true;
                    currentNodes[i] = directions[j] == 'L' ? networkNodes[currentNodes[i]].Item1 : networkNodes[currentNodes[i]].Item2;
                    stepCount++;
                }
                stepCounts.Add(stepCount);
            }

            //Greatest common divisor
            static long gcd(long a, long b)
            {
                if (a % b != 0)
                    return gcd(b, a % b);
                else
                    return b;
            }

            // Returns lowest (or least) common multiple of the numbers in the list
            static long lcm(List<long> list)
            {
                long ans = 1;
                foreach (int i in list)
                    ans = ans * i / gcd(ans, i);
                return ans;
            }

            return lcm(stepCounts);
        }
    }
}
