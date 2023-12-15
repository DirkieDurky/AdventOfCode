namespace Year2022
{
    public class Day08 : IDay
    {
        public object Sol1(string input)
        {
            int[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();
            int maxTreeHeight = -1;
            int currentTreeHeight;

            HashSet<Tree> visibleTreeList = new();

            //Top
            for (int i = 1; i < grid.Length - 1; i++)
            {
                string line = "";
                maxTreeHeight = -1;
                for (int j = 0; j < grid[i].Length; j++)
                {
                    currentTreeHeight = grid[j][i];
                    if (currentTreeHeight > maxTreeHeight)
                    {
                        visibleTreeList.Add(new Tree(i, j));
                        maxTreeHeight = currentTreeHeight;
                        line += currentTreeHeight;
                    }
                }
                // Console.WriteLine(line);
            }
            // Console.WriteLine();

            //Bottom
            for (int i = 1; i < grid.Length - 1; i++)
            {
                string line = "";
                maxTreeHeight = -1;
                for (int j = grid[i].Length - 1; j > 0; j--)
                {
                    currentTreeHeight = grid[j][i];
                    if (currentTreeHeight > maxTreeHeight)
                    {
                        visibleTreeList.Add(new Tree(i, j));
                        maxTreeHeight = currentTreeHeight;
                        line += currentTreeHeight;
                    }
                }
                // Console.WriteLine(line);
            }
            // Console.WriteLine();

            //Left
            for (int i = 1; i < grid.Length - 1; i++)
            {
                string line = "";
                maxTreeHeight = -1;
                for (int j = 0; j < grid[i].Length; j++)
                {
                    currentTreeHeight = grid[i][j];
                    if (currentTreeHeight > maxTreeHeight)
                    {
                        visibleTreeList.Add(new Tree(j, i));
                        maxTreeHeight = currentTreeHeight;
                        line += currentTreeHeight;
                    }
                }
                // Console.WriteLine(line);
            }
            // Console.WriteLine();

            //Right
            for (int i = 1; i < grid.Length - 1; i++)
            {
                string line = "";
                maxTreeHeight = -1;
                for (int j = grid[i].Length - 1; j > 0; j--)
                {
                    currentTreeHeight = grid[i][j];
                    if (currentTreeHeight > maxTreeHeight)
                    {
                        visibleTreeList.Add(new Tree(j, i));
                        maxTreeHeight = currentTreeHeight;
                        line += currentTreeHeight;
                    }
                }
                // Console.WriteLine(line);
            }
            // Console.WriteLine();

            // foreach (Tree tree in visibleTreeList)
            // {
            //     Console.WriteLine($"x: {tree.X} y: {tree.Y}");
            // }

            return visibleTreeList.Count + 4;
        }

        public object Sol2(string input)
        {
            int[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();

            List<int> scores = new();

            //For each tree
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    int currentTreeHeight = grid[j][i];
                    // Console.WriteLine($"Current tree: {j}{i} ({currentTreeHeight})");
                    int totalScore = 1;

                    int score;
                    int current;
                    int minTreeHeight;

                    //Top
                    score = 0;
                    minTreeHeight = currentTreeHeight;
                    for (int k = 1; j - k >= 0; k++)
                    {
                        current = grid[j - k][i];
                        if (current < currentTreeHeight)
                        {
                            score++;
                        }
                        else
                        {
                            score++;
                            break;
                        }
                    }
                    // Console.WriteLine($"Score top: {score}");
                    totalScore *= score;

                    //Bottom
                    score = 0;
                    minTreeHeight = currentTreeHeight;
                    for (int k = 1; j + k < grid.Length; k++)
                    {
                        current = grid[j + k][i];
                        if (current < currentTreeHeight)
                        {
                            score++;
                        }
                        else
                        {
                            score++;
                            break;
                        }
                    }
                    // Console.WriteLine($"Score bottom: {score}");
                    totalScore *= score;

                    //Left
                    score = 0;
                    minTreeHeight = currentTreeHeight;
                    for (int k = 1; i - k >= 0; k++)
                    {
                        current = grid[j][i - k];
                        if (current < currentTreeHeight)
                        {
                            score++;
                        }
                        else
                        {
                            score++;
                            break;
                        }
                    }
                    // Console.WriteLine($"Score left: {score}");
                    totalScore *= score;

                    //Right
                    score = 0;
                    minTreeHeight = currentTreeHeight;
                    for (int k = 1; i + k < grid[i].Length; k++)
                    {
                        current = grid[j][i + k];
                        if (current < currentTreeHeight)
                        {
                            score++;
                        }
                        else
                        {
                            score++;
                            break;
                        }
                    }
                    // Console.WriteLine($"Score right: {score}");
                    totalScore *= score;
                    // Console.WriteLine($"Total score: {totalScore}");
                    scores.Add(totalScore);
                }
            }

            return scores.Max();
        }
    }
}
