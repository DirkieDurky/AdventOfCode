using Advent_of_Code.HelperClasses;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Year2024
{
    public class Day14 : IDay
    {
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');
            List<Robot> robots = new();
            //Test
            //int width = 11;
            //int height = 7;
            //Original
            int width = 101;
            int height = 103;

            foreach (string robot in lines)
            {
                Match match = Regex.Match(robot, "p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)");
                Position pos = new Position(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                Position velocity = new Position(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                robots.Add(new Robot(pos, velocity));
            }

            Print(robots, width, height);
            Console.WriteLine();

            foreach (Robot robot in robots)
            {
                int x = robot.Pos.X + robot.Velocity.X * 100;
                int y = robot.Pos.Y + robot.Velocity.Y * 100;

                if (x >= 0)
                {
                    x %= width;
                }
                else
                {
                    x += (int)Math.Abs(Math.Floor(x / (double)width) * width);
                }

                if (y >= 0)
                {
                    y %= height;
                }
                else
                {
                    y += (int)Math.Abs(Math.Floor(y / (double)height) * height);
                }

                robot.Pos.X = x;
                robot.Pos.Y = y;
            }

            Print(robots, width, height);
            //0: Top left
            //1: Top right
            //2: Bottom left
            //3: Bottom right
            int[] robotNumberPerQuadrant = new int[4];
            foreach (Robot robot in robots)
            {
                if (robot.Pos.X < 0 || robot.Pos.X >= width
                    || robot.Pos.Y < 0 || robot.Pos.Y >= height)
                {
                    throw new Exception("robot is out of bounds");
                }
                //Console.WriteLine($"X:{robot.Pos.X} Y:{robot.Pos.Y}");
                if (robot.Pos.X < width / 2)
                {
                    if (robot.Pos.Y < height / 2)
                    {
                        robotNumberPerQuadrant[0]++;
                    }
                    else if (robot.Pos.Y > height / 2)
                    {
                        robotNumberPerQuadrant[2]++;
                    }
                }
                else if (robot.Pos.X > width / 2)
                {
                    if (robot.Pos.Y < height / 2)
                    {
                        robotNumberPerQuadrant[1]++;
                    }
                    else if (robot.Pos.Y > height / 2)
                    {
                        robotNumberPerQuadrant[3]++;
                    }
                }
            }

            foreach (int quadrant in robotNumberPerQuadrant)
            {
                Console.WriteLine(quadrant);
            }
            return robotNumberPerQuadrant.Aggregate(1, (a, b) => a * b);
        }

        private struct Robot(Position pos, Position velocity)
        {
            public Position Pos = pos;
            public Position Velocity = velocity;
        }

        private void Print(List<Robot> robots, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                Console.Write($"{y}: ");
                for (int x = 0; x < width; x++)
                {
                    int count = robots.Count(r => r.Pos.X == x && r.Pos.Y == y);
                    if (count > 0) Console.Write(count);
                    else Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');
            List<Robot> robots = new();
            //Test
            //int width = 11;
            //int height = 7;
            //Original
            int width = 101;
            int height = 103;

            foreach (string robot in lines)
            {
                Match match = Regex.Match(robot, "p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)");
                Position pos = new Position(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                Position velocity = new Position(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                robots.Add(new Robot(pos, velocity));
            }

            int moveCount = 0;
            while (true)
            {
                FindContender();

                ConsoleKey key = Console.ReadKey().Key;
                Console.Clear();
            }

            void FindContender()
            {
                while (true)
                {
                    Move(true);
                    moveCount++;
                    if (moveCount % 10000 == 0) Console.WriteLine(moveCount);

                    int consecutiveEqualCount = 0;

                    List<int> xValuesOnLastYAxis = new();
                    for (int y = 0; y < height; y++)
                    {
                        List<int> xValuesOnYAxis = robots.Where(r => r.Pos.Y == y).Select(r => r.Pos.X).ToList();
                        if (xValuesOnLastYAxis.Any() && xValuesOnYAxis.Count > xValuesOnLastYAxis.Count)
                        {
                            consecutiveEqualCount++;
                            if (consecutiveEqualCount >= 6)
                            {
                                Print(robots, width, height);
                                Console.WriteLine($"MoveCount:{moveCount}. Line {y - 1} and {y} are equal");
                                return;
                            }
                        }
                        else
                        {
                            consecutiveEqualCount = 0;
                        }
                        xValuesOnLastYAxis = xValuesOnYAxis;
                    }
                }
            }

            void Move(bool forwards)
            {
                foreach (Robot robot in robots)
                {
                    int x = robot.Pos.X + robot.Velocity.X * (forwards ? 1 : -1);
                    int y = robot.Pos.Y + robot.Velocity.Y * (forwards ? 1 : -1);

                    if (x >= 0)
                    {
                        x %= width;
                    }
                    else
                    {
                        x += (int)Math.Abs(Math.Floor(x / (double)width) * width);
                    }

                    if (y >= 0)
                    {
                        y %= height;
                    }
                    else
                    {
                        y += (int)Math.Abs(Math.Floor(y / (double)height) * height);
                    }

                    robot.Pos.X = x;
                    robot.Pos.Y = y;
                }
            }
        }

        public object FrameSkipper(string input)
        {
            string[] lines = input.Split('\n');
            List<Robot> robots = new();
            //Test
            //int width = 11;
            //int height = 7;
            //Original
            int width = 101;
            int height = 103;

            foreach (string robot in lines)
            {
                Match match = Regex.Match(robot, "p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)");
                Position pos = new Position(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                Position velocity = new Position(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                robots.Add(new Robot(pos, velocity));
            }

            Print(robots, width, height);

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.OemPeriod)
                {
                    Move(true);
                }
                else if (key == ConsoleKey.OemComma)
                {
                    Move(false);
                }
            }

            void Move(bool forwards)
            {
                Console.Clear();

                foreach (Robot robot in robots)
                {
                    int x = robot.Pos.X + robot.Velocity.X * (forwards ? 1 : -1);
                    int y = robot.Pos.Y + robot.Velocity.Y * (forwards ? 1 : -1);

                    if (x >= 0)
                    {
                        x %= width;
                    }
                    else
                    {
                        x += (int)Math.Abs(Math.Floor(x / (double)width) * width);
                    }

                    if (y >= 0)
                    {
                        y %= height;
                    }
                    else
                    {
                        y += (int)Math.Abs(Math.Floor(y / (double)height) * height);
                    }

                    robot.Pos.X = x;
                    robot.Pos.Y = y;
                }

                Print(robots, width, height);
            }
        }
    }
}