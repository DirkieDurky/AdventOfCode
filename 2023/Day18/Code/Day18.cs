using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Text;
using HelperClasses;

namespace Year2023
{
    public class Day18 : IDay
    {
        internal class Tile
        {
            public bool Filled;
            public string? HexColor;

            public Tile(bool filled = false, string? hexColor = null)
            {
                Filled = filled;
                HexColor = hexColor;
            }
        }

        internal class Instruction
        {
            public Direction Direction;
            public int Amount;
            public string HexColor;

            public Instruction(Direction direction, int amount, string hexColor)
            {
                Direction = direction;
                Amount = amount;
                HexColor = hexColor;
            }
        }
        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');
            Instruction[] instructions = new Instruction[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] split = line.Split();

                Direction direction = split[0] switch
                {
                    "U" => Direction.Up,
                    "D" => Direction.Down,
                    "L" => Direction.Left,
                    "R" => Direction.Right,
                    _ => throw new Exception("I'm a teapot"),
                };

                instructions[i] = new Instruction(direction, int.Parse(split[1]), split[2].Replace("(", "").Replace(")", ""));
            }

            int minX = int.MaxValue;
            int maxX = 0;
            int minY = int.MaxValue;
            int maxY = 0;

            int testX = 0;
            int testY = 0;
            foreach (Instruction instruction in instructions)
            {
                testX += instruction.Direction.DeltaX * instruction.Amount;
                testY += instruction.Direction.DeltaY * instruction.Amount;

                if (testX < minX) minX = testX;
                if (testX > maxX) maxX = testX;
                if (testY < minY) minY = testY;
                if (testY > maxY) maxY = testY;
            }

            Map<Tile> map = new Map<Tile>(Math.Abs(minX) + maxX + 1, Math.Abs(minY) + maxY + 1);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    map[x, y] = new Tile();
                }
            }

            int rightTurnCount = 0;
            int leftTurnCount = 0;
            Direction.DirectionEnum? lastDirection = Direction.DirectionEnum.Up;

            //Start at the center of the map
            int currentX = Math.Abs(minX);
            int currentY = Math.Abs(minY);

            map[currentX, currentY].Filled = true;
            foreach (Instruction instruction in instructions)
            {
                if (lastDirection != null)
                {
                    if (instruction.Direction.DirectionE > lastDirection || (instruction.Direction.DirectionE == Direction.DirectionEnum.Up && lastDirection == Direction.DirectionEnum.Left)) rightTurnCount++;
                    if (instruction.Direction.DirectionE < lastDirection || (instruction.Direction.DirectionE == Direction.DirectionEnum.Left && lastDirection == Direction.DirectionEnum.Up)) leftTurnCount++;
                }
                lastDirection = instruction.Direction.DirectionE;

                for (int amountLeft = instruction.Amount; amountLeft > 0; amountLeft--)
                {
                    currentX += instruction.Direction.DeltaX;
                    currentY += instruction.Direction.DeltaY;
                    map[currentX, currentY].Filled = true;
                    map[currentX, currentY].HexColor = instruction.HexColor;
                }
            }

            //Calculate whether the hole was dug counterclockwise or clockwise
            int turnDifference = rightTurnCount - leftTurnCount;
            if (turnDifference != 3 && turnDifference != -3) throw new Exception("Im confused");
            bool drawnClockwise = turnDifference == 3;

            // Console.WriteLine("The hole was dug " + (!drawnClockwise ? "counter" : "" + "clockwise"));

            //Now that we know whether we dug counterclockwise or clockwise we can find a cell that is inside the hole to start a floodfill to dig the insides
            //To find a point in the center of the hole, start at the center
            Point startPoint = new Point((map.Width - 1) / 2, (map.Height - 1) / 2);
            //Move one into the direction of the first instruction
            startPoint.X += instructions[0].Direction.DeltaX;
            startPoint.Y += instructions[0].Direction.DeltaY;
            //Depending on whether we are going counterclockwise or clockwise, go inside the hole
            Direction directionToInsideHole = Direction.FindDirectionByDirectionEnum((Direction.DirectionEnum)(instructions[0].Direction.DirectionE! + (drawnClockwise ? 1 : -1)));
            startPoint.X += directionToInsideHole.DeltaX;
            startPoint.Y += directionToInsideHole.DeltaY;

            Queue<Point> todoPoints = new(new[] { startPoint });
            HashSet<Point> donePoints = new();

            while (todoPoints.Any())
            {
                Point currentPoint = todoPoints.Dequeue();
                map[currentPoint.X, currentPoint.Y].Filled = true;

                foreach (Direction direction in Direction.Directions)
                {
                    Point testPoint = new Point(currentPoint.X + direction.DeltaX, currentPoint.Y + direction.DeltaY);

                    if (!donePoints.Contains(testPoint) && !map[testPoint.X, testPoint.Y].Filled)
                    {
                        todoPoints.Enqueue(testPoint);
                        donePoints.Add(testPoint);
                    }
                }
            }

            int sum = 0;

            for (int y = 0; y < map.Height; y++)
            {
                // StringBuilder line = new();
                for (int x = 0; x < map.Width; x++)
                {
                    // line.Append(map[x, y].Filled ? '#' : '.');
                    if (map[x, y].Filled) sum++;
                }
                // Console.WriteLine(line);
            }
            // Console.WriteLine();

            return sum;
        }

        public object Sol2(string input)
        {


            return 0;
        }
    }
}
