using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Year2022;

namespace Year2023
{
    public class Day10 : IDay
    {
        public class Direction
        {
            public static Direction None => new Direction(0, 0, "NONE");

            public static Direction Up => new Direction(0, -1, "UP");
            public static Direction Down => new Direction(0, 1, "DOWN");
            public static Direction Left => new Direction(-1, 0, "LEFT");
            public static Direction Right => new Direction(1, 0, "RIGHT");

            public static List<Direction> Directions = new List<Direction> { Up, Down, Left, Right };

            public Int32 DeltaX { get; }
            public Int32 DeltaY { get; }
            public String Text { get; }

            private Direction(Int32 deltaX, Int32 deltaY, String text)
            {
                DeltaX = deltaX;
                DeltaY = deltaY;
                Text = text;
            }

            public override Boolean Equals(Object? other) =>
               other != null && GetType() == other.GetType() && Equals((Direction)other);

            public Boolean Equals(Direction other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;

            public override Int32 GetHashCode()
            {
                return DeltaX * 2 + DeltaY;
            }

            public static Direction operator -(Direction direction)
            {
                return new Direction(-direction.DeltaX, -direction.DeltaY, direction.Text switch
                {
                    "UP" => "DOWN",
                    "DOWN" => "UP",
                    "LEFT" => "RIGHT",
                    "RIGHT" => "LEFT",
                    _ => direction.Text,
                });
            }
        }

        class Node
        {
            public int X;
            public int Y;
            public List<Direction> ConnectionDirections = new();
            public int? Distance;

            public Node(int x, int y, List<Direction> connectionDirections)
            {
                X = x;
                Y = y;
                ConnectionDirections = connectionDirections;
            }
        }

        public Object Sol1(String input)
        {
            String[] lines = input.Split('\n');
            int gridHeight = lines.Length;
            int gridWidth = lines[0].Length;
            Node[,] nodes = new Node[gridHeight, gridWidth];

            Node? startNode = null;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    List<Direction> connectionDirections = new();
                    switch (lines[y][x])
                    {
                        case '.':
                            break;
                        case 'F':
                            connectionDirections.Add(Direction.Right);
                            connectionDirections.Add(Direction.Down);
                            break;
                        case '7':
                            connectionDirections.Add(Direction.Left);
                            connectionDirections.Add(Direction.Down);
                            break;
                        case 'J':
                            connectionDirections.Add(Direction.Up);
                            connectionDirections.Add(Direction.Left);
                            break;
                        case 'L':
                            connectionDirections.Add(Direction.Up);
                            connectionDirections.Add(Direction.Right);
                            break;
                        case '|':
                            connectionDirections.Add(Direction.Up);
                            connectionDirections.Add(Direction.Down);
                            break;
                        case '-':
                            connectionDirections.Add(Direction.Left);
                            connectionDirections.Add(Direction.Right);
                            break;
                        case 'S':
                            connectionDirections.Add(Direction.Up);
                            connectionDirections.Add(Direction.Right);
                            connectionDirections.Add(Direction.Down);
                            connectionDirections.Add(Direction.Left);
                            break;
                    }

                    Node newNode = new Node(x, y, connectionDirections);
                    nodes[y, x] = newNode;
                    if (lines[y][x] == 'S') startNode = newNode;
                }
            }

            if (startNode == null) throw new Exception("No startNode was found");

            startNode.Distance = 0;
            Queue<Node> todoNodes = new Queue<Node>(new[] { startNode });

            while (todoNodes.Count > 0)
            {
                Node currentNode = todoNodes.Dequeue();

                foreach (Direction direction in Direction.Directions)
                {
                    if (currentNode.ConnectionDirections.Contains(direction) && currentNode.Y + direction.DeltaY >= 0 && currentNode.Y + direction.DeltaY < gridHeight && currentNode.X + direction.DeltaX >= 0 && currentNode.X + direction.DeltaX < gridWidth)
                    {
                        Node connectedNode = nodes[currentNode.Y + direction.DeltaY, currentNode.X + direction.DeltaX];
                        if (connectedNode.Distance is null && connectedNode.ConnectionDirections.Contains(-direction))
                        {
                            connectedNode.Distance = currentNode.Distance + 1;
                            todoNodes.Enqueue(connectedNode);
                        }
                    }
                }
            }

            // for (int y = 0; y < gridHeight; y++)
            // {
            //     StringBuilder line = new StringBuilder();
            //     for (int x = 0; x < gridWidth; x++)
            //     {
            //         if (nodes[y, x].Distance is null)
            //         {
            //             line.Append(lines[y][x]);
            //         }
            //         else
            //         {
            //             line.Append(nodes[y, x].Distance % 10);
            //         }
            //     }
            //     Console.WriteLine(line.ToString());
            // }

            int highestDistance = 0;

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (nodes[y, x].Distance == null) continue;
                    if (nodes[y, x].Distance > highestDistance) highestDistance = (int)nodes[y, x].Distance!;
                }
            }

            return highestDistance;
        }


        public Object Sol2(String input)
        {
            return "result";
        }
    }
}
