using System.Text.RegularExpressions;

namespace Year2023
{
    public class Day01 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split("\n");
            int sum = 0;

            Regex regex = new Regex(@"\d");
            foreach (String line in lines)
            {
                if (line == "") continue;
                MatchCollection matches = regex.Matches(line);
                sum += int.Parse(matches[0].ToString() + matches[matches.Count - 1].ToString());
            }

            return sum;
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split("\n");
            int sum = 0;

            foreach (String line in lines)
            {
                if (line == "") continue;

                List<String> matches = new();

                List<(String, int)> foundNumbers = new();

                foreach (String number in new String[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", })
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

                Dictionary<String, int> numbers = new(){
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

                String[] firstAndLast = { matches[0].ToString(), matches[matches.Count - 1].ToString() };

                String result = "";

                foreach (String item in firstAndLast)
                {
                    if (Char.IsNumber(item[0]))
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
