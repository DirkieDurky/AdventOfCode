using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using HelperClasses;

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
                HashSet<string> GetPossibilities(string springs, int[] rule, HashSet<string> foundPossibilities)
                {
                    if (IsPossible(springs, rule)) foundPossibilities.Add(springs);
                    int firstUnknownIndex = springs.IndexOf('?');
                    if (firstUnknownIndex == -1) return foundPossibilities;

                    List<int> ruleList = new(rule);

                    int springIndex = 0;
                    int damagedSpringAmountLeft = 0;
                    while (springIndex < springs.Length)
                    {
                        char c = springs[springIndex];
                        if (c == '?') break;
                        switch (c)
                        {
                            case '#':
                                if (damagedSpringAmountLeft == 0 && ruleList.Count > 0)
                                {
                                    damagedSpringAmountLeft = ruleList[0];
                                    ruleList.RemoveAt(0);
                                }
                                damagedSpringAmountLeft--;
                                break;
                            case '.':
                                if (damagedSpringAmountLeft > 0) return foundPossibilities;
                                break;
                        }
                        springIndex++;
                    }

                    //Finish possibly unfinished streak of damaged springs (Like in #?.# 2,1 for example)
                    //as opposed to a new streak of damaged springs (Like ??.# 2,1 or .?.# 1,1)
                    if (damagedSpringAmountLeft > 0)
                    {
                        StringBuilder newString = new(springs);
                        springs.Remove(springIndex, damagedSpringAmountLeft);
                        springs.Insert(springIndex, new string('#', damagedSpringAmountLeft));
                    }

                    foreach (int dotIndex in HelperFunctions.AllIndexesOf(springs, "?"))
                    {
                        StringBuilder newString = new(springs);
                        newString.Remove(dotIndex, rule[0]);
                        newString.Insert(dotIndex, '.');
                        foundPossibilities.UnionWith(GetPossibilities(newString.ToString(), rule, foundPossibilities));
                    }

                    // List<int> placementPossibilities = HelperFunctions.AllIndexesOf(springs, new string('?', rule[0]));
                    List<int> placementPossibilities = Regex.Matches(springs, $"\\?(?=([?#]{{{rule[0] - 1}}}))").Select(x => x.Index).ToList();
                    if (placementPossibilities.Count == 0) return foundPossibilities;

                    foreach (int placementIndex in placementPossibilities)
                    {
                        StringBuilder newString = new(springs);
                        //Put damaged springs in a position to try
                        // if (placementIndex + rule[0] >= springs.Length)
                        // {
                        newString.Remove(placementIndex, rule[0]);
                        newString.Insert(placementIndex, new string('#', rule[0]));
                        // }
                        // else
                        // {
                        //     newString.Remove(placementIndex, rule[0] + 1);
                        //     newString.Insert(placementIndex, new string('#', rule[0]) + '.');
                        // }
                        //Replace all unknown springs before the index with working springs
                        if (placementIndex > 0)
                            newString.Replace('?', '.', 0, placementIndex);
                        foundPossibilities.UnionWith(GetPossibilities(newString.ToString(), rule, foundPossibilities));
                    }

                    return foundPossibilities;
                }

                HashSet<string> possibilities = GetPossibilities(lines[i].Springs, lines[i].Rule, new());
                Console.WriteLine(possibilities.Count);
                count += possibilities.Count;
            }

            return count;
        }

        public object Sol2(string input)
        {


            return "result";
        }
    }
}
