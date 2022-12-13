using System.Text;
using System.Drawing;

namespace Year2022
{
    public class Day12 : IDay
    {
        public static List<String> Map = null!;
        public static Point End;

        public Object Sol1(String input)
        {
            List<String> lines = input.Split('\n').ToList();
            Map = lines;

            List<List<Node>> foundRoutes = new();

            Int32 startY = Map.FindIndex(x => x.Contains("S"));
            Int32 startX = Map[startY].IndexOf("S", StringComparison.Ordinal);
            Node start = new(startX, startY);

            StringBuilder sb = new(Map[startY]);
            sb[startX] = 'a';
            Map[startY] = sb.ToString().Trim();

            Int32 endY = Map.FindIndex(x => x.Contains("E"));
            Int32 endX = Map[endY].IndexOf("E", StringComparison.Ordinal);
            Node end = new(endX, endY);

            sb = new(Map[endY]);
            sb[endX] = 'z';
            Map[endY] = sb.ToString().Trim();

            List<Node> activeNodes = new();
            List<Node> visitedNodes = new();

            activeNodes.Add(start);

            List<Node>? bestRoute = null;
            Int32 bestRouteLength = Int32.MaxValue;

            while (activeNodes.Any())
            {
                Node checkNode = activeNodes.MinBy(x => x.CostDistance)!;

                if (checkNode.X == endX && checkNode.Y == endY)
                {
                    Node currentNode = checkNode;
                    List<Node> route = new();
                    route.Add(currentNode);
                    while (currentNode.Parent != null)
                    {
                        route.Add(currentNode.Parent);
                        currentNode = currentNode.Parent;
                    }

                    route.Reverse();
                    route.RemoveAt(0);

                    if (route.Count < bestRouteLength)
                    {
                        bestRoute = route;
                        bestRouteLength = route.Count;
                    }

                    visitedNodes.Add(checkNode);
                    activeNodes.Remove(checkNode);
                    continue;
                }

                visitedNodes.Add(checkNode);
                activeNodes.Remove(checkNode);

                List<Node> walkableNodes = GetWalkableNodes(checkNode);

                foreach (Node walkableNode in walkableNodes)
                {
                    Node currentNode = walkableNode;
                    List<Node> route = new();
                    while (currentNode.Parent != null)
                    {
                        route.Add(currentNode.Parent);
                        currentNode = currentNode.Parent;
                    }

                    route.Reverse();
                    if (route.Any(node => node.X == walkableNode.X && node.Y == walkableNode.Y)) continue;

                    if (activeNodes.Any(x => x.X == walkableNode.X && x.Y == walkableNode.Y))
                    {
                        Node existingNode = activeNodes.First(x => x.X == walkableNode.X && x.Y == walkableNode.Y);
                        if (existingNode.CostDistance > checkNode.CostDistance)
                        {
                            activeNodes.Remove(existingNode);
                            activeNodes.Add(walkableNode);
                        }
                    }
                    else
                    {
                        activeNodes.Add(walkableNode);
                    }
                }
            }

            // Console.WriteLine("Shortest route:");
            // for (Int32 i = 0; i < shortestRoute.Count; i++)
            // {
            //     Point point = shortestRoute[i];
            //     Point previousPoint = i == 0 ? new Point(0, 0) : shortestRoute[i - 1];
            //     Direction direction = Direction.GetDirection(point.X - previousPoint.X, point.Y - previousPoint.Y);
            //     Console.WriteLine($"{point} ({direction.Text})");
            // }

            // foreach (List<Node> route in foundRoutes)
            // {
            //     Console.WriteLine("Found route:");
            //     for (Int32 i = 0; i < route.Count; i++)
            //     {
            //         Point point = new(route[i].X, route[i].Y);
            //         Point previousPoint =
            //             i == 0 ? new Point(0, 0) : new(route[i - 1].X, route[i - 1].Y);
            //         Direction direction =
            //             Direction.GetDirection(point.X - previousPoint.X, point.Y - previousPoint.Y);
            //         Console.WriteLine($"{point} ({direction.Text})");
            //     }
            //
            //     Console.WriteLine();
            // }

            return bestRouteLength;
        }

        private static List<Node> GetWalkableNodes(Node currentNode)
        {
            List<Node> walkableNodes = new();

            const String heightLookup = "abcdefghijklmnopqrstuvwxyz";
            Char c = Map[currentNode.Y][currentNode.X];
            Int32 currentNodeHeight = heightLookup.IndexOf(c);

            foreach (Direction direction in Direction.Directions)
            {
                Point adjacentPoint = new(currentNode.X + direction.DeltaX, currentNode.Y + direction.DeltaY);
                if (adjacentPoint.X >= Map[0].Length || adjacentPoint.X < 0 ||
                    adjacentPoint.Y >= Map.Count || adjacentPoint.Y < 0) continue;

                c = Map[adjacentPoint.Y][adjacentPoint.X];

                Int32 adjacentNodeHeight = heightLookup.IndexOf(c);

                if (adjacentNodeHeight > currentNodeHeight + 1) continue;

                walkableNodes.Add(new Node(adjacentPoint.X, adjacentPoint.Y,
                    currentNode.Cost + 1, currentNode));
            }

            return walkableNodes;
        }

        public Object Sol2(String input)
        {
            return "";
        }
    }
}