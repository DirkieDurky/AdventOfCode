using Advent_of_Code.HelperClasses;

namespace Year2024
{
    public class Day12 : IDay
    {
        public object Sol1(string input)
        {
            CharMap grid = new CharMap(input.Split('\n'));
            List<Region> foundRegions = new();
            List<Position> donePositions = new();

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    Position pos = new Position(x, y);
                    if (donePositions.Contains(pos)) continue;
                    Region currentRegion = new Region(grid[pos], new());

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
                                currentRegion.Perimiter++;
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
                                currentRegion.Perimiter++;
                            }
                        }
                    }

                    foundRegions.Add(currentRegion);
                }
            }

            foreach (Region region in foundRegions.OrderBy(x => x.Plots.Count))
            {
                Console.WriteLine($"{region.Plots.Count} * {region.Perimiter} = {region.Plots.Count * region.Perimiter} (Starting at {region.Plots.First().X}, {region.Plots.First().Y} with type {region.Type})");
            }
            return foundRegions.Sum(r => r.Plots.Count * r.Perimiter);
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
                                currentRegion.Borders.Add(new Border(currentPosition, direction));
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
                                currentRegion.Borders.Add(new Border(currentPosition, direction));
                            }
                        }
                    }

                    foundRegions.Add(currentRegion);
                }
            }

            for (Int32 i = 0; i < foundRegions.Count; i++)
            {
                Region2 region = foundRegions[i];

                region.Sides = region.CountCorners(grid);
            }

            foreach (Region2 region in foundRegions)
            {
                Console.WriteLine($"{region.Plots.Count} * {region.Sides} = {region.Plots.Count * region.Sides} (Starting at {region.Plots.First().X}, {region.Plots.First().Y} with type {region.Type})");
            }
            return foundRegions.Sum(r => r.Plots.Count * r.Sides);
        }

        private class Region2(char type, List<Position> plots)
        {
            public char Type = type;
            public List<Position> Plots = plots;
            public List<Border> Borders { get; private set; } = new();
            public int Sides { get; set; } = 0;

            public int CountCorners(CharMap grid)
            {
                int corners = 0;

                foreach (Position plot in Plots)
                {
                    int neighborCount = plot.CountEqualNeighbors(grid, Direction.OrthogonalDirections);

                    if (neighborCount == 0) { corners += 4; continue; }
                    if (neighborCount == 1) { corners += 2; continue; }
                    if (neighborCount == 2)
                    {
                        //Opposing neighbors (I shape)
                        if ((plot.HasEqualNeighbor(grid, Direction.Up) && plot.HasEqualNeighbor(grid, Direction.Down))
                            || (plot.HasEqualNeighbor(grid, Direction.Left) && plot.HasEqualNeighbor(grid, Direction.Right)))
                        {
                            corners += 0; continue;
                        }
                        //Corner neighbors (L shape)
                        else
                        {
                            corners += 1;
                            if ((plot.HasEqualNeighbor(grid, Direction.Up) && plot.HasEqualNeighbor(grid, Direction.Right) && plot.HasOpposingNeighbor(grid, Direction.UpRight))
                            || (plot.HasEqualNeighbor(grid, Direction.Down) && plot.HasEqualNeighbor(grid, Direction.Right) && plot.HasOpposingNeighbor(grid, Direction.DownRight))
                            || (plot.HasEqualNeighbor(grid, Direction.Down) && plot.HasEqualNeighbor(grid, Direction.Left) && plot.HasOpposingNeighbor(grid, Direction.DownLeft))
                            || (plot.HasEqualNeighbor(grid, Direction.Up) && plot.HasEqualNeighbor(grid, Direction.Left) && plot.HasOpposingNeighbor(grid, Direction.UpLeft)))
                            {
                                corners += 1;
                            }
                            continue;
                        }
                    }
                    //T shape
                    if (neighborCount == 3 || neighborCount == 4)
                    {
                        if (plot.HasEqualNeighbor(grid, Direction.Up) && plot.HasEqualNeighbor(grid, Direction.Right) && plot.HasOpposingNeighbor(grid, Direction.UpRight)) corners += 1;
                        if (plot.HasEqualNeighbor(grid, Direction.Down) && plot.HasEqualNeighbor(grid, Direction.Right) && plot.HasOpposingNeighbor(grid, Direction.DownRight)) corners += 1;
                        if (plot.HasEqualNeighbor(grid, Direction.Down) && plot.HasEqualNeighbor(grid, Direction.Left) && plot.HasOpposingNeighbor(grid, Direction.DownLeft)) corners += 1;
                        if (plot.HasEqualNeighbor(grid, Direction.Up) && plot.HasEqualNeighbor(grid, Direction.Left) && plot.HasOpposingNeighbor(grid, Direction.UpLeft)) corners += 1;
                        continue;
                    }
                }

                return corners;
            }
        }

        private struct Border(Position position, Direction side)
        {
            public Position Position = position;
            public Direction Side = side;
        }
    }
}