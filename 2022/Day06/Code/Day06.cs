namespace Year2022
{
    public class Day06 : IDay
    {
        public object Sol1(string input)
        {
            for (int i = 4; i < input.Length; i++)
            {
                string substring = input.Substring(i - 4, 4);
                if (substring.Length == substring.Distinct().Count()) return i;
            }

            return "None found";
        }

        public object Sol2(string input)
        {
            for (int i = 14; i < input.Length; i++)
            {
                string substring = input.Substring(i - 14, 14);
                if (substring.Length == substring.Distinct().Count()) return i;
            }

            return "None found";
        }
    }
}
