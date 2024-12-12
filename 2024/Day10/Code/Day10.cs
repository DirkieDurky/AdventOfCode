using Advent_of_Code.HelperClasses;

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

            foreach (Position trailStart in grid.IndexesOf(0))
            {
                int subTotal = 0;

                Queue<Position> PositionsToCheck = new();
                PositionsToCheck.Enqueue(trailStart);

                while (PositionsToCheck.Count > 0)
                {
                    Position currentPosition = PositionsToCheck.Dequeue();
                    int currentNumber = grid[currentPosition];

                    if (grid[currentPosition] == 9)
                    {
                        subTotal++;
                        continue;
                    }

                    foreach (Direction direction in Direction.OrthogonalDirections)
                    {
                        if (currentPosition.X + direction.DeltaX < 0 || currentPosition.X + direction.DeltaX >= grid.Width
                            || currentPosition.Y + direction.DeltaY < 0 || currentPosition.Y + direction.DeltaY >= grid.Height) continue;
                        Position contender = new Position(currentPosition.X + direction.DeltaX, currentPosition.Y + direction.DeltaY);
                        if (grid[contender] == currentNumber + 1)
                        {
                            if (PositionsToCheck.Contains(contender)) continue;
                            PositionsToCheck.Enqueue(contender);
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

            foreach (Position trailStart in grid.IndexesOf(0))
            {
                int subTotal = 0;

                Queue<Position> PositionsToCheck = new();
                PositionsToCheck.Enqueue(trailStart);

                while (PositionsToCheck.Count > 0)
                {
                    Position currentPosition = PositionsToCheck.Dequeue();
                    int currentNumber = grid[currentPosition];

                    if (grid[currentPosition] == 9)
                    {
                        subTotal++;
                        continue;
                    }

                    foreach (Direction direction in Direction.OrthogonalDirections)
                    {
                        if (currentPosition.X + direction.DeltaX < 0 || currentPosition.X + direction.DeltaX >= grid.Width
                            || currentPosition.Y + direction.DeltaY < 0 || currentPosition.Y + direction.DeltaY >= grid.Height) continue;
                        Position contender = new Position(currentPosition.X + direction.DeltaX, currentPosition.Y + direction.DeltaY);
                        if (grid[contender] == currentNumber + 1)
                        {
                            PositionsToCheck.Enqueue(contender);
                        }
                    }
                }

                total += subTotal;
            }

            return total;
        }
    }
}