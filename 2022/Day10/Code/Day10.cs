using System.Text;
using System.Text.RegularExpressions;

namespace Year2022;

public class Day10 : IDay
{
    public static int X = 1;

    public object Sol1(string input)
    {
        X = 1;
        string[] lines = input.Split('\n');

        ICommand? currentCommand = null;
        List<(int, int)> xValues = new();

        int tickIndex = 1;
        int commandIndex = 0;
        while (commandIndex < lines.Length || (currentCommand != null && currentCommand.LeftExecutionTime > 0))
        {
            if ((tickIndex + 20) % 40 == 0)
            {
                xValues.Add((tickIndex, X));
            }

            if (currentCommand == null)
            {
                string line = lines[commandIndex];
                if (line.StartsWith("noop"))
                {
                    currentCommand = new Noop(1);
                }
                else if (line.StartsWith("addx "))
                {
                    int addAmount = int.Parse(line.Replace("addx ", ""));
                    currentCommand = new AddX(addAmount, 2);
                }

                commandIndex++;
            }

            // Console.WriteLine($"{tickIndex} {X}");

            currentCommand!.LeftExecutionTime--;

            if (currentCommand.LeftExecutionTime == 0)
            {
                currentCommand.Execute();
                currentCommand = null;
            }

            tickIndex++;
        }

        int signalStrengthSum = 0;

        foreach ((int i, int x) in xValues)
        {
            signalStrengthSum += i * x;
        }

        return signalStrengthSum;
    }

    public object Sol2(string input)
    {
        X = 1;
        string[] lines = input.Split('\n');

        StringBuilder pixels = new();
        ICommand? currentCommand = null;

        int tickIndex = 1;
        int commandIndex = 0;
        while (commandIndex < lines.Length || (currentCommand != null && currentCommand.LeftExecutionTime > 0))
        {
            pixels.Append(Math.Abs(X - (tickIndex % 40 - 1)) < 2 ? '#' : '.');

            if (currentCommand == null)
            {
                string line = lines[commandIndex];
                if (line.StartsWith("noop"))
                {
                    currentCommand = new Noop(1);
                }
                else if (line.StartsWith("addx "))
                {
                    int addAmount = int.Parse(line.Replace("addx ", ""));
                    currentCommand = new AddX(addAmount, 2);
                }

                commandIndex++;
            }

            // Console.WriteLine($"{tickIndex} {X}");

            currentCommand!.LeftExecutionTime--;

            if (currentCommand.LeftExecutionTime == 0)
            {
                currentCommand.Execute();
                currentCommand = null;
            }

            tickIndex++;
        }

        string pixelsString = pixels.ToString();

        const int screenWidth = 40;

        return Regex.Replace(pixelsString, ".{" + screenWidth + "}", "$0\n");
    }
}
