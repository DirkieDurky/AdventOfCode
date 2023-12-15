using System.ComponentModel.Design;
using System.Globalization;

namespace Year2021
{
    public class Day11 : IDay
    {
        public object Sol1(string input)
        {
            int flashCount = 0;

            int[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();

            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        grid[x][y]++;
                    }
                }

                bool active = true;
                List<(int, int)> totalFlashedOctopusses = new();
                while (active)
                {
                    active = false;
                    List<(int, int)> flashedOctopusses = new();
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if (grid[x][y] > 9 && !totalFlashedOctopusses.Any(a => a.Item1.Equals(x) && a.Item2.Equals(y)))
                            //if (grid[x][y] > 9 && !flashedOctopusses.Contains((x,y)))
                            {
                                flashCount++;
                                if (x > 0 && y > 0) grid[x - 1][y - 1]++;
                                if (x > 0) grid[x - 1][y]++;
                                if (y > 0) grid[x][y - 1]++;
                                if (y < 9) grid[x][y + 1]++;
                                if (y < 9 && x < 9) grid[x + 1][y + 1]++;
                                if (x < 9) grid[x + 1][y]++;
                                if (x > 0 && y < 9) grid[x - 1][y + 1]++;
                                if (x < 9 && y > 0) grid[x + 1][y - 1]++;
                                active = true;
                                //grid[x][y] = 0;
                                totalFlashedOctopusses.Add((x, y));
                            }
                        }
                    }
                }

                foreach ((int, int) flashedOctopus in totalFlashedOctopusses)
                {
                    grid[flashedOctopus.Item1][flashedOctopus.Item2] = 0;
                }
            }

            return flashCount;
        }

        public object Sol2(string input)
        {
            int[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();
            bool found = false;
            int i = 0;
            while (!found)
            {
                int flashCount = 0;
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        grid[x][y]++;
                    }
                }

                bool active = true;
                List<(int, int)> totalFlashedOctopusses = new();
                while (active)
                {
                    active = false;
                    List<(int, int)> flashedOctopusses = new();
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if (grid[x][y] > 9 && !totalFlashedOctopusses.Any(a => a.Item1.Equals(x) && a.Item2.Equals(y)))
                            //if (grid[x][y] > 9 && !flashedOctopusses.Contains((x,y)))
                            {
                                flashCount++;
                                if (x > 0 && y > 0) grid[x - 1][y - 1]++;
                                if (x > 0) grid[x - 1][y]++;
                                if (y > 0) grid[x][y - 1]++;
                                if (y < 9) grid[x][y + 1]++;
                                if (y < 9 && x < 9) grid[x + 1][y + 1]++;
                                if (x < 9) grid[x + 1][y]++;
                                if (x > 0 && y < 9) grid[x - 1][y + 1]++;
                                if (x < 9 && y > 0) grid[x + 1][y - 1]++;
                                active = true;
                                //grid[x][y] = 0;
                                totalFlashedOctopusses.Add((x, y));
                            }
                        }
                    }
                }

                foreach ((int, int) flashedOctopus in totalFlashedOctopusses)
                {
                    grid[flashedOctopus.Item1][flashedOctopus.Item2] = 0;
                }

                if (flashCount >= 100) found = true;
                i++;
            }
            return i;
        }
    }
}
