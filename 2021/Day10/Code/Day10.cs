using System.Runtime.Serialization.Formatters;
using System.Xml.Schema;

namespace Year2021
{
    public class Day10 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split('\n').Select(x => x.Trim()).ToArray();
            Int32 total = 0;
            for (Int32 i = 0; i < lines.Length; i++)
            {
                Int32 range = 1;
                while (true)
                {
                    Boolean charFound = false;
                    for (Int32 j = range; j < lines[i].Length; j++)
                    {
                        Char openingBracket = lines[i][j - range];
                        Char c = lines[i][j];
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

                foreach (Char c in lines[i])
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

        public Object Sol2(String input)
        {
            String[] lines = input.Split('\n').Select(x => x.Trim()).ToArray();
            List<Int64> scores = new();
            for (Int32 i = 0; i < lines.Length; i++)
            {
                Int32 range = 1;
                while (true)
                {
                    Boolean charFound = false;
                    for (Int32 j = range; j < lines[i].Length; j++)
                    {
                        Char openingBracket = lines[i][j - range];
                        Char c = lines[i][j];
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
                Int64 total = 0;
                foreach (Char c in lines[i].Reverse())
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

            return scores[scores.Count/2];
        }
    }
}