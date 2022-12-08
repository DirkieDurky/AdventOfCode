namespace Year2022
{
    public class Day08 : IDay
    {
        public Object Sol1(String input)
        {
            Int32[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();
            Int32 maxTreeHeight = -1;
            Int32 currentTreeHeight;

            HashSet<Tree> visibleTreeList = new();

            //Top
            for (Int32 i = 1; i < grid.Length - 1; i++)
            {
                String line = "";
                maxTreeHeight = -1;
                for (Int32 j = 0; j < grid[i].Length; j++)
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
            for (Int32 i = 1; i < grid.Length - 1; i++)
            {
                String line = "";
                maxTreeHeight = -1;
                for (Int32 j = grid[i].Length - 1; j > 0; j--)
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
            for (Int32 i = 1; i < grid.Length - 1; i++)
            {
                String line = "";
                maxTreeHeight = -1;
                for (Int32 j = 0; j < grid[i].Length; j++)
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
            for (Int32 i = 1; i < grid.Length - 1; i++)
            {
                String line = "";
                maxTreeHeight = -1;
                for (Int32 j = grid[i].Length - 1; j > 0; j--)
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

        public Object Sol2(String input)
        {
            Int32[][] grid = input.Split('\n').Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();

            List<Int32> scores = new();

            //For each tree
            for (Int32 i = 0; i < grid.Length; i++)
            {
                for (Int32 j = 0; j < grid[i].Length; j++)
                {
                    Int32 currentTreeHeight = grid[j][i];
                    // Console.WriteLine($"Current tree: {j}{i} ({currentTreeHeight})");
                    Int32 totalScore = 1;

                    Int32 score;
                    Int32 current;
                    Int32 minTreeHeight;

                    //Top
                    score = 0;
                    minTreeHeight = currentTreeHeight;
                    for (Int32 k = 1; j - k >= 0; k++)
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
                    for (Int32 k = 1; j + k < grid.Length; k++)
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
                    for (Int32 k = 1; i - k >= 0; k++)
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
                    for (Int32 k = 1; i + k < grid[i].Length; k++)
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