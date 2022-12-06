namespace Year2022
{
    public class Day06 : IDay
    {
        public Object Sol1(String input)
        {
            for (Int32 i = 4; i < input.Length; i++)
            {
                String substring = input.Substring(i - 4, 4);
                if (substring.Length == substring.Distinct().Count()) return i;
            }

            return "None found";
        }

        public Object Sol2(String input)
        {
            for (Int32 i = 14; i < input.Length; i++)
            {
                String substring = input.Substring(i - 14, 14);
                if (substring.Length == substring.Distinct().Count()) return i;
            }

            return "None found";
        }
    }
}