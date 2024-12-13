using Advent_of_Code.HelperClasses;
using System.Text.RegularExpressions;

namespace Year2024
{
    public class Day13 : IDay
    {
        public object Sol1(string input)
        {
            string[] clawMachinesStr = input.Split("\n\n");
            List<ClawMachine> clawMachines = new();

            foreach (string clawMachineStr in clawMachinesStr)
            {
                string[] split = clawMachineStr.Split('\n');
                Match buttonAInfo = Regex.Match(split[0], "Button A: X\\+(\\d+), Y\\+(\\d+)");
                Match buttonBInfo = Regex.Match(split[1], "Button B: X\\+(\\d+), Y\\+(\\d+)");
                Match prizeInfo = Regex.Match(split[2], "Prize: X=(\\d+), Y=(\\d+)");


                Position buttonA = new(int.Parse(buttonAInfo.Groups[1].Value), int.Parse(buttonAInfo.Groups[2].Value));
                Position buttonB = new(int.Parse(buttonBInfo.Groups[1].Value), int.Parse(buttonBInfo.Groups[2].Value));
                Position prize = new(int.Parse(prizeInfo.Groups[1].Value), int.Parse(prizeInfo.Groups[2].Value));

                clawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
            }

            List<(int buttonA, int buttonB)> optimalOrder = new();

            (int buttonA, int buttonB) pressCount = (0, 0);

            while (pressCount.buttonA < 100)
            {
                if (pressCount.buttonB < 100)
                {
                    pressCount.buttonB++;
                }
                else
                {
                    pressCount.buttonB = 0;
                    pressCount.buttonA++;
                }

                optimalOrder.Add(pressCount);
            }

            optimalOrder = optimalOrder.OrderBy(x => x.Item1 * 3 + x.Item2).ToList();

            foreach ((int buttonA, int buttonB) pressCount2 in optimalOrder)
            {
                Console.WriteLine($"{pressCount2.buttonA} {pressCount2.buttonB}");
            }

            int total = 0;
            foreach (ClawMachine clawMachine in clawMachines)
            {
                total += GetMinimalPrice(optimalOrder, clawMachine);
            }

            return total;
        }

        private int GetMinimalPrice(List<(int buttonA, int buttonB)> optimalOrder, ClawMachine clawMachine)
        {
            foreach ((int buttonA, int buttonB) pressCount in optimalOrder)
            {
                Position currentPos = clawMachine.ButtonA * pressCount.buttonA;
                currentPos += clawMachine.ButtonB * pressCount.buttonB;

                if (currentPos == clawMachine.Prize) return pressCount.buttonA * 3 + pressCount.buttonB;
            }

            return 0;
        }

        class ClawMachine(Position buttonA, Position buttonB, Position prize)
        {
            public Position ButtonA = buttonA;
            public Position ButtonB = buttonB;
            public Position Prize = prize;
        }

        public object Sol2(string input)
        {
            string[] clawMachinesStr = input.Split("\n\n");
            List<ClawMachine2> clawMachines = new();

            foreach (string clawMachineStr in clawMachinesStr)
            {
                string[] split = clawMachineStr.Split('\n');
                Match buttonAInfo = Regex.Match(split[0], "Button A: X\\+(\\d+), Y\\+(\\d+)");
                Match buttonBInfo = Regex.Match(split[1], "Button B: X\\+(\\d+), Y\\+(\\d+)");
                Match prizeInfo = Regex.Match(split[2], "Prize: X=(\\d+), Y=(\\d+)");

                Position buttonA = new(int.Parse(buttonAInfo.Groups[1].Value), int.Parse(buttonAInfo.Groups[2].Value));
                Position buttonB = new(int.Parse(buttonBInfo.Groups[1].Value), int.Parse(buttonBInfo.Groups[2].Value));
                long prizeX = int.Parse(prizeInfo.Groups[1].Value) + 10000000000000;
                long prizeY = int.Parse(prizeInfo.Groups[2].Value) + 10000000000000;

                clawMachines.Add(new ClawMachine2(buttonA, buttonB, prizeX, prizeY));
            }

            long total = 0;
            foreach (ClawMachine2 clawMachine in clawMachines)
            {
                long result = GetMinimalPrice2(clawMachine);
                Console.WriteLine(result);
                total += result;
            }

            return total;
        }

        private long GetMinimalPrice2(ClawMachine2 clawMachine)
        {
            double denominator = clawMachine.ButtonA.X * clawMachine.ButtonB.Y - clawMachine.ButtonA.Y * clawMachine.ButtonB.X;
            double numerator = clawMachine.PrizeY * clawMachine.ButtonA.X - clawMachine.PrizeX * clawMachine.ButtonA.Y;

            double buttonBPresses = numerator / denominator;
            if (buttonBPresses % 1 > 0) return 0;

            double buttonAPresses = (clawMachine.PrizeX - clawMachine.ButtonB.X * buttonBPresses) / clawMachine.ButtonA.X;
            if (buttonAPresses % 1 > 0) return 0;

            return (long)(buttonAPresses * 3 + buttonBPresses);
        }

        class ClawMachine2(Position buttonA, Position buttonB, long prizeX, long prizeY)
        {
            public Position ButtonA = buttonA;
            public Position ButtonB = buttonB;
            public long PrizeX = prizeX;
            public long PrizeY = prizeY;
        }
    }
}