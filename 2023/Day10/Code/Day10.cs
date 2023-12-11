using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Year2023
{
    public class Day10 : IDay
    {
        class Node
        {
            public int X;
            public int Y;
            public bool UpConnection;
            public bool RightConnection;
            public bool DownConnection;
            public bool LeftConnection;
            public int? Distance;

            public Node(int x, int y, bool upConnection, bool rightConnection, bool downConnection, bool leftConnection)
            {
                X = x;
                Y = y;
                UpConnection = upConnection;
                RightConnection = rightConnection;
                DownConnection = downConnection;
                LeftConnection = leftConnection;
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
                    bool upConnection = false;
                    bool rightConnection = false;
                    bool downConnection = false;
                    bool leftConnection = false;

                    switch (lines[y][x])
                    {
                        case '.':
                            break;
                        case 'F':
                            rightConnection = true;
                            downConnection = true;
                            break;
                        case '7':
                            leftConnection = true;
                            downConnection = true;
                            break;
                        case 'J':
                            upConnection = true;
                            leftConnection = true;
                            break;
                        case 'L':
                            upConnection = true;
                            rightConnection = true;
                            break;
                        case '|':
                            upConnection = true;
                            downConnection = true;
                            break;
                        case '-':
                            leftConnection = true;
                            rightConnection = true;
                            break;
                        case 'S':
                            upConnection = true;
                            rightConnection = true;
                            downConnection = true;
                            leftConnection = true;
                            break;
                    }

                    Node newNode = new Node(x, y, upConnection, rightConnection, downConnection, leftConnection);
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

                if (currentNode.UpConnection && currentNode.Y > 0)
                {
                    Node connectedNode = nodes[currentNode.Y - 1, currentNode.X];
                    if (connectedNode.Distance is null && connectedNode.DownConnection)
                    {
                        connectedNode.Distance = currentNode.Distance + 1;
                        todoNodes.Enqueue(connectedNode);
                    }
                }
                if (currentNode.RightConnection && currentNode.X < gridWidth)
                {
                    Node connectedNode = nodes[currentNode.Y, currentNode.X + 1];
                    if (connectedNode.Distance is null && connectedNode.LeftConnection)
                    {
                        connectedNode.Distance = currentNode.Distance + 1;
                        todoNodes.Enqueue(connectedNode);
                    }
                }
                if (currentNode.DownConnection && currentNode.Y < gridHeight)
                {
                    Node connectedNode = nodes[currentNode.Y + 1, currentNode.X];
                    if (connectedNode.Distance is null && connectedNode.UpConnection)
                    {
                        connectedNode.Distance = currentNode.Distance + 1;
                        todoNodes.Enqueue(connectedNode);
                    }
                }
                if (currentNode.LeftConnection && currentNode.X > 0)
                {
                    Node connectedNode = nodes[currentNode.Y, currentNode.X - 1];
                    if (connectedNode.Distance is null && connectedNode.RightConnection)
                    {
                        connectedNode.Distance = currentNode.Distance + 1;
                        todoNodes.Enqueue(connectedNode);
                    }
                }

                // foreach (Node node in todoNodes)
                // {
                //     Console.Write(lines[node.Y][node.X] + " ");
                // }
                // Console.WriteLine();
            }

            foreach (String line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();

            for (int y = 0; y < gridHeight; y++)
            {
                StringBuilder line = new StringBuilder();
                for (int x = 0; x < gridWidth; x++)
                {
                    if (nodes[y, x].Distance is null)
                    {
                        line.Append(lines[y][x]);
                    }
                    else
                    {
                        line.Append(nodes[y, x].Distance % 10);
                    }
                }
                Console.WriteLine(line.ToString());
            }

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
