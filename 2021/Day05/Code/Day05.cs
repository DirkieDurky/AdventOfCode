
namespace Year2021
{
    public class Day05 : IDay
    {
        public object Sol1(string input)
        {
            int[][][] lines = input.Split('\n').Select(x => x.Split(" -> ").Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).ToArray()).ToArray();
            int[,] grid = new int[1000, 1000];

            foreach (int[][] line in lines)
            {
                if (line[0][0] != line[1][0] && line[0][1] != line[1][1]) continue;
                int x = line[0][0];
                int y = line[0][1];
                int xIncrementAmount = line[1][0] < line[0][0] ? -1 : line[1][0] > line[0][0] ? 1 : 0;
                int yIncrementAmount = line[1][1] < line[0][1] ? -1 : line[1][1] > line[0][1] ? 1 : 0;
                do
                {
                    grid[x, y]++;

                    x += xIncrementAmount;
                    y += yIncrementAmount;
                } while (x - xIncrementAmount != line[1][0] || y - yIncrementAmount != line[1][1]);
            }

            // for (int i = 0; i < 10; i++)
            // {
            //     int[] line = new int[10];
            //     for (int j = 0; j < 10; j++)
            //     {
            //         line[j] = grid[j, i];
            //     }
            //     Console.WriteLine(String.Join(',', line));
            // }
            return HelperClasses.HelperFunctions.Flatten(grid).Where(x => x > 1).ToArray().Length;
        }

        public object Sol2(string input)
        {
            int[][][] lines = input.Split('\n').Select(x => x.Split(" -> ").Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).ToArray()).ToArray();
            int[,] grid = new int[1000, 1000];

            foreach (int[][] line in lines)
            {
                int x = line[0][0];
                int y = line[0][1];
                int xIncrementAmount = line[1][0] < line[0][0] ? -1 : line[1][0] > line[0][0] ? 1 : 0;
                int yIncrementAmount = line[1][1] < line[0][1] ? -1 : line[1][1] > line[0][1] ? 1 : 0;
                do
                {
                    grid[x, y]++;

                    x += xIncrementAmount;
                    y += yIncrementAmount;
                } while (x - xIncrementAmount != line[1][0] || y - yIncrementAmount != line[1][1]);
            }

            return HelperClasses.HelperFunctions.Flatten(grid).Where(x => x > 1).ToArray().Length;
        }
    }
}
