using Advent_of_Code.HelperClasses;

namespace Year2024
{
    public class Day26 : IDay
    {
        public object Sol1(string input)
        {
            return 0;
        }

        private struct Region(char type, List<Position> plots)
        {
            public char Type = type;
            public List<Position> Plots { get; set; } = plots;
            public int Perimiter = 0;
        }

        public object Sol2(string input)
        {
            CharMap grid = new CharMap(input.Split('\n'));
            List<Region2> foundRegions = new();
            List<Position> donePositions = new();

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    Position pos = new Position(x, y);
                    if (donePositions.Contains(pos)) continue;
                    Region2 currentRegion = new Region2(grid[pos], new());

                    Queue<Position> toDoPositions = new();
                    toDoPositions.Enqueue(new Position(x, y));

                    while (toDoPositions.Any())
                    {
                        Position currentPosition = toDoPositions.Dequeue();
                        char currentChar = grid[currentPosition];

                        currentRegion.Plots.Add(currentPosition);
                        donePositions.Add(currentPosition);

                        foreach (Direction direction in Direction.OrthogonalDirections)
                        {
                            Position nextPosition = currentPosition + direction;
                            if (!nextPosition.IsInBounds(grid))
                            {
                                currentRegion.AddBorder(new Border(currentPosition, direction));
                                continue;
                            }
                            if (currentRegion.Plots.Contains(nextPosition)) continue;

                            if (grid[nextPosition] == currentChar)
                            {
                                if (toDoPositions.Contains(nextPosition)) continue;
                                toDoPositions.Enqueue(nextPosition);
                            }
                            else
                            {
                                currentRegion.AddBorder(new Border(currentPosition, direction));
                            }
                        }
                    }

                    foundRegions.Add(currentRegion);
                }
            }

            //foreach (Region2 region in foundRegions)
            //{
            //    Console.WriteLine($"{region.Plots.Count} * {region.Sides} = {region.Plots.Count * region.Sides} (Starting at {region.Plots.First().X}, {region.Plots.First().Y} with type {region.Type})");
            //}
            return foundRegions.Sum(r => r.Plots.Count * r.Sides);
        }

        private class Region2(char type, List<Position> plots)
        {
            public char Type = type;
            public List<Position> Plots = plots;
            public List<Border> Borders { get; private set; } = new();
            public int Sides { get; set; } = 0;

            public void AddBorder(Border border)
            {
                Borders.Add(border);
                foreach (Direction direction in Direction.OrthogonalDirections)
                {
                    Position newPos = border.Position + direction;

                    if (Borders.Any(b => b.Position == newPos && b.Side == border.Side))
                    {
                        return;
                    }
                }

                Sides++;
            }
        }

        private struct Border(Position position, Direction side)
        {
            public Position Position = position;
            public Direction Side = side;
        }
    }
}