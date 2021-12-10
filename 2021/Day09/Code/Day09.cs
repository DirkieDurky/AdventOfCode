namespace Year2021
{
    public class Day09 : IDay
    {
        public Object Sol1(String input)
        {
            int[][] grid = input.Split('\n').Select(x => x.Trim().Select(y => y - '0').ToArray()).ToArray();
            List<int> lowPoints = new();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    int c = grid[j][i];
                    if (
                        (j < 1 || c < grid[j - 1][i]) &&
                        (j >= grid[i].Length - 1 || c < grid[j + 1][i]) &&
                        (i >= grid.Length - 1 || c < grid[j][i + 1]) &&
                        (i < 1 || c < grid[j][i - 1])
                        )
                    {
                        lowPoints.Add(c);
                    }
                }
            }

            return lowPoints.Sum() + lowPoints.Count;
        }

        public Object Sol2(String input)
        {
            int[][] grid = input.Split('\n').Select(x => x.Trim().Select(y => y - '0').ToArray()).ToArray();
            int width = grid.Length;
            int height = grid[0].Length;
            List<int[][]> basins = new();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int c = grid[j][i];
                    if (basins.Any(x => x.Any(y => y[0] == j && y[1] == i))) continue;
                    if (
                        c != 9 &&
                        (j < 1 || c < grid[j - 1][i]) &&
                        (j >= width - 1 || c < grid[j + 1][i]) &&
                        (i >= height - 1 || c < grid[j][i + 1]) &&
                        (i < 1 || c < grid[j][i - 1])
                        )
                    {
                        basins.Add(GetFlowPoints(grid, j, i, new List<int[]>()).ToArray());
                    }
                }
            }

            basins = basins.Select(Unique).ToList(); ;
            basins = basins.OrderBy(x => x.Length).ToList();
            basins.Reverse();

            return basins[0].Length * basins[1].Length * basins[2].Length;
            List<int[]> GetFlowPoints(Int32[][] inputs, int x, int y, List<int[]> oldFlowPoints)
            {
                List<int[]> flowPoints = new();

                if (x - 1 >= 0 && inputs[x - 1][y] != 9 && !oldFlowPoints.Any(a => a.SequenceEqual(new[] { x - 1, y }))) flowPoints.Add(new[] { x - 1, y });
                if (x + 1 < width && inputs[x + 1][y] != 9 && !oldFlowPoints.Any(a => a.SequenceEqual(new[] { x + 1, y }))) flowPoints.Add(new[] { x + 1, y });
                if (y - 1 >= 0 && inputs[x][y - 1] != 9 && !oldFlowPoints.Any(a => a.SequenceEqual(new[] { x, y - 1 }))) flowPoints.Add(new[] { x, y - 1 });
                if (y + 1 < height && inputs[x][y + 1] != 9 && !oldFlowPoints.Any(a => a.SequenceEqual(new[] { x, y + 1 }))) flowPoints.Add(new[] { x, y + 1 });

                List<int[]> basin = new();
                basin.AddRange(oldFlowPoints);
                basin.AddRange(flowPoints);
                basin.Add(new[] { x, y });
                basin = Unique(basin.ToArray()).ToList();
                foreach (int[] flowPoint in flowPoints)
                {
                    basin.AddRange(GetFlowPoints(inputs, flowPoint[0], flowPoint[1], basin));
                }
                return basin;
            }

            int[][] Unique(int[][] list)
            {
                List<int[]> output = new();
                foreach (int[] item in list)
                {
                    if (!output.Any(a => a.SequenceEqual(item)))
                    {
                        output.Add(item);
                    }
                }
                return output.ToArray();
            }
        }
    }
}