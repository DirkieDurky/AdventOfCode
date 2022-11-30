using System.Text;

namespace Year2021
{
    public class Day14 : IDay
    {
        public Object Sol1(String input)
        {
            String[] split = input.Split("\n\n");
            String startString = split[0];
            Dictionary<String, Char> table = split[1].Split('\n').Select(x => x.Split(" -> "))
                .ToDictionary(split => split[0], split => split[1][0]);

            String ExecuteStep(String inputString)
            {
                String output = inputString;
                Int32 lettersAddedCount = 0;
                for (Int32 i = 0; i < inputString.Length - 1; i++)
                {
                    foreach (KeyValuePair<String, Char> element in table)
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

            for (Int32 i = 0; i < 10; i++)
            {
                startString = ExecuteStep(startString);
                Console.WriteLine(i + 1);
            }

            Dictionary<char, int> result = startString.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            Int32 mostCommon = result.Where(x => x.Value == result.Values.Max()).Select(x => x.Value).First();
            Int32 leastCommon = result.Where(x => x.Value == result.Values.Min()).Select(x => x.Value).First();

            return mostCommon - leastCommon;
        }

        public Object Sol2(String input)
        {
            String[] split = input.Split("\n\n");

            String startString = split[0];

            String[] lines = split[1].Split('\n');
            String[][] rules = lines.Select(x => x.Split(" -> ")).ToArray();
            Char[] uniqueChars = Unique(rules.Select(x=>x[1][0]).ToArray());

            Dictionary<String, Char> table = rules.ToDictionary(split => split[0], split => split[1][0]);
            Dictionary<String, Int64> combinationCount = rules.ToDictionary(split => split[0], _ => 0L);
            Dictionary<Char, Int64> charCount = uniqueChars.ToDictionary(chars => chars, _ => 0L);

            for (Int32 i = 0; i < startString.Length - 1; i++)
            {
                combinationCount[startString[i].ToString() + startString[i + 1].ToString()]++;
            }

            foreach (char c in startString)
            {
                charCount[c]++;
            }

            for (Int32 i = 0; i < 40; i++)
            {
                Console.WriteLine(i + 1);
                combinationCount = ExecuteStep(combinationCount);
                Console.WriteLine(charCount.Max(x => x.Value)+" "+ charCount.Min(x => x.Value));
            }


            return charCount.Max(x => x.Value) - charCount.Min(x => x.Value);

            Dictionary<String, Int64> ExecuteStep(Dictionary<String, Int64> inputCombinationCount)
            {
                Dictionary<String, Int64> outputCombinationCount = rules.ToDictionary(split => split[0], _ => 0L);
                foreach (KeyValuePair<String, Char> pair in table)
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

        Char[] Unique(Char[] array)
        {
            List<Char> output = new();
            foreach (Char item in array)
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