using System.Text.RegularExpressions;
using System.Drawing;

namespace Year2023
{
    public class Day03 : IDay
    {
        public class NumberPoint
        {
            public Point StartPoint = new();
            public int Length = 0;

            public NumberPoint(Point firstDigitPoint, int length)
            {
                StartPoint = firstDigitPoint;
                Length = length;
            }

            public override bool Equals(object? obj)
            {
                if (obj is not NumberPoint numberPoint) return false;
                return StartPoint == numberPoint.StartPoint && Length == numberPoint.Length;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(StartPoint.X, StartPoint.Y, Length);
            }
        }

        public Object Sol1(String input)
        {
            String[] lines = input.Split("\n");
            int height = lines.Length;
            int width = lines[0].Length;

            List<Point> gearPoints = new();

            for (int y = 0; y < lines.Length; y++)
            {
                String line = lines[y];

                Regex regex = new Regex("[^\\d\\.]");
                foreach (Match match in regex.Matches(line))
                {
                    gearPoints.Add(new Point(match.Index, y));
                }
            }

            HashSet<Point> foundDigits = new();

            foreach (Point gearPoint in gearPoints)
            {

                Point[] offsets = {
                    new Point(-1, -1),
                    new Point(0, -1),
                    new Point(+1, -1),
                    new Point(-1, 0),
                    new Point(+1, 0),
                    new Point(-1, +1),
                    new Point(0, +1),
                    new Point(+1, +1),
                };

                foreach (Point offset in offsets)
                {
                    Point newPoint = new Point(gearPoint.X + offset.X, gearPoint.Y + offset.Y);
                    if (newPoint.X >= width || newPoint.Y >= height) continue;

                    if (Char.IsNumber(lines[newPoint.Y][newPoint.X])) foundDigits.Add(newPoint);
                }
            }

            // Console.WriteLine("FoundDigits:");
            // foreach (Point foundDigit in foundDigits)
            // {
            //     Console.WriteLine("X: " + foundDigit.X + " Y: " + foundDigit.Y);
            // }

            HashSet<NumberPoint> foundNumbers = new();

            foreach (Point foundDigit in foundDigits)
            {
                int startPointX = foundDigit.X;
                int length = 1;

                while (startPointX > 0 && Char.IsNumber(lines[foundDigit.Y][startPointX - 1]))
                {
                    startPointX--;
                    length++;
                }

                int currentX = foundDigit.X;
                while (currentX + 1 < width && Char.IsNumber(lines[foundDigit.Y][currentX + 1]))
                {
                    currentX++;
                    length++;
                }

                foundNumbers.Add(new NumberPoint(new Point(startPointX, foundDigit.Y), length));
            }

            // Console.WriteLine();
            // Console.WriteLine("FoundNumbers:");
            // foreach (NumberPoint foundNumber in foundNumbers)
            // {
            //     Console.WriteLine("X: " + foundNumber.StartPoint.X + " Y: " + foundNumber.StartPoint.Y + " Length: " + foundNumber.Length);
            // }

            int sum = 0;

            // Console.WriteLine();
            // Console.WriteLine("Numbers: ");
            foreach (NumberPoint numberPoint in foundNumbers)
            {
                int number = 0;
                for (int i = 0; i < numberPoint.Length; i++)
                {
                    number += (int)Char.GetNumericValue(lines[numberPoint.StartPoint.Y][numberPoint.StartPoint.X + i]) * (int)Math.Pow(10, (numberPoint.Length - i - 1));
                }

                // Console.WriteLine(number);
                sum += number;
            }

            return sum;
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split("\n");
            int height = lines.Length;
            int width = lines[0].Length;

            List<Point> gearPoints = new();

            for (int y = 0; y < lines.Length; y++)
            {
                String line = lines[y];

                Regex regex = new Regex("\\*");
                foreach (Match match in regex.Matches(line))
                {
                    gearPoints.Add(new Point(match.Index, y));
                }
            }

            int sum = 0;

            foreach (Point gearPoint in gearPoints)
            {
                HashSet<Point> foundDigits = new();

                Point[] offsets = {
                    new Point(-1, -1),
                    new Point(0, -1),
                    new Point(+1, -1),
                    new Point(-1, 0),
                    new Point(+1, 0),
                    new Point(-1, +1),
                    new Point(0, +1),
                    new Point(+1, +1),
                };

                foreach (Point offset in offsets)
                {
                    Point newPoint = new Point(gearPoint.X + offset.X, gearPoint.Y + offset.Y);
                    if (newPoint.X >= width || newPoint.Y >= height) continue;

                    if (Char.IsNumber(lines[newPoint.Y][newPoint.X])) foundDigits.Add(newPoint);
                }

                if (foundDigits.Count >= 2)
                {
                    HashSet<NumberPoint> foundNumbers = new();

                    foreach (Point foundDigit in foundDigits)
                    {
                        int startPointX = foundDigit.X;
                        int length = 1;

                        while (startPointX > 0 && Char.IsNumber(lines[foundDigit.Y][startPointX - 1]))
                        {
                            startPointX--;
                            length++;
                        }

                        int currentX = foundDigit.X;
                        while (currentX + 1 < width && Char.IsNumber(lines[foundDigit.Y][currentX + 1]))
                        {
                            currentX++;
                            length++;
                        }

                        foundNumbers.Add(new NumberPoint(new Point(startPointX, foundDigit.Y), length));
                    }

                    if (foundNumbers.Count == 2)
                    {
                        List<int> numbers = new();

                        foreach (NumberPoint numberPoint in foundNumbers)
                        {
                            int number = 0;
                            for (int i = 0; i < numberPoint.Length; i++)
                            {
                                number += (int)Char.GetNumericValue(lines[numberPoint.StartPoint.Y][numberPoint.StartPoint.X + i]) * (int)Math.Pow(10, (numberPoint.Length - i - 1));
                            }

                            numbers.Add(number);
                        }

                        sum += numbers[0] * numbers[1];
                    }
                }
            }

            return sum;
        }
    }
}
