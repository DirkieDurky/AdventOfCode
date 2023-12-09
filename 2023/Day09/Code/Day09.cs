using System.Diagnostics.CodeAnalysis;

namespace Year2023
{
    public class Day09 : IDay
    {
        public Object Sol1(String input)
        {
            int GetPrediction(List<int> inputMap)
            {
                if (inputMap.All(x => x == 0))
                {
                    return 0;
                }
                else
                {
                    List<int> resultMap = new();

                    for (int i = 0; i < inputMap.Count - 1; i++)
                    {
                        resultMap.Add(inputMap[i + 1] - inputMap[i]);
                    }

                    return inputMap.Last() + GetPrediction(resultMap);
                }
            }

            String[] lines = input.Split('\n');
            List<int[]> histories = lines.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToList();

            int total = 0;

            foreach (int[] history in histories)
            {
                int prediction = GetPrediction(history.ToList());
                // Console.WriteLine(prediction);
                total += prediction;
            }

            return total;
        }

        public Object Sol2(String input)
        {
            int GetPrediction(List<int> inputMap)
            {
                if (inputMap.All(x => x == 0))
                {
                    return 0;
                }
                else
                {
                    List<int> resultMap = new();

                    for (int i = 0; i < inputMap.Count - 1; i++)
                    {
                        resultMap.Add(inputMap[i + 1] - inputMap[i]);
                    }

                    return inputMap.First() - GetPrediction(resultMap);
                }
            }

            String[] lines = input.Split('\n');
            List<int[]> histories = lines.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToList();

            int total = 0;

            foreach (int[] history in histories)
            {
                int prediction = GetPrediction(history.ToList());
                // Console.WriteLine(prediction);
                total += prediction;
            }

            return total;
        }
    }
}
