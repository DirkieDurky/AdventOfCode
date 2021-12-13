using System.Text.RegularExpressions;

namespace Year2021
{
    public class Day04 : IDay
    {
        public Object Sol1(String input)
        {
            String[] lines = input.Split("\n").ToArray();
            Int32[] numbers = lines[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
            List<Int32[,]> boards = new();
            {
                Int32[,] board = new Int32[5, 5];
                for (Int32 i = 2; i < lines.Length; i++)
                {
                    if ((i - 1) % 6 != 0)
                    {
                        // int[] input = Regex.Split(inputs[i], " +").Select(x => int.Parse(x)).ToArray();
                        Int32[] ints = Regex.Split(lines[i].Trim(), " +").Select(x => Int32.Parse(x)).ToArray();
                        for (Int32 k = 0; k < 5; k++)
                        {
                            board[k, ((i - 1) % 6) - 1] = ints[k];
                        }
                    }
                    else
                    {
                        boards.Add(board);
                        board = new Int32[5, 5];
                    }
                }
                boards.Add(board);
            }
            List<Int32> calledNumbers = new();

            // for (int i = 0; i < 5; i++)
            // {
            //     int[] line = new int[5];
            //     for (int j = 0; j < 5; j++)
            //     {
            //         line[j] = boards[0][j, i];
            //     }
            //     Console.WriteLine(String.Join(',', line));
            // }

            foreach (Int32 number in numbers)
            {
                calledNumbers.Add(number);
                foreach (Int32[,] board in boards)
                {
                    if (matchFound(board))
                    {
                        Int32 total = 0;
                        foreach (Int32 cell in board)
                        {
                            if (!calledNumbers.Contains(cell)) total += cell;
                        }
                        // return $"{total} {number}";
                        return total * number;
                    }
                }
            }
            return "";

            Boolean matchFound(Int32[,] board)
            {
                for (Int32 i = 0; i < 5; i++)
                {
                    Int32 matchesInRow = 0;
                    Int32[] line = new Int32[5];
                    for (Int32 j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[j, i])) matchesInRow++;
                        line[j] = board[j, i];
                    }
                    // Console.WriteLine(String.Join(' ', line));
                    if (matchesInRow == 5) return true;
                }
                for (Int32 i = 0; i < 5; i++)
                {
                    Int32 matchesInColumn = 0;
                    for (Int32 j = 0; j < 5; j++)
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
            String[] lines = input.Split("\n").ToArray();
            Int32[] numbers = lines[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
            List<Int32[,]> boards = new();
            List<(Int32[,], Int32, Int32)> completedBoards = new();
            {
                Int32[,] board = new Int32[5, 5];
                for (Int32 i = 2; i < lines.Length; i++)
                {
                    if ((i - 1) % 6 != 0)
                    {
                        // int[] input = Regex.Split(inputs[i], " +").Select(x => int.Parse(x)).ToArray();
                        Int32[] ints = Regex.Split(lines[i].Trim(), " +").Select(Int32.Parse).ToArray();
                        for (Int32 k = 0; k < 5; k++)
                        {
                            board[k, ((i - 1) % 6) - 1] = ints[k];
                        }
                    }
                    else
                    {
                        boards.Add(board);
                        board = new Int32[5, 5];
                    }
                }
                boards.Add(board);
            }
            List<Int32> calledNumbers = new();

            // for (int i = 0; i < 5; i++)
            // {
            //     int[] line = new int[5];
            //     for (int j = 0; j < 5; j++)
            //     {
            //         line[j] = boards[0][j, i];
            //     }
            //     Console.WriteLine(String.Join(',', line));
            // }

            Int32 lastSum = 0;
            Int32 lastNumber = 0;
            foreach (Int32 number in numbers)
            {
                calledNumbers.Add(number);
                foreach (Int32[,] board in boards)
                {
                    if (completedBoards.Any(x => x.Item1 == board)) continue;
                    if (matchFound(board))
                    {
                        Int32 total = 0;
                        foreach (Int32 cell in board)
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

            Boolean matchFound(Int32[,] board)
            {
                for (Int32 i = 0; i < 5; i++)
                {
                    Int32 matchesInRow = 0;
                    Int32[] line = new Int32[5];
                    for (Int32 j = 0; j < 5; j++)
                    {
                        if (calledNumbers.Contains(board[j, i])) matchesInRow++;
                        line[j] = board[j, i];
                    }
                    // Console.WriteLine(String.Join(' ', line));
                    if (matchesInRow == 5) return true;
                }
                for (Int32 i = 0; i < 5; i++)
                {
                    Int32 matchesInColumn = 0;
                    for (Int32 j = 0; j < 5; j++)
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