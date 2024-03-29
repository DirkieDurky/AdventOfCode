using System.Drawing;
namespace Year2022
{
    public class Day09 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');

            HashSet<Point> tailPath = new();
            List<Move.Direction> directions = Move.Parse(lines);

            Point headPoint = new Point(0, 0);
            Point tailPoint = new Point(0, 0);
            tailPath.Add(tailPoint);

            foreach (Move.Direction direction in directions)
            {
                switch (direction)
                {
                    case Move.Direction.Up:
                        headPoint.Y++;
                        break;
                    case Move.Direction.Down:
                        headPoint.Y--;
                        break;
                    case Move.Direction.Left:
                        headPoint.X--;
                        break;
                    case Move.Direction.Right:
                        headPoint.X++;
                        break;
                }

                int horDiff = headPoint.X - tailPoint.X;
                int verDiff = headPoint.Y - tailPoint.Y;
                int horAbsDiff = Math.Abs(headPoint.X - tailPoint.X);
                int verAbsDiff = Math.Abs(headPoint.Y - tailPoint.Y);
                bool horDetached = horAbsDiff > 1;
                bool verDetached = verAbsDiff > 1;

                if (horAbsDiff > 1)
                {
                    //Change -2 to -1 and 2 to 1
                    tailPoint.X += horDiff / Math.Abs(horDiff);
                    if (verAbsDiff > 0)
                    {
                        tailPoint.Y += verDiff / Math.Abs(verDiff);
                    }
                }
                if (verAbsDiff > 1)
                {
                    tailPoint.Y += verDiff / Math.Abs(verDiff);
                    if (horAbsDiff > 0)
                    {
                        tailPoint.X += horDiff / Math.Abs(horDiff);
                    }
                }

                tailPath.Add(tailPoint);
            }

            return tailPath.Count();
        }

        public object Sol2(string input)
        {
            const int KnotAmount = 10;

            string[] lines = input.Split('\n');

            HashSet<Point> tailPath = new();
            List<Move.Direction> directions = Move.Parse(lines);

            Point[] knotPoints = Enumerable.Repeat(new Point(0, 0), KnotAmount).ToArray();
            tailPath.Add(knotPoints[^1]);

            foreach (Move.Direction direction in directions)
            {
                switch (direction)
                {
                    case Move.Direction.Up:
                        knotPoints[0].Y++;
                        break;
                    case Move.Direction.Down:
                        knotPoints[0].Y--;
                        break;
                    case Move.Direction.Left:
                        knotPoints[0].X--;
                        break;
                    case Move.Direction.Right:
                        knotPoints[0].X++;
                        break;
                }

                // Console.WriteLine($"{knotPoints[0].X} {knotPoints[0].Y}");

                for (int i = 1; i < knotPoints.Length; i++)
                {
                    int horDiff = knotPoints[i - 1].X - knotPoints[i].X;
                    int verDiff = knotPoints[i - 1].Y - knotPoints[i].Y;
                    int horAbsDiff = Math.Abs(knotPoints[i - 1].X - knotPoints[i].X);
                    int verAbsDiff = Math.Abs(knotPoints[i - 1].Y - knotPoints[i].Y);
                    bool horDetached = horAbsDiff > 1;
                    bool verDetached = verAbsDiff > 1;

                    if (horAbsDiff > 1)
                    {
                        //Change every negative number to -1 and every positive number to 1
                        // knotPoints[i].X += horDiff / Math.Abs(horDiff);
                        knotPoints[i].X += Math.Clamp(horDiff, -1, 1);
                        if (verAbsDiff > 0)
                        {
                            knotPoints[i].Y += Math.Clamp(verDiff, -1, 1);
                        }
                    }
                    else if (verAbsDiff > 1)
                    {
                        knotPoints[i].Y += Math.Clamp(verDiff, -1, 1);
                        if (horAbsDiff > 0)
                        {
                            knotPoints[i].X += Math.Clamp(horDiff, -1, 1);
                        }
                    }

                    // Console.WriteLine($"{knotPoints[i].X} {knotPoints[i].Y}");
                }

                // Console.WriteLine();

                tailPath.Add(knotPoints[^1]);
            }

            // foreach (Point point in tailPath)
            // {
            //     Console.WriteLine($"{point.X} {point.Y}");
            // }

            return tailPath.Count();
        }
    }
}
