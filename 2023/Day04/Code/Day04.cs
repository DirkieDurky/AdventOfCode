using System.Diagnostics.CodeAnalysis;

namespace Year2023
{
    public class Day04 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split("\n");

            int sum = 0;

            foreach (String line in lines)
            {
                String values = line.Split(": ")[1].Replace("  ", " ").Trim();
                String[] split = values.Split(" | ");
                int[] winningNumbers = split[0].Split(" ").Select(int.Parse).ToArray();
                int[] myNumbers = split[1].Split(" ").Select(int.Parse).ToArray();

                int myWinningNumberCount = 0;
                foreach (int number in myNumbers)
                {
                    if (winningNumbers.Contains(number)) myWinningNumberCount++;
                }

                if (myWinningNumberCount > 0)
                    sum += 1 * (int)Math.Pow(2, myWinningNumberCount - 1);
            }

            return sum;
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split("\n");
            String[] cards = lines.Select(x => x.Split(": ")[1].Replace("  ", " ").Trim()).ToArray();

            List<int> cardsTodo = Enumerable.Range(1, cards.Length).ToList();
            for (int i = 0; i < cardsTodo.Count; i++)
            {
                // Console.WriteLine(cardsTodo[i]);
                String card = cards[cardsTodo[i] - 1];
                // Console.WriteLine(card);
                String[] split = card.Split(" | ");
                int[] winningNumbers = split[0].Split(" ").Select(int.Parse).ToArray();
                int[] myNumbers = split[1].Split(" ").Select(int.Parse).ToArray();

                int myWinningNumberCount = 0;
                foreach (int number in myNumbers)
                {
                    if (winningNumbers.Contains(number)) myWinningNumberCount++;
                }

                for (int j = 0; j < myWinningNumberCount; j++)
                {
                    cardsTodo.Add(cardsTodo[i] + j + 1);
                    // Console.WriteLine(cardsTodo[i] + j + 1 + " added");
                }
            }

            return cardsTodo.Count;
        }
    }
}
