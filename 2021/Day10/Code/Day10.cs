using System.Runtime.Serialization.Formatters;
using System.Xml.Schema;

namespace Year2021
{
    public class Day10 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n').Select(x => x.Trim()).ToArray();
            int total = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int range = 1;
                while (true)
                {
                    bool charFound = false;
                    for (int j = range; j < lines[i].Length; j++)
                    {
                        char openingBracket = lines[i][j - range];
                        char c = lines[i][j];
                        if (openingBracket == '(' && c == ')' ||
                            openingBracket == '[' && c == ']' ||
                            openingBracket == '{' && c == '}' ||
                            openingBracket == '<' && c == '>')
                        {
                            charFound = true;
                            lines[i] = lines[i].Remove(j, 1);
                            lines[i] = lines[i].Remove(j - range, 1);
                        }
                    }

                    if (!charFound)
                    {
                        //if (range < lines[i].Length - 1)
                        //{
                        //    range++;
                        //}
                        //else
                        {
                            break;
                        }
                    }
                }

                foreach (char c in lines[i])
                {
                    if (c == ')')
                    {
                        total += 3;
                        break;
                    }
                    if (c == ']')
                    {
                        total += 57;
                        break;
                    }
                    if (c == '}')
                    {
                        total += 1197;
                        break;
                    }
                    if (c == '>')
                    {
                        total += 25137;
                        break;
                    }
                }
            }
            //foreach (string line2 in lines)
            //{
            //    Console.WriteLine(line2);
            //}

            return total;
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n').Select(x => x.Trim()).ToArray();
            List<long> scores = new();
            for (int i = 0; i < lines.Length; i++)
            {
                int range = 1;
                while (true)
                {
                    bool charFound = false;
                    for (int j = range; j < lines[i].Length; j++)
                    {
                        char openingBracket = lines[i][j - range];
                        char c = lines[i][j];
                        if (openingBracket == '(' && c == ')' ||
                            openingBracket == '[' && c == ']' ||
                            openingBracket == '{' && c == '}' ||
                            openingBracket == '<' && c == '>')
                        {
                            charFound = true;
                            lines[i] = lines[i].Remove(j, 1);
                            lines[i] = lines[i].Remove(j - range, 1);
                        }
                    }

                    if (!charFound)
                    {
                        //if (range < lines[i].Length - 1)
                        //{
                        //    range++;
                        //}
                        //else
                        {
                            break;
                        }
                    }
                }

                if (lines[i].Contains(')') || lines[i].Contains(']') || lines[i].Contains('}') || lines[i].Contains('>')) continue;
                long total = 0;
                foreach (char c in lines[i].Reverse())
                {
                    if (c == '(')
                    {
                        total = total * 5 + 1;
                    }
                    else if (c == '[')
                    {
                        total = total * 5 + 2;
                    }
                    else if (c == '{')
                    {
                        total = total * 5 + 3;
                    }
                    else if (c == '<')
                    {
                        total = total * 5 + 4;
                    }
                }

                scores.Add(total);
            }
            //foreach (string line2 in lines)
            //{
            //    Console.WriteLine(line2);
            //}
            scores.Sort();
            //foreach (long score in scores)
            //{
            //    Console.WriteLine(score);
            //}

            return scores[scores.Count / 2];
        }
    }
}
