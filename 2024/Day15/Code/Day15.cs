using Advent_of_Code.HelperClasses;

namespace Year2024
{
    public class Day15 : IDay
    {
        public object Sol1(string input)
        {
            string[] split = input.Split("\n\n");
            CharMap map = new(split[0].Split('\n'));
            Direction[] moves = split[1].Replace("\n", "").Select(x => x switch
            {
                '^' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Up),
                'v' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Down),
                '<' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Left),
                '>' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Right),
                _ => throw new InvalidDataException("Invalid move given")
            }).ToArray();

            Position robotPos = FindRobot(map);

            foreach (Direction move in moves)
            {
                Position nextPosition = robotPos + move;
                if (map[nextPosition] == '.')
                {
                    MoveRobot(move);
                    continue;
                }
                else if (map[nextPosition] == '#') continue;

                Position? nextFreeCell = NextFreeCellInDirection(map, robotPos, move);
                if (nextFreeCell is null) continue;

                map[nextFreeCell] = 'O';
                MoveRobot(move);
            }

            //map.Print();

            int total = 0;
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map[x, y] != 'O') continue;
                    total += y * 100 + x;
                }
            }

            return total;

            void MoveRobot(Direction direction)
            {
                Position newPos = robotPos + direction;
                map[robotPos] = '.';
                robotPos = newPos;
                map[newPos] = '@';
            }
        }

        public object Sol2(string input)
        {
            string[] split = input.Split("\n\n");
            CharMap grid = new(split[0].Split('\n'));
            CharMap map = new(grid.Width * 2, grid.Height);

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    switch (grid[x, y])
                    {
                        case '#':
                            map[x * 2, y] = '#';
                            map[x * 2 + 1, y] = '#';
                            break;
                        case 'O':
                            map[x * 2, y] = '[';
                            map[x * 2 + 1, y] = ']';
                            break;
                        case '.':
                            map[x * 2, y] = '.';
                            map[x * 2 + 1, y] = '.';
                            break;
                        case '@':
                            map[x * 2, y] = '@';
                            map[x * 2 + 1, y] = '.';
                            break;
                    }
                }
            }

            Direction[] moves = split[1].Replace("\n", "").Select(x => x switch
            {
                '^' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Up),
                'v' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Down),
                '<' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Left),
                '>' => Direction.FindDirectionByDirectionEnum(Direction.DirectionEnum.Right),
                _ => throw new InvalidDataException("Invalid move given")
            }).ToArray();

            Position robotPos = FindRobot(map);

            foreach (Direction move in moves)
            {
                Position nextPosition = robotPos + move;
                if (map[nextPosition] == '.')
                {
                    MoveRobot(move);
                    continue;
                }
                else if (map[nextPosition] == '#') continue;

                if (CanMoveBox(nextPosition, move))
                {
                    MoveBox(nextPosition, move);
                    MoveRobot(move);
                }
            }

            //map.Print();

            int total = 0;
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map[x, y] != '[') continue;
                    total += y * 100 + x;
                }
            }

            return total;

            void MoveRobot(Direction direction)
            {
                Position newPos = robotPos + direction;
                map[robotPos] = '.';
                robotPos = newPos;
                map[newPos] = '@';

                //map.Print();
            }

            Position GetOtherBoxHalf(Position boxPos)
            {
                return map[boxPos] switch
                {
                    '[' => new Position(boxPos.X + 1, boxPos.Y),
                    ']' => new Position(boxPos.X - 1, boxPos.Y),
                    _ => throw new InvalidDataException("Given boxPos is not a box")
                };
            }

            bool CanMoveBox(Position boxPos, Direction direction)
            {
                Position nextPosition = boxPos + direction;
                if (direction.DirectionE == Direction.DirectionEnum.Left || direction.DirectionE == Direction.DirectionEnum.Right)
                    return CanMoveBoxHalf(nextPosition, direction);

                Position otherHalfPosition = GetOtherBoxHalf(boxPos);
                return CanMoveBoxHalf(boxPos, direction) && CanMoveBoxHalf(otherHalfPosition, direction);
            }

            bool CanMoveBoxHalf(Position boxPos, Direction direction)
            {
                Position nextPosition = boxPos + direction;

                if (map[nextPosition] == '.') return true;
                if (map[nextPosition] == '#') return false;

                if (direction.DirectionE == Direction.DirectionEnum.Left || direction.DirectionE == Direction.DirectionEnum.Right)
                    return CanMoveBoxHalf(nextPosition, direction);

                Position nextPositionOtherHalf = GetOtherBoxHalf(nextPosition);
                return CanMoveBoxHalf(nextPosition, direction) && CanMoveBoxHalf(nextPositionOtherHalf, direction);
            }

            void MoveBox(Position boxPos, Direction direction)
            {
                Position otherHalfPos = GetOtherBoxHalf(boxPos);

                if (direction.DirectionE == Direction.DirectionEnum.Left)
                {
                    if (map[boxPos] == '[')
                    {
                        MoveBoxHalf(boxPos, direction);
                        MoveBoxHalf(otherHalfPos, direction, true);
                    }
                    else
                    {
                        MoveBoxHalf(otherHalfPos, direction);
                        MoveBoxHalf(boxPos, direction, true);
                    }
                }
                else if (direction.DirectionE == Direction.DirectionEnum.Right)
                {
                    if (map[boxPos] == ']')
                    {
                        MoveBoxHalf(boxPos, direction);
                        MoveBoxHalf(otherHalfPos, direction, true);
                    }
                    else
                    {
                        MoveBoxHalf(otherHalfPos, direction);
                        MoveBoxHalf(boxPos, direction, true);
                    }
                }
                else
                {
                    MoveBoxHalf(boxPos, direction);
                    MoveBoxHalf(otherHalfPos, direction);
                }
            }

            void MoveBoxHalf(Position boxPos, Direction direction, bool ignoreCheck = false)
            {
                Position nextPos = boxPos + direction;

                if (ignoreCheck)
                {
                    map[nextPos] = map[boxPos];
                    return;
                }

                switch (map[nextPos])
                {
                    case '.':
                        map[nextPos] = map[boxPos];
                        map[boxPos] = '.';
                        break;
                    case '[':
                    case ']':
                        MoveBox(nextPos, direction);
                        map[nextPos] = map[boxPos];
                        map[boxPos] = '.';
                        break;
                    default: throw new InvalidDataException("Can't move boxhalf");
                }
            }
        }

        private Position FindRobot(CharMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map[x, y] == '@')
                    {
                        return new Position(x, y);
                    }
                }
            }

            throw new Exception("Nothing found");
        }

        private Position? NextFreeCellInDirection(CharMap map, Position pos, Direction direction)
        {
            Position nextPos = pos + direction;
            while (map[nextPos] == 'O')
            {
                nextPos += direction;
            }

            if (map[nextPos] == '.') return nextPos;
            return null;
        }
    }
}