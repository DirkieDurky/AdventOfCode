using System.Text;

namespace Year2023
{
    public class Day12 : IDay
    {
        internal class Line
        {
            public string Springs;
            public int[] Rule;

            public Line(string springs, int[] rule)
            {
                Springs = springs;
                Rule = rule;
            }
        }

        internal static List<int> GetForm(string springs)
        {
            List<int> form = new();
            int currentCount = 0;

            foreach (char c in springs)
            {
                if (c == '#') { currentCount++; }
                else if (currentCount > 0) { form.Add(currentCount); currentCount = 0; }
            }
            if (currentCount > 0) form.Add(currentCount);

            return form;
        }

        internal static bool IsPossible(string possibility, int[] rule)
        {
            List<int> form = GetForm(possibility);
            return Enumerable.SequenceEqual(form, rule);
        }

        public object Sol1(string input)
        {
            string[] stringLines = input.Split('\n');
            List<Line> lines = new();

            foreach (string stringLine in stringLines)
            {
                string[] split = stringLine.Split(' ');
                lines.Add(new Line(split[0], split[1].Split(',').Select(int.Parse).ToArray()));
            }

            int count = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                int GetPossibilities(string springs, int[] rule)
                {
                    int count = 0;

                    int firstUnknownIndex = springs.IndexOf('?');
                    if (firstUnknownIndex == -1)
                    {
                        if (IsPossible(springs, rule)) count++;
                        return count;
                    }

                    StringBuilder newLine = new(springs);
                    newLine[firstUnknownIndex] = '.';
                    count += GetPossibilities(newLine.ToString(), rule);
                    newLine = new(springs);
                    newLine[firstUnknownIndex] = '#';
                    count += GetPossibilities(newLine.ToString(), rule);

                    return count;
                }
                count += GetPossibilities(lines[i].Springs, lines[i].Rule);

                // Console.WriteLine("Possibilities");
                // foreach (String possibility in possibilities)
                // {
                //     Console.WriteLine(possibility);
                // }
                // Console.WriteLine();
            }

            return count;
        }

        public object Sol2(string input)
        {


            return "result";
        }
    }
}
