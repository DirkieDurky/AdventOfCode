using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

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

        public class Card
        {
            public int Number;
            public int[] WinningNumbers;
            public int[] MyNumbers;
            public int? CardGenerateAmount;

            public Card(int number, int[] winningNumbers, int[] myNumbers)
            {
                Number = number;
                WinningNumbers = winningNumbers;
                MyNumbers = myNumbers;
            }
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split("\n");
            List<Card> cards = new();

            foreach (String line in lines)
            {
                String[] split = line.Split(": ");

                int number = int.Parse(split[0].Replace("   ", " ").Replace("  ", " ").Split(" ")[1]);
                String[] values = split[1].Split(" | ");
                int[] winningNumbers = values[0].Replace("  ", " ").Trim().Split(" ").Select(int.Parse).ToArray();
                int[] myNumbers = values[1].Replace("  ", " ").Trim().Split(" ").Select(int.Parse).ToArray();
                cards.Add(new Card(number, winningNumbers, myNumbers));
            }

            for (int i = cards.Count - 1; i >= 0; i--)
            {
                cards[i].CardGenerateAmount = GetWinnedCardIndexes(i);
            }

            int GetWinnedCardIndexes(int cardIndex)
            {
                if (cards[cardIndex].CardGenerateAmount is not null)
                {
                    return (int)cards[cardIndex].CardGenerateAmount!;
                }

                int myWinningNumberCount = 0;
                foreach (int number in cards[cardIndex].MyNumbers)
                {
                    if (cards[cardIndex].WinningNumbers.Contains(number)) myWinningNumberCount++;
                }

                int sum = 1;
                foreach (int index in Enumerable.Range(cardIndex + 1, myWinningNumberCount))
                {
                    sum += GetWinnedCardIndexes(index);
                }

                return sum;
            }

            int sum = 0;
            foreach (Card card in cards)
            {
                if (card.CardGenerateAmount is null) continue;
                sum += (int)card.CardGenerateAmount;
            }

            return sum;
        }
    }
}
