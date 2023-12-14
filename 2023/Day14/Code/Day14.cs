using System.Data;
using System.Drawing;
using System.Text;

namespace Year2023
{
    public class Day14 : IDay
    {
        public Object Sol1(String input)
        {
            String[] map = input.Split('\n');
            int sum = 0;

            for (int x = 0; x < map[0].Length; x++)
            {
                int roundRockCount = 0;

                for (int y2 = 0; y2 < map.Length; y2++)
                {
                    if (map[y2][x] == 'O') roundRockCount++;
                    if (map[y2][x] == '#') break;
                }

                for (int y3 = 1; roundRockCount > 0; y3++)
                {
                    sum += map.Length - y3 + 1;
                    roundRockCount--;
                }
            }
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (map[y][x] != '#') continue;

                    int roundRockCount = 0;

                    for (int y2 = y + 1; y2 < map.Length; y2++)
                    {
                        if (map[y2][x] == 'O') roundRockCount++;
                        if (map[y2][x] == '#') break;
                    }

                    for (int y3 = y + 1; roundRockCount > 0; y3++)
                    {
                        sum += map.Length - y3;
                        roundRockCount--;
                    }
                }
            }

            return sum;
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split('\n');
            int mapWidth = lines[0].Length;
            int mapHeight = lines.Length;
            Char[,] map = new Char[lines[0].Length, lines.Length];

            //Fill map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    map[x, y] = lines[y][x];
                }
            }

            List<Point> roundRocks = new();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (map[x, y] != '#') continue;
                    roundRocks.Add(new Point(x, y));
                }
            }

            List<Char[,]> mapHistory = new();
            bool started = false;

            Direction[] directionLookup = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];

            for (int i = 0; !mapHistory.Contains(map) || !started; i++)
            {
                Roll(directionLookup[i % 4]);
            }

            void Roll(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Right:
                        for (int y = 0; y < mapHeight; y++)
                        {
                            int roundRockCount = 0;
                            for (int x = 0; x < mapWidth; x++)
                            {
                                switch (map[x, y])
                                {
                                    //Remove and count round rocks
                                    case 'O':
                                        roundRockCount++;
                                        map[x, y] = '.';
                                        break;
                                    //Put round rocks back
                                    case '#':
                                        while (roundRockCount > 0)
                                        {
                                            roundRockCount--;
                                            map[x - roundRockCount - 1, y] = 'O';
                                        }
                                        break;
                                }
                            }
                            //Put round rocks at the end of the line back as well
                            while (roundRockCount > 0)
                            {
                                roundRockCount--;
                                map[mapWidth - roundRockCount - 1, y] = 'O';
                            }
                        }
                        break;
                    case Direction.Left:
                        for (int y = 0; y < mapHeight; y++)
                        {
                            int roundRockCount = 0;
                            for (int x = mapWidth - 1; x >= 0; x--)
                            {
                                switch (map[x, y])
                                {
                                    //Remove and count round rocks
                                    case 'O':
                                        roundRockCount++;
                                        map[x, y] = '.';
                                        break;
                                    //Put round rocks back
                                    case '#':
                                        while (roundRockCount > 0)
                                        {
                                            roundRockCount--;
                                            map[x + roundRockCount + 1, y] = 'O';
                                        }
                                        break;
                                }
                            }
                            //Put round rocks at the end of the line back as well
                            while (roundRockCount > 0)
                            {
                                roundRockCount--;
                                map[roundRockCount, y] = 'O';
                            }
                        }
                        break;
                    case Direction.Down:
                        for (int x = 0; x < mapWidth; x++)
                        {
                            int roundRockCount = 0;
                            for (int y = 0; y < mapHeight; y++)
                            {
                                switch (map[x, y])
                                {
                                    //Remove and count round rocks
                                    case 'O':
                                        roundRockCount++;
                                        map[x, y] = '.';
                                        break;
                                    //Put round rocks back
                                    case '#':
                                        while (roundRockCount > 0)
                                        {
                                            roundRockCount--;
                                            map[x, y - roundRockCount - 1] = 'O';
                                        }
                                        break;
                                }
                            }
                            //Put round rocks at the end of the line back as well
                            while (roundRockCount > 0)
                            {
                                roundRockCount--;
                                map[x, mapHeight - roundRockCount - 1] = 'O';
                            }
                        }
                        break;
                    case Direction.Up:
                        for (int x = 0; x < mapWidth; x++)
                        {
                            int roundRockCount = 0;
                            for (int y = mapHeight - 1; y >= 0; y--)
                            {
                                switch (map[x, y])
                                {
                                    //Remove and count round rocks
                                    case 'O':
                                        roundRockCount++;
                                        map[x, y] = '.';
                                        break;
                                    //Put round rocks back
                                    case '#':
                                        while (roundRockCount > 0)
                                        {
                                            roundRockCount--;
                                            map[x, y + roundRockCount + 1] = 'O';
                                        }
                                        break;
                                }
                            }
                            //Put round rocks at the end of the line back as well
                            while (roundRockCount > 0)
                            {
                                roundRockCount--;
                                map[x, roundRockCount] = 'O';
                            }
                        }
                        break;
                }
            }

            return "";
        }

        internal enum Direction
        {
            Up,
            Right,
            Down,
            Left,
        }
    }
}
