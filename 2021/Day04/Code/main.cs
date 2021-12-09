using System.Text.RegularExpressions;

namespace Year2021
{
    public class Day04 : IDay
    {
        public Object Sol1(String input)
        {
            string[] lines = input.Split("\n").ToArray();
            int[] numbers = lines[0].Split(',').Select(x => int.Parse(x)).ToArray();
            List<int[,]> boards = new();
            {
                int[,] board = new int[5, 5];
                for (int i = 2; i < lines.Length; i++)
                {
                    if ((i - 1) % 6 != 0)
                    {
                        // int[] input = Regex.Split(inputs[i], " +").Select(x => int.Parse(x)).ToArray();
                        int[] ints = Regex.Split(lines[i].Trim(), " +").Select(x => int.Parse(x)).ToArray();
                        for (int k = 0; k < 5; k++)
                        {
                            board[k, ((i - 1) % 6) - 1] = ints[k];
                        }
                    }
                    else
                    {
                        boards.Add(board);
                        board = new int[5, 5];
                    }
                }
                boards.Add(board);
            }
            List<int> calledNumbers = new();

            // for (int i = 0; i < 5; i++)
            // {
            //     int[] line = new int[5];
            //     for (int j = 0; j < 5; j++)
            //     {
            //         line[j] = boards[0][j, i];
            //     }
            //     Console.WriteLine(String.Join(',', line));
            // }

            foreach (int number in numbers)
            {
                calledNumbers.Add(number);
                foreach (int[,] board in boards)
                {
                    if (matchFound(board))
                    {
                        int total = 0;
                        foreach (int cell in board)
                        {
                            if (!calledNumbers.Contains(cell)) total += cell;
                        }
                        // return $"{total} {number}";
                        return total * number;
                    }
                }
            }
            return "";

            bool matchFound(int[,] board)
            {
                for (int i = 0; i < 5; i++)
                {
                    int matchesInRow = 0;
                    int[] line = new int[5];
                    for (int j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[j, i])) matchesInRow++;
                        line[j] = board[j, i];
                    }
                    // Console.WriteLine(String.Join(' ', line));
                    if (matchesInRow == 5) return true;
                }
                for (int i = 0; i < 5; i++)
                {
                    int matchesInColumn = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[i, j])) matchesInColumn++;
                    }
                    if (matchesInColumn == 5) return true;
                }
                return false;
            }
        }

        public Object Sol2(String input)
        {
            string[] lines = input.Split("\n").ToArray();
            int[] numbers = lines[0].Split(',').Select(x => int.Parse(x)).ToArray();
            List<int[,]> boards = new();
            List<(int[,], int, int)> completedBoards = new();
            {
                int[,] board = new int[5, 5];
                for (int i = 2; i < lines.Length; i++)
                {
                    if ((i - 1) % 6 != 0)
                    {
                        // int[] input = Regex.Split(inputs[i], " +").Select(x => int.Parse(x)).ToArray();
                        int[] ints = Regex.Split(lines[i].Trim(), " +").Select(int.Parse).ToArray();
                        for (int k = 0; k < 5; k++)
                        {
                            board[k, ((i - 1) % 6) - 1] = ints[k];
                        }
                    }
                    else
                    {
                        boards.Add(board);
                        board = new int[5, 5];
                    }
                }
                boards.Add(board);
            }
            List<int> calledNumbers = new();

            // for (int i = 0; i < 5; i++)
            // {
            //     int[] line = new int[5];
            //     for (int j = 0; j < 5; j++)
            //     {
            //         line[j] = boards[0][j, i];
            //     }
            //     Console.WriteLine(String.Join(',', line));
            // }

            int lastSum = 0;
            int lastNumber = 0;
            foreach (int number in numbers)
            {
                calledNumbers.Add(number);
                foreach (int[,] board in boards)
                {
                    if (completedBoards.Any(x => x.Item1 == board)) continue;
                    if (matchFound(board))
                    {
                        int total = 0;
                        foreach (int cell in board)
                        {
                            if (!calledNumbers.Contains(cell)) total += cell;
                        }
                        lastSum = total;
                        lastNumber = number;
                        completedBoards.Add((board, lastSum, lastNumber));
                    }
                }
            }
            // return $"{completedBoards.Last().Item2} {completedBoards.Last().Item3}";
            return completedBoards.Last().Item2 * completedBoards.Last().Item3;

            bool matchFound(int[,] board)
            {
                for (int i = 0; i < 5; i++)
                {
                    int matchesInRow = 0;
                    int[] line = new int[5];
                    for (int j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[j, i])) matchesInRow++;
                        line[j] = board[j, i];
                    }
                    // Console.WriteLine(String.Join(' ', line));
                    if (matchesInRow == 5) return true;
                }
                for (int i = 0; i < 5; i++)
                {
                    int matchesInColumn = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[i, j])) matchesInColumn++;
                    }
                    if (matchesInColumn == 5) return true;
                }
                return false;
            }
        }
    }
}