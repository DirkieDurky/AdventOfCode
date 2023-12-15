namespace Year2022
{
    public class Day11 : IDay
    {
        public static int ModNumber = 1;

        public object Sol1(string input)
        {
            Monkey.Monkeys.Clear();
            string[] monkeys = input.Split("\n\n");

            foreach (string monkey in monkeys)
            {
                Monkey.Monkeys.Add(new Monkey(monkey));
            }

            for (int i = 0; i < 20; i++)
            {
                foreach (Monkey monkey in Monkey.Monkeys)
                {
                    monkey.ExecuteTurn();
                }

                // foreach (Monkey monkey in Monkey.Monkeys)
                // {
                //     foreach (Int32 item in monkey.GetHoldingItems())
                //     {
                //         Console.Write(item + ", ");
                //     }
                //
                //     Console.WriteLine();
                // }
                //
                // Console.WriteLine();
            }

            List<long> inspectCounts = Monkey.Monkeys.Select(monkey => monkey.InspectCount).ToList();
            inspectCounts.Sort();

            // foreach (Int32 count in inspectCounts)
            // {
            //     Console.WriteLine(count);
            // }

            return inspectCounts[^1] * inspectCounts[^2];
        }

        public object Sol2(string input)
        {
            Monkey.Monkeys.Clear();
            string[] monkeys = input.Split("\n\n");

            foreach (string monkey in monkeys)
            {
                Monkey.Monkeys.Add(new Monkey(monkey));
            }

            foreach (Monkey monkey in Monkey.Monkeys)
            {
                ModNumber *= monkey.TestDivisibleBy;
            }

            // Console.WriteLine(ModNumber);

            int[] arr = { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };

            for (int i = 0; i < 10000; i++)
            {
                foreach (Monkey monkey in Monkey.Monkeys)
                {
                    monkey.ExecuteTurn(false);
                }

                // if (arr.Contains(i + 1))
                // {
                //     Console.WriteLine($"{i + 1}: ");
                //     foreach (Monkey monkey in Monkey.Monkeys)
                //     {
                //         foreach (Int32 item in monkey.GetHoldingItems())
                //         {
                //             Console.Write(item + ", ");
                //         }
                //
                //         Console.WriteLine();
                //     }
                //
                //     Console.WriteLine();
                // }
            }

            List<long> inspectCounts = Monkey.Monkeys.Select(monkey => monkey.InspectCount).ToList();
            inspectCounts.Sort();

            // foreach (Int32 count in inspectCounts)
            // {
            //     Console.WriteLine(count);
            // }

            return inspectCounts[^1] * inspectCounts[^2];
        }
    }
}
