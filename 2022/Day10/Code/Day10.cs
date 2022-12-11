using System.Text;
using System.Text.RegularExpressions;

namespace Year2022;

public class Day10 : IDay
{
    public static Int32 X = 1;

    public Object Sol1(String input)
    {
        X = 1;
        String[] lines = input.Split('\n');

        ICommand? currentCommand = null;
        List<(Int32, Int32)> xValues = new();

        Int32 tickIndex = 1;
        Int32 commandIndex = 0;
        while (commandIndex < lines.Length || (currentCommand != null && currentCommand.LeftExecutionTime > 0))
        {
            if ((tickIndex + 20) % 40 == 0)
            {
                xValues.Add((tickIndex, X));
            }

            if (currentCommand == null)
            {
                String line = lines[commandIndex];
                if (line.StartsWith("noop"))
                {
                    currentCommand = new Noop(1);
                }
                else if (line.StartsWith("addx "))
                {
                    Int32 addAmount = Int32.Parse(line.Replace("addx ", ""));
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

        Int32 signalStrengthSum = 0;

        foreach ((Int32 i, Int32 x) in xValues)
        {
            signalStrengthSum += i * x;
        }

        return signalStrengthSum;
    }

    public Object Sol2(String input)
    {
        X = 1;
        String[] lines = input.Split('\n');

        StringBuilder pixels = new();
        ICommand? currentCommand = null;

        Int32 tickIndex = 1;
        Int32 commandIndex = 0;
        while (commandIndex < lines.Length || (currentCommand != null && currentCommand.LeftExecutionTime > 0))
        {
            pixels.Append(Math.Abs(X - (tickIndex % 40 - 1)) < 2 ? '#' : '.');

            if (currentCommand == null)
            {
                String line = lines[commandIndex];
                if (line.StartsWith("noop"))
                {
                    currentCommand = new Noop(1);
                }
                else if (line.StartsWith("addx "))
                {
                    Int32 addAmount = Int32.Parse(line.Replace("addx ", ""));
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

        String pixelsString = pixels.ToString();

        const Int32 screenWidth = 40;

        return Regex.Replace(pixelsString, ".{40}", "$0\n");
    }
}