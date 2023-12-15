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

            public int DeltaX { get; }
            public int DeltaY { get; }
            public string Text { get; }

            private Direction(int deltaX, int deltaY, string text)
            {
                DeltaX = deltaX;
                DeltaY = deltaY;
                Text = text;
            }

            public override bool Equals(object? other) =>
               other != null && GetType() == other.GetType() && Equals((Direction)other);

            public bool Equals(Direction other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;

            public override int GetHashCode()
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

        public object Sol1(string input)
        {
            string[] lines = input.Split('\n');
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

        class Node2
        {
            public int X;
            public int Y;
            public List<Direction> ConnectionDirections = new();
            public int? Distance;

            public Node2(int x, int y, List<Direction> connectionDirections)
            {
                X = x;
                Y = y;
                ConnectionDirections = connectionDirections;
            }
        }

        public object Sol2(string input)
        {
            string[] lines = input.Split('\n');
            int gridHeight = lines.Length;
            int gridWidth = lines[0].Length;
            Node2[,] nodes = new Node2[gridHeight, gridWidth];

            int expandedGridHeight = gridHeight * 2;
            int expandedGridWidth = gridWidth * 2;
            string[] expandedGrid = new string[expandedGridHeight];

            Node2? startNode = null;

            //Set directions each node has a connection to
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

                    Node2 newNode = new Node2(x, y, connectionDirections);
                    nodes[y, x] = newNode;
                    if (lines[y][x] == 'S') startNode = newNode;
                }
            }

            if (startNode == null) throw new Exception("No startNode was found");

            startNode.Distance = 0;
            Queue<Node2> todoNodes = new Queue<Node2>(new[] { startNode });
            List<Node2> foundNodes = new() { startNode };

            //Find all nodes that are in the main loop
            while (todoNodes.Count > 0)
            {
                Node2 currentNode = todoNodes.Dequeue();

                foreach (Direction direction in Direction.Directions)
                {
                    if (currentNode.ConnectionDirections.Contains(direction) && currentNode.Y + direction.DeltaY >= 0 && currentNode.Y + direction.DeltaY < gridHeight && currentNode.X + direction.DeltaX >= 0 && currentNode.X + direction.DeltaX < gridWidth)
                    {
                        Node2 connectedNode = nodes[currentNode.Y + direction.DeltaY, currentNode.X + direction.DeltaX];
                        if (connectedNode.Distance is null && connectedNode.ConnectionDirections.Contains(-direction))
                        {
                            connectedNode.Distance = currentNode.Distance + 1;
                            foundNodes.Add(connectedNode);
                            todoNodes.Enqueue(connectedNode);
                        }
                    }
                }
            }

            //Replace all cells with 2x2 cells so floodfill will reach all desired spots
            for (int y = 0; y < lines.Length; y++)
            {
                StringBuilder newLine1 = new();
                StringBuilder newLine2 = new();

                for (int x = 0; x < lines[0].Length; x++)
                {
                    IEnumerable<Node2> matchingNodes = foundNodes.Where(node => node.X == x && node.Y == y);
                    // Node2 matchingNode = matchingNodes.First();
                    if (matchingNodes.Count() == 0)
                    {
                        newLine1.Append("..");
                        newLine2.Append("..");
                    }
                    else
                    {
                        switch (lines[y][x])
                        {
                            case '|':
                                newLine1.Append(",|");
                                newLine2.Append(",|");
                                break;
                            case '-':
                                newLine1.Append("--");
                                newLine2.Append(",,");
                                break;
                            case 'L':
                                newLine1.Append(",L");
                                newLine2.Append(",,");
                                break;
                            case 'J':
                                newLine1.Append("J,");
                                newLine2.Append(",,");
                                break;
                            case '7':
                                newLine1.Append(",,");
                                newLine2.Append("7,");
                                break;
                            case 'F':
                                newLine1.Append(",,");
                                newLine2.Append(",F");
                                break;
                            case 'S':
                                newLine1.Append("SS");
                                newLine2.Append("SS");
                                break;
                        }
                    }
                }

                expandedGrid[y * 2] = newLine1.ToString();
                expandedGrid[y * 2 + 1] = newLine2.ToString();
            }

            // foreach (String line in expandedGrid)
            // {
            //     Console.WriteLine(line);
            // }

            Queue<Point> todoPoints = new();
            List<Point> foundPoints = new();

            List<Point> foundDots = new();
            int outsideCount = 0;

            //Run a floodfill to count all cells on the outside
            for (int x = 0; x < expandedGridWidth; x++)
            {
                Point newPoint = new Point(x, 0);
                if (expandedGrid[newPoint.Y][newPoint.X] == '.' || expandedGrid[newPoint.Y][newPoint.X] == ',')
                {
                    todoPoints.Enqueue(newPoint);
                    foundPoints.Add(newPoint);

                    if (expandedGrid[newPoint.Y][newPoint.X] == '.')
                    {
                        foundDots.Add(newPoint);
                        outsideCount++;
                    }
                }

                Point newPoint2 = new Point(x, expandedGridHeight - 1);
                if (expandedGrid[newPoint2.Y][newPoint2.X] == '.' || expandedGrid[newPoint2.Y][newPoint2.X] == ',')
                {
                    todoPoints.Enqueue(newPoint2);
                    foundPoints.Add(newPoint2);

                    if (expandedGrid[newPoint.Y][newPoint.X] == '.')
                    {
                        foundDots.Add(newPoint);
                        outsideCount++;
                    }
                }
            }

            for (int y = 1; y < expandedGridHeight - 1; y++)
            {
                Point newPoint = new Point(0, y);

                if (expandedGrid[newPoint.Y][newPoint.X] == '.' || expandedGrid[newPoint.Y][newPoint.X] == ',')
                {
                    todoPoints.Enqueue(newPoint);
                    foundPoints.Add(newPoint);

                    if (expandedGrid[newPoint.Y][newPoint.X] == '.')
                    {
                        foundDots.Add(newPoint);
                        outsideCount++;
                    }
                }

                Point newPoint2 = new Point(expandedGridWidth - 1, y);
                if (expandedGrid[newPoint2.Y][newPoint2.X] == '.' || expandedGrid[newPoint2.Y][newPoint2.X] == ',')
                {
                    todoPoints.Enqueue(newPoint2);
                    foundPoints.Add(newPoint2);

                    if (expandedGrid[newPoint.Y][newPoint.X] == '.')
                    {
                        foundDots.Add(newPoint);
                        outsideCount++;
                    }
                }
            }

            //Find all nodes that are in the main loop
            while (todoPoints.Count > 0)
            {
                Point currentPoint = todoPoints.Dequeue();

                foreach (Direction direction in Direction.Directions)
                {
                    Point newPoint = new Point(currentPoint.X + direction.DeltaX, currentPoint.Y + direction.DeltaY);
                    if (!foundPoints.Contains(newPoint)
                    && currentPoint.Y + direction.DeltaY >= 0
                    && currentPoint.Y + direction.DeltaY < expandedGridHeight
                    && currentPoint.X + direction.DeltaX >= 0
                    && currentPoint.X + direction.DeltaX < expandedGridWidth
                    && (expandedGrid[newPoint.Y][newPoint.X] == '.' || expandedGrid[newPoint.Y][newPoint.X] == ','))
                    {
                        // Console.WriteLine("X: " + newPoint.X + " Y: " + newPoint.Y);
                        foundPoints.Add(newPoint);
                        todoPoints.Enqueue(newPoint);
                        if (expandedGrid[newPoint.Y][newPoint.X] == '.')
                        {
                            outsideCount++;
                            foundDots.Add(new Point(newPoint.X, newPoint.Y));
                        }
                    }
                }
            }

            // for (int y = 0; y < expandedGridHeight; y++)
            // {
            //     StringBuilder line = new();
            //     for (int x = 0; x < expandedGridWidth; x++)
            //     {
            //         if (foundDots.Contains(new Point(x, y)))
            //         {
            //             line.Append('O');
            //         }
            //         else
            //         {
            //             line.Append(expandedGrid[y][x]);
            //         }
            //     }
            //     Console.WriteLine(line);
            // }

            //Count amount of total empty cells
            int totalEmptyCells = 0;
            foreach (string line in expandedGrid)
            {
                totalEmptyCells += line.Count(x => x == '.');
            }

            return (totalEmptyCells - outsideCount) / 4;
        }
    }
}
