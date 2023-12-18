using HelperClasses;

namespace Year2023
{
    public class Day16 : IDay
    {
        internal enum TileType
        {
            Empty,
            SlashMirror,
            BackslashMirror,
            VerticalSplitter,
            HorizontalSplitter,
        }

        internal class Tile
        {
            public TileType TileType;
            public bool Energized;

            public Tile(TileType tileType)
            {
                TileType = tileType;
                Energized = false;
            }

            // public object Clone()
            // {
            //     Tile newTile = new Tile(TileType)
            //     {
            //         Energized = Energized
            //     };
            //     return newTile;
            // }
        }

        internal class Beam
        {
            public int X;
            public int Y;
            public Direction CurrentDirection;

            public Beam(int x = 0, int y = 0, Direction? startDirection = null)
            {
                X = x;
                Y = y;
                CurrentDirection = startDirection ?? Direction.Right;
            }
        }

        internal class Turn
        {
            public int X;
            public int Y;
            public Direction Direction;

            public Turn(int x, int y, Direction direction)
            {
                X = x;
                Y = y;
                Direction = direction;
            }
        }

        internal int GetEnergizedAmount(Map<Tile> map, Beam beam)
        {
            List<Beam> beams = new() { beam };
            List<Turn> turns = new() { new Turn(beam.X, beam.Y, beam.CurrentDirection) };

            while (beams.Any())
            {
                Beam currentBeam = beams[0];

                map[currentBeam.X, currentBeam.Y].Energized = true;

                switch (map[currentBeam.X, currentBeam.Y].TileType)
                {
                    case TileType.Empty:
                        break;
                    case TileType.SlashMirror:
                        currentBeam.CurrentDirection = currentBeam.CurrentDirection.DirectionE switch
                        {
                            Direction.DirectionEnum.Up => Direction.Right,
                            Direction.DirectionEnum.Down => Direction.Left,
                            Direction.DirectionEnum.Right => Direction.Up,
                            Direction.DirectionEnum.Left => Direction.Down,
                            _ => throw new Exception("I'm a teapot"),
                        };
                        break;
                    case TileType.BackslashMirror:
                        currentBeam.CurrentDirection = currentBeam.CurrentDirection.DirectionE switch
                        {
                            Direction.DirectionEnum.Up => Direction.Left,
                            Direction.DirectionEnum.Down => Direction.Right,
                            Direction.DirectionEnum.Right => Direction.Down,
                            Direction.DirectionEnum.Left => Direction.Up,
                            _ => throw new Exception("I'm a teapot"),
                        };
                        break;
                    case TileType.HorizontalSplitter:
                        if (currentBeam.CurrentDirection.DirectionE == Direction.DirectionEnum.Up
                        || currentBeam.CurrentDirection.DirectionE == Direction.DirectionEnum.Down)
                        {
                            if (turns.Any(Turn => Turn.X == currentBeam.X && Turn.Y == currentBeam.Y && Turn.Direction.DirectionE == Direction.DirectionEnum.Left))
                            {
                                beams.Remove(currentBeam);
                            }
                            else
                            {
                                turns.Add(new Turn(currentBeam.X, currentBeam.Y, Direction.Left));
                                currentBeam.CurrentDirection = Direction.Left;
                            }

                            if (!turns.Any(Turn => Turn.X == currentBeam.X && Turn.Y == currentBeam.Y && Turn.Direction.DirectionE == Direction.DirectionEnum.Right))
                            {
                                beams.Add(new Beam(currentBeam.X, currentBeam.Y, Direction.Right));
                                turns.Add(new Turn(currentBeam.X, currentBeam.Y, Direction.Right));
                            }
                        }
                        break;
                    case TileType.VerticalSplitter:
                        if (currentBeam.CurrentDirection.DirectionE == Direction.DirectionEnum.Right
                        || currentBeam.CurrentDirection.DirectionE == Direction.DirectionEnum.Left)
                        {
                            if (turns.Any(Turn => Turn.X == currentBeam.X && Turn.Y == currentBeam.Y && Turn.Direction.DirectionE == Direction.DirectionEnum.Up))
                            {
                                beams.Remove(currentBeam);
                            }
                            else
                            {
                                turns.Add(new Turn(currentBeam.X, currentBeam.Y, Direction.Up));
                                currentBeam.CurrentDirection = Direction.Up;
                            }

                            if (!turns.Any(Turn => Turn.X == currentBeam.X && Turn.Y == currentBeam.Y && Turn.Direction.DirectionE == Direction.DirectionEnum.Down))
                            {
                                beams.Add(new Beam(currentBeam.X, currentBeam.Y, Direction.Down));
                                turns.Add(new Turn(currentBeam.X, currentBeam.Y, Direction.Down));
                            }
                        }
                        break;
                }

                currentBeam.X += currentBeam.CurrentDirection.DeltaX;
                currentBeam.Y += currentBeam.CurrentDirection.DeltaY;

                if (currentBeam.X < 0 || currentBeam.X >= map.Width
                || currentBeam.Y < 0 || currentBeam.Y >= map.Height)
                {
                    beams.Remove(currentBeam);
                }
            }

            int sum = 0;

            for (int y = 0; y < map.Height; y++)
            {
                // StringBuilder line = new();
                for (int x = 0; x < map.Width; x++)
                {
                    if (map[x, y].Energized)
                    {
                        sum++;
                        //     line.Append('#');
                    }
                    // else
                    // {
                    //     line.Append('.');
                    // }
                }
                // Console.WriteLine(line);
            }

            return sum;
        }

        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');

