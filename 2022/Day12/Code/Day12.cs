using System.Text;
using System.Drawing;

namespace Year2022
{
    public class Day12 : IDay
    {
        public static List<string> Map = null!;
        public static Point End;

        public object Sol1(string input)
        {
            List<string> lines = input.Split('\n').ToList();
            Map = lines;

            int startY = Map.FindIndex(x => x.Contains("S"));
            int startX = Map[startY].IndexOf("S", StringComparison.Ordinal);
            Node start = new(startX, startY);

            StringBuilder sb = new(Map[startY]);
            sb[startX] = 'a';
            Map[startY] = sb.ToString().Trim();

            int endY = Map.FindIndex(x => x.Contains("E"));
            int endX = Map[endY].IndexOf("E", StringComparison.Ordinal);

            sb = new(Map[endY]);
            sb[endX] = 'z';
            Map[endY] = sb.ToString().Trim();

            List<Node> activeNodes = new();
            List<Node> visitedNodes = new();

            activeNodes.Add(start);

            // List<Node>? bestRoute = null;
            int bestRouteLength = int.MaxValue;

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

                    return route.Count;

                    // if (route.Count < bestRouteLength)
                    // {
                    //     // bestRoute = route;
                    //     bestRouteLength = route.Count;
                    // }
                }

                visitedNodes.Add(checkNode);
                activeNodes.Remove(checkNode);

                List<Node> walkableNodes = GetWalkableNodes(checkNode);

                foreach (Node walkableNode in walkableNodes)
                {
                    if (visitedNodes.Any(node => node.X == walkableNode.X && node.Y == walkableNode.Y)) continue;

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

            const string heightLookup = "abcdefghijklmnopqrstuvwxyz";
            char c = Map[currentNode.Y][currentNode.X];
            int currentNodeHeight = heightLookup.IndexOf(c);

            foreach (Direction direction in Direction.Directions)
            {
                Point adjacentPoint = new(currentNode.X + direction.DeltaX, currentNode.Y + direction.DeltaY);
                if (adjacentPoint.X >= Map[0].Length || adjacentPoint.X < 0 ||
                    adjacentPoint.Y >= Map.Count || adjacentPoint.Y < 0) continue;

                c = Map[adjacentPoint.Y][adjacentPoint.X];

                int adjacentNodeHeight = heightLookup.IndexOf(c);

                if (adjacentNodeHeight > currentNodeHeight + 1) continue;

                walkableNodes.Add(new Node(adjacentPoint.X, adjacentPoint.Y,
                    currentNode.Cost + 1, currentNode));
            }

            return walkableNodes;
        }

        public object Sol2(string input)
        {
            List<string> lines = input.Split('\n').ToList();
            Map = lines;

            int startY = Map.FindIndex(x => x.Contains("E"));
            int startX = Map[startY].IndexOf("E", StringComparison.Ordinal);
            Node2 start = new(startX, startY);

            StringBuilder sb = new(Map[startY]);
            sb[startX] = 'z';
            Map[startY] = sb.ToString().Trim();

            List<Node2> activeNodes = new();
            List<Node2> visitedNodes = new();

            activeNodes.Add(start);

            // List<Node>? bestRoute = null;
            int bestRouteLength = int.MaxValue;

            while (activeNodes.Any())
            {
                Node2 checkNode = activeNodes.MinBy(x => x.CostDistance)!;

                if (Map[checkNode.Y][checkNode.X] == 'a')
                {
                    Node2 currentNode = checkNode;
                    List<Node2> route = new();
                    route.Add(currentNode);
                    while (currentNode.Parent != null)
                    {
                        route.Add(currentNode.Parent);
                        currentNode = currentNode.Parent;
                    }

                    route.Reverse();
                    route.RemoveAt(0);

                    return route.Count;

                    // if (route.Count < bestRouteLength)
                    // {
                    //     // bestRoute = route;
                    //     bestRouteLength = route.Count;
                    // }
                }

                visitedNodes.Add(checkNode);
                activeNodes.Remove(checkNode);

                List<Node2> walkableNodes = GetWalkableNodes2(checkNode);

                foreach (Node2 walkableNode in walkableNodes)
                {
                    if (visitedNodes.Any(node => node.X == walkableNode.X && node.Y == walkableNode.Y)) continue;

                    if (activeNodes.Any(x => x.X == walkableNode.X && x.Y == walkableNode.Y))
                    {
                        Node2 existingNode = activeNodes.First(x => x.X == walkableNode.X && x.Y == walkableNode.Y);
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

        private static List<Node2> GetWalkableNodes2(Node2 currentNode)
        {
            List<Node2> walkableNodes = new();

            const string heightLookup = "abcdefghijklmnopqrstuvwxyz";
            char c = Map[currentNode.Y][currentNode.X];
            int currentNodeHeight = heightLookup.IndexOf(c);

            foreach (Direction direction in Direction.Directions)
            {
                Point adjacentPoint = new(currentNode.X + direction.DeltaX, currentNode.Y + direction.DeltaY);
                if (adjacentPoint.X >= Map[0].Length || adjacentPoint.X < 0 ||
                    adjacentPoint.Y >= Map.Count || adjacentPoint.Y < 0) continue;

                c = Map[adjacentPoint.Y][adjacentPoint.X];

                int adjacentNodeHeight = heightLookup.IndexOf(c);

                if (adjacentNodeHeight + 1 < currentNodeHeight) continue;

                walkableNodes.Add(new Node2(adjacentPoint.X, adjacentPoint.Y,
                    currentNode.Cost + 1, currentNode));
            }

            return walkableNodes;
        }
    }
}
