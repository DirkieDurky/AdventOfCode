using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Year2024
{
    public class Day03 : IDay
    {
        public object Sol1(string input)
        {
            int sum = 0;
            MatchCollection matches = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");

			foreach (Match match in matches)
            {
                String numbersStr = match.Value.Substring(4, match.Value.Length - 5);
                List<int> numbers = numbersStr.Split(",").Select(int.Parse).ToList();

                sum += numbers[0] * numbers[1];
            }

            return sum;
        }

		public object Sol2(string input)
		{
			input = Regex.Replace(input, "don't\\(\\)(.|\\n)*?(do\\(\\)|$)", "");
			int sum = 0;
			MatchCollection matches = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");

			foreach (Match match in matches)
			{
				String numbersStr = match.Value.Substring(4, match.Value.Length - 5);
				List<int> numbers = numbersStr.Split(",").Select(int.Parse).ToList();

				sum += numbers[0] * numbers[1];
			}

			return sum;
		}
    }
}