
namespace Year2021
{
    public class Day05 : IDay
    {
        public Object Sol1(String input)
        {
            Int32[][][] lines = input.Split('\n').Select(x => x.Split(" -> ").Select(x => x.Split(',').Select(x => Int32.Parse(x)).ToArray()).ToArray()).ToArray();
            Int32[,] grid = new Int32[1000, 1000];

            foreach (Int32[][] line in lines)
            {
                if (line[0][0] != line[1][0] && line[0][1] != line[1][1]) continue;
                Int32 x = line[0][0];
                Int32 y = line[0][1];
                Int32 xIncrementAmount = line[1][0] < line[0][0] ? -1 : line[1][0] > line[0][0] ? 1 : 0;
                Int32 yIncrementAmount = line[1][1] < line[0][1] ? -1 : line[1][1] > line[0][1] ? 1 : 0;
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

        public Object Sol2(String input)
        {
            Int32[][][] lines = input.Split('\n').Select(x => x.Split(" -> ").Select(x => x.Split(',').Select(x => Int32.Parse(x)).ToArray()).ToArray()).ToArray();
            Int32[,] grid = new Int32[1000, 1000];

            foreach (Int32[][] line in lines)
            {
                Int32 x = line[0][0];
                Int32 y = line[0][1];
                Int32 xIncrementAmount = line[1][0] < line[0][0] ? -1 : line[1][0] > line[0][0] ? 1 : 0;
                Int32 yIncrementAmount = line[1][1] < line[0][1] ? -1 : line[1][1] > line[0][1] ? 1 : 0;
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
