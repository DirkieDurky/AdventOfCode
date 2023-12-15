using System.Text.RegularExpressions;

namespace Year2023
{
    public class Day01 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split("\n");
            int sum = 0;

            Regex regex = new Regex(@"\d");
            foreach (string line in lines)
            {
                if (line == "") continue;
                MatchCollection matches = regex.Matches(line);
                sum += int.Parse(matches[0].ToString() + matches[matches.Count - 1].ToString());
            }

            return sum;
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split("\n");
            int sum = 0;

            foreach (string line in lines)
            {
                if (line == "") continue;

                List<string> matches = new();

                List<(string, int)> foundNumbers = new();

                foreach (string number in new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", })
                {
                    int index = 0;
                    while (index != -1)
                    {
                        index = line.IndexOf(number, index);
                        if (index != -1)
                        {
                            foundNumbers.Add((number, index));
                            index += number.Length;
                        }
                    }
                }

                matches.AddRange(foundNumbers.OrderBy(x => x.Item2).Select(x => x.Item1));

                Dictionary<string, int> numbers = new(){
                    {"one", 1},
                    {"two", 2},
                    {"three", 3},
                    {"four", 4},
                    {"five", 5},
                    {"six", 6},
                    {"seven", 7},
                    {"eight", 8},
                    {"nine", 9},
                };

                string[] firstAndLast = { matches[0].ToString(), matches[matches.Count - 1].ToString() };

                string result = "";

                foreach (string item in firstAndLast)
                {
                    if (char.IsNumber(item[0]))
                    {
                        result += item;
                    }
                    else
                    {
                        result += numbers[item].ToString();
                    }
                }

                sum += int.Parse(result);
            }

            return sum;
        }
    }
}