            Map<Tile> map = new(lines[0].Length, lines.Length);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    TileType tileType = lines[y][x] switch
                    {
                        '.' => TileType.Empty,
                        '/' => TileType.SlashMirror,
                        '\\' => TileType.BackslashMirror,
                        '|' => TileType.VerticalSplitter,
                        '-' => TileType.HorizontalSplitter,
                        _ => throw new Exception("Invalid input"),
                    };
                    map[x, y] = new Tile(tileType);
                }
            }

            return GetEnergizedAmount(map, new Beam());
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');

            Map<Tile> GenerateMap()
            {
                Map<Tile> map = new(lines[0].Length, lines.Length);

                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        TileType tileType = lines[y][x] switch
                        {
                            '.' => TileType.Empty,
                            '/' => TileType.SlashMirror,
                            '\\' => TileType.BackslashMirror,
                            '|' => TileType.VerticalSplitter,
                            '-' => TileType.HorizontalSplitter,
                            _ => throw new Exception("Invalid input"),
                        };
                        map[x, y] = new Tile(tileType);
                    }
                }
                return map;
            }

            Map<Tile> map = GenerateMap();

            int highestEnergizedAmount = 0;

            for (int y = 0; y < map.Height; y++)
            {
                map = GenerateMap();
                int energizedAmount = GetEnergizedAmount(map, new Beam(0, y, Direction.Right));
                if (energizedAmount > highestEnergizedAmount) highestEnergizedAmount = energizedAmount;
                // Console.WriteLine(energizedAmount);
                map = GenerateMap();
                energizedAmount = GetEnergizedAmount(map, new Beam(map.Width - 1, y, Direction.Left));
                if (energizedAmount > highestEnergizedAmount) highestEnergizedAmount = energizedAmount;
                // Console.WriteLine(energizedAmount);
            }

            for (int x = 0; x < map.Height; x++)
            {
                map = GenerateMap();
                int energizedAmount = GetEnergizedAmount(map, new Beam(x, 0, Direction.Down));
                if (energizedAmount > highestEnergizedAmount) highestEnergizedAmount = energizedAmount;
                // Console.WriteLine(energizedAmount);
                map = GenerateMap();
                energizedAmount = GetEnergizedAmount(map, new Beam(x, map.Height - 1, Direction.Up));
                if (energizedAmount > highestEnergizedAmount) highestEnergizedAmount = energizedAmount;
                // Console.WriteLine(energizedAmount);
            }

            return highestEnergizedAmount;
        }
    }
}
