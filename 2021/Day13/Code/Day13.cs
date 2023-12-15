namespace Year2021
{
    public class Day13 : IDay
    {
        public object Sol1(string input)
        {
            //String[] split = input.Split("\r\n\r\n");
            //String[] paperLines = split[0].Split('\n').ToArray();
            //Int32[][] paper = paperLines.Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
            int[][] paper = input.Split("\r\n\r\n")[0].Split('\n').Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
            string[] folds = input.Split("\r\n\r\n")[1].Split('\n').Select(x => x.Replace("fold along ", "")).ToArray();

            char axis = folds[0].Split('=')[0][0];
            if (axis == 'x')
            {
                paper = Fold(paper, 0, int.Parse(folds[0].Split('=')[1]));
            }
            else if (axis == 'y')
            {
                paper = Fold(paper, 1, int.Parse(folds[1].Split('=')[1]));
            }

            paper = Unique(paper);

            return paper.Length;
        }

        public object Sol2(string input)
        {
            int[][] paper = input.Split("\r\n\r\n")[0].Split('\n').Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
            string[] folds = input.Split("\r\n\r\n")[1].Split('\n').Select(x => x.Replace("fold along ", "")).ToArray();
            //Console.WriteLine(paper.Length);
            foreach (string fold in folds)
            {
                char axis = fold.Split('=')[0][0];
                if (axis == 'x')
                {
                    paper = Fold(paper, 0, int.Parse(fold.Split('=')[1]));
                }
                else if (axis == 'y')
                {
                    paper = Fold(paper, 1, int.Parse(fold.Split('=')[1]));
                }
                else
                {
                    throw new Exception("AAAAAAAAAAAAAAAAAAAAAAA");
                }

            }
            paper = Unique(paper);
            char[,] visualPaper = new char[paper.MaxBy(x => x[0])![0] + 1, paper.MaxBy(x => x[1])![1] + 1];

            for (int x = 0; x < visualPaper.GetLength(0); x++)
            {
                for (int y = 0; y < visualPaper.GetLength(1); y++)
                {
                    visualPaper[x, y] = '.';
                }
            }

            foreach (int[] coord in paper)
            {
                visualPaper[coord[0], coord[1]] = '#';
            }

            string output = "";
            for (int y = 0; y < visualPaper.GetLength(1); y++)
            {
                string line = "";
                for (int x = 0; x < visualPaper.GetLength(0); x++)
                {
                    line += visualPaper[x, y];
                }

                output += line;
                if (y != visualPaper.GetLength(1) - 1) output += '\n';
            }

            return output;
        }

        public static int[][] Fold(int[][] paper, int axis, int coord)
        {
            for (int i = 0; i < paper.Length; i++)
            {
                if (paper[i][axis] > coord)
                {
                    paper[i][axis] = coord - (paper[i][axis] - coord);
                }
            }

            return paper;
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
