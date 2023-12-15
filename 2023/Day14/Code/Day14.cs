using System.Data;
using System.Drawing;
using HelperClasses;

namespace Year2023
{
    public class Day14 : IDay
    {
        public object Sol1(string input)
        {
            string[] map = input.Split('\n');
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

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');
            int mapWidth = lines[0].Length;
            int mapHeight = lines.Length;
            CharMap map = new CharMap(lines[0].Length, lines.Length);

            //Fill map
            map.Fill(lines);

            List<Point> roundRocks = new();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (map[x, y] != '#') continue;
                    roundRocks.Add(new Point(x, y));
                }
            }

            List<CharMap> mapHistory = new();

            int i = 0;
            while (true)
            {
                Roll(Direction.DirectionEnum.Up);
                Roll(Direction.DirectionEnum.Left);
                Roll(Direction.DirectionEnum.Down);
                Roll(Direction.DirectionEnum.Right);

                //checks whether there is already a map that looks exactly like map in mapHistory
                if (mapHistory.Contains(map)) break;

                mapHistory.Add((CharMap)map.Clone());
                i++;
            }

            //Take first map in history that equals to map
            int loopStart = mapHistory.TakeWhile(x => !x.Equals(map)).Count();
            int loopEnd = i;
            int loopLength = loopEnd - loopStart;

            CharMap finalMap = mapHistory[loopStart + ((1_000_000_000 - loopStart) % loopLength) - 1];

            Console.WriteLine("Final map:");
            finalMap.Print();

            int sum = 0;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (finalMap[x, y] == 'O')
                    {
                        sum += mapHeight - y;
                    }
                }
            }

            return sum;

            void Roll(Direction.DirectionEnum direction)
            {
                switch (direction)
                {
                    case Direction.DirectionEnum.Right:
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
                    case Direction.DirectionEnum.Left:
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
                    case Direction.DirectionEnum.Down:
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
                    case Direction.DirectionEnum.Up:
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
        }
    }
}
