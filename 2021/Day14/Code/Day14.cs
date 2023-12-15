using System.Text;

namespace Year2021
{
    public class Day14 : IDay
    {
        public object Sol1(string input)
        {
            string[] split = input.Split("\n\n");
            string startString = split[0];
            Dictionary<string, char> table = split[1].Split('\n').Select(x => x.Split(" -> "))
                .ToDictionary(split => split[0], split => split[1][0]);

            string ExecuteStep(string inputString)
            {
                string output = inputString;
                int lettersAddedCount = 0;
                for (int i = 0; i < inputString.Length - 1; i++)
                {
                    foreach (KeyValuePair<string, char> element in table)
                    {
                        if (inputString[i] == element.Key[0] && inputString[i + 1] == element.Key[1])
                        {
                            output = output.Insert(i + 1 + lettersAddedCount, element.Value.ToString());
                            lettersAddedCount++;
                            break;
                        }
                    }
                }

                return output;
            }

            for (int i = 0; i < 10; i++)
            {
                startString = ExecuteStep(startString);
                Console.WriteLine(i + 1);
            }

            Dictionary<char, int> result = startString.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            int mostCommon = result.Where(x => x.Value == result.Values.Max()).Select(x => x.Value).First();
            int leastCommon = result.Where(x => x.Value == result.Values.Min()).Select(x => x.Value).First();

            return mostCommon - leastCommon;
        }

        public object Sol2(string input)
        {
            string[] split = input.Split("\n\n");

            string startString = split[0];

            string[] lines = split[1].Split('\n');
            string[][] rules = lines.Select(x => x.Split(" -> ")).ToArray();
            char[] uniqueChars = Unique(rules.Select(x => x[1][0]).ToArray());

            Dictionary<string, char> table = rules.ToDictionary(split => split[0], split => split[1][0]);
            Dictionary<string, long> combinationCount = rules.ToDictionary(split => split[0], _ => 0L);
            Dictionary<char, long> charCount = uniqueChars.ToDictionary(chars => chars, _ => 0L);

            for (int i = 0; i < startString.Length - 1; i++)
            {
                combinationCount[startString[i].ToString() + startString[i + 1].ToString()]++;
            }

            foreach (char c in startString)
            {
                charCount[c]++;
            }

            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine(i + 1);
                combinationCount = ExecuteStep(combinationCount);
                Console.WriteLine(charCount.Max(x => x.Value) + " " + charCount.Min(x => x.Value));
            }


            return charCount.Max(x => x.Value) - charCount.Min(x => x.Value);

            Dictionary<string, long> ExecuteStep(Dictionary<string, long> inputCombinationCount)
            {
                Dictionary<string, long> outputCombinationCount = rules.ToDictionary(split => split[0], _ => 0L);
                foreach (KeyValuePair<string, char> pair in table)
                {
                    outputCombinationCount[pair.Key[0].ToString() + pair.Value.ToString()] +=
                        inputCombinationCount[pair.Key];
                    outputCombinationCount[pair.Value.ToString() + pair.Key[1].ToString()] +=
                        inputCombinationCount[pair.Key];
                    charCount[pair.Value] += inputCombinationCount[pair.Key];
                }

                return outputCombinationCount;
            }
        }

        char[] Unique(char[] array)
        {
            List<char> output = new();
            foreach (char item in array)
            {
                if (!output.Contains(item))
                {
                    output.Add(item);
                }
            }
            return output.ToArray();
        }
    }
}
