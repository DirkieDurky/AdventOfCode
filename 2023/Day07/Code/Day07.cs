using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace Year2023
{
    public class Day07 : IDay
    {
        public int GetHandType(string hand)
        {
            Dictionary<char, int> charCounts = new();

            foreach (char c in hand)
            {
                if (!charCounts.ContainsKey(c))
                {
                    charCounts.Add(c, 1);
                }
                else
                {
                    charCounts[c]++;
                }
            }

            if (charCounts.Any(x => x.Value == 5)) return 1;
            if (charCounts.Any(x => x.Value == 4)) return 2;
            if (charCounts.Any(x => x.Value == 3) && charCounts.Any(x => x.Value == 2)) return 3;
            if (charCounts.Any(x => x.Value == 3)) return 4;
            if (charCounts.Count(x => x.Value == 2) == 2) return 5;
            if (charCounts.Any(x => x.Value == 2)) return 6;
            return 7;
        }

        public class SortHandByCards : IComparer<string>
        {
            public int Compare(string? hand1, string? hand2)
            {
                if (hand1 is null || hand2 is null || hand1.Length != hand2.Length) throw new Exception();

                for (int i = 0; i < hand1.Length; i++)
                {
                    string order = "AKQJT98765432";

                    if (order.IndexOf(hand1[i]) > order.IndexOf(hand2[i])) return -1;
                    if (order.IndexOf(hand1[i]) < order.IndexOf(hand2[i])) return 1;
                }
                return 0;
            }
        }

        public object Sol1(string input)
        {
            string[] lines = input.Split("\n");
            List<(string, int)> hands = new();

            foreach (string line in lines)
            {
                string[] split = line.Split(" ");
                hands.Add((split[0], int.Parse(split[1])));
            }

            hands = hands.OrderByDescending(x => GetHandType(x.Item1))
            .ThenBy(x => x.Item1, new SortHandByCards()).ToList();

            int sum = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                sum += hands[i].Item2 * (i + 1);
                // Console.WriteLine(hands[i].Item1 + " (type " + GetHandType(hands[i].Item1) + ", " + hands[i].Item2 + " * " + (i + 1) + " = " + hands[i].Item2 * (i + 1) + ")");
            }

            return sum;
        }
        public int GetHandType2(string hand)
        {
            Dictionary<char, int> charCounts = new();

            foreach (char c in hand)
            {
                if (!charCounts.ContainsKey(c))
                {
                    charCounts.Add(c, 1);
                }
                else
                {
                    charCounts[c]++;
                }
            }

            Dictionary<char, int> charCountsWithoutJ = charCounts.Where(x => x.Key != 'J').ToDictionary(x => x.Key, x => x.Value);

            if (charCountsWithoutJ.Count == 0) return 1;

            char mostCommonChar = charCountsWithoutJ.MaxBy(x => x.Value).Key;
            if (charCounts.ContainsKey('J'))
            {
                charCounts[mostCommonChar] += charCounts['J'];
                charCounts['J'] = 0;
            }

            if (charCounts.Any(x => x.Value == 5)) return 1;
            if (charCounts.Any(x => x.Value == 4)) return 2;
            if (charCounts.Any(x => x.Value == 3) && charCounts.Any(x => x.Value == 2)) return 3;
            if (charCounts.Any(x => x.Value == 3)) return 4;
            if (charCounts.Count(x => x.Value == 2) == 2) return 5;
            if (charCounts.Any(x => x.Value == 2)) return 6;
            return 7;
        }

        public class SortHandByCards2 : IComparer<string>
        {
            public int Compare(string? hand1, string? hand2)
            {
                if (hand1 is null || hand2 is null || hand1.Length != hand2.Length) throw new Exception();

                for (int i = 0; i < hand1.Length; i++)
                {
                    string order = "AKQT98765432J";

                    if (order.IndexOf(hand1[i]) > order.IndexOf(hand2[i])) return -1;
                    if (order.IndexOf(hand1[i]) < order.IndexOf(hand2[i])) return 1;
                }
                return 0;
            }
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split("\n");
            List<(string, int)> hands = new();

            foreach (string line in lines)
            {
                string[] split = line.Split(" ");
                hands.Add((split[0], int.Parse(split[1])));
            }

            hands = hands.OrderByDescending(x => GetHandType2(x.Item1))
            .ThenBy(x => x.Item1, new SortHandByCards2()).ToList();

            int sum = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                sum += hands[i].Item2 * (i + 1);
                // Console.WriteLine(hands[i].Item1 + " (type " + GetHandType2(hands[i].Item1) + ", " + hands[i].Item2 + " * " + (i + 1) + " = " + hands[i].Item2 * (i + 1) + ")");
            }

            return sum;
        }
    }
}
