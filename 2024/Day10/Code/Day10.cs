using HelperClasses;
using System.Drawing;

namespace Year2024
{
    public class Day10 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split("\n");
            int[][] intGrid = lines.Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
            Map<int> grid = new Map<int>(intGrid);
            int total = 0;

            foreach (Point trailStart in grid.IndexesOf(0))
            {
                int subTotal = 0;

                Queue<Point> pointsToCheck = new();
                pointsToCheck.Enqueue(trailStart);

                while (pointsToCheck.Count > 0)
                {
                    Point currentPoint = pointsToCheck.Dequeue();
                    int currentNumber = grid[currentPoint];

                    if (grid[currentPoint] == 9)
                    {
                        subTotal++;
                        continue;
                    }

                    foreach (Direction direction in Direction.Directions)
                    {
                        if (currentPoint.X + direction.DeltaX < 0 || currentPoint.X + direction.DeltaX >= grid.Width
                            || currentPoint.Y + direction.DeltaY < 0 || currentPoint.Y + direction.DeltaY >= grid.Height) continue;
                        Point contender = new Point(currentPoint.X + direction.DeltaX, currentPoint.Y + direction.DeltaY);
                        if (grid[contender] == currentNumber + 1)
                        {
                            if (pointsToCheck.Contains(contender)) continue;
                            pointsToCheck.Enqueue(contender);
                        }
                    }
                }

                total += subTotal;
            }

            return total;
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split("\n");
            int[][] intGrid = lines.Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
            Map<int> grid = new Map<int>(intGrid);
            int total = 0;

            foreach (Point trailStart in grid.IndexesOf(0))
            {
                int subTotal = 0;

                Queue<Point> pointsToCheck = new();
                pointsToCheck.Enqueue(trailStart);

                while (pointsToCheck.Count > 0)
                {
                    Point currentPoint = pointsToCheck.Dequeue();
                    int currentNumber = grid[currentPoint];

                    if (grid[currentPoint] == 9)
                    {
                        subTotal++;
                        continue;
                    }

                    foreach (Direction direction in Direction.Directions)
                    {
                        if (currentPoint.X + direction.DeltaX < 0 || currentPoint.X + direction.DeltaX >= grid.Width
                            || currentPoint.Y + direction.DeltaY < 0 || currentPoint.Y + direction.DeltaY >= grid.Height) continue;
                        Point contender = new Point(currentPoint.X + direction.DeltaX, currentPoint.Y + direction.DeltaY);
                        if (grid[contender] == currentNumber + 1)
                        {
                            pointsToCheck.Enqueue(contender);
                        }
                    }
                }

                total += subTotal;
            }

            return total;
        }
    }
}