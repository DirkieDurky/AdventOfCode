using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Year2023
{
    public class Day06 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split("\n");
            int[] times = Regex.Replace(lines[0].Split(": ")[1].Trim(), @" +", " ").Split(" ").Select(int.Parse).ToArray();
            int[] distances = Regex.Replace(lines[1].Split(": ")[1].Trim(), @" +", " ").Split(" ").Select(int.Parse).ToArray();

            List<int> results = new();

            for (int i = 0; i < times.Length; i++)
            {
                int time = times[i];
                int distance = distances[i];
                int count = 0;

                bool beginFound = false;
                for (int j = 1; j < time; j++)
                {
                    // Console.WriteLine((time - j) * j + " " + distance);
                    if ((time - j) * j > distance)
                    {
                        count++;
                        beginFound = true;
                    }
                    else if (beginFound)
                    {
                        break;
                    }
                }
                results.Add(count);
            }
            // Console.WriteLine();

            // foreach (int result in results)
            // {
            //     Console.WriteLine(result);
            // }

            return results.Aggregate(1, (a, b) => a * b);
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split("\n");
            long time = long.Parse(Regex.Replace(lines[0].Split(": ")[1].Trim(), @" +", ""));
            long distance = long.Parse(Regex.Replace(lines[1].Split(": ")[1].Trim(), @" +", ""));

            List<int> results = new();

            int count = 0;

            bool beginFound = false;
            for (long j = 1; j < time; j++)
            {
                // Console.WriteLine((time - j) * j + " " + distance);
                if ((time - j) * j > distance)
                {
                    count++;
                    beginFound = true;
                }
                else if (beginFound)
                {
                    break;
                }
            }
            results.Add(count);
            // Console.WriteLine();

            // foreach (int result in results)
            // {
            //     Console.WriteLine(result);
            // }

            return results.Aggregate(1, (a, b) => a * b);
        }
    }
}
