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

            Int32 startY = Map.FindIndex(x => x.Contains("S"));
            Int32 startX = Map[startY].IndexOf("S", StringComparison.Ordinal);
            Node start = new(startX, startY);

            StringBuilder sb = new(Map[startY]);
            sb[startX] = 'a';
            Map[startY] = sb.ToString();

            Int32 endY = Map.FindIndex(x => x.Contains("E"));
            Int32 endX = Map[startY].IndexOf("E", StringComparison.Ordinal);
            Node end = new(endX, endY);

            sb = new(Map[endY]);
            sb[endX] = 'z';
            Map[endY] = sb.ToString();

            List<Node> activeNodes = new();
            List<Node> visitedNodes = new();

            activeNodes.Add(start);
            activeNodes.Add(end);

            while (activeNodes.Any()){
                Node checkNode = activeNodes.MaxBy(x=>x.CostDistance)!;

                if (checkNode.X == endX && checkNode.Y == endY){
                    
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

            return "";
        }

        private static List<Node> GetWalkableNodes(Node currentNode, Node endNode)
        {
            List<Node> walkableNodes = new();

            foreach (Direction direction in Direction.Directions)
            {
                Point adjacentPoint = new(currentNode.X + direction.DeltaX, currentNode.Y + direction.DeltaY);
                if (adjacentPoint.X >= Map[0].Length || adjacentPoint.X < 0 ||
                    adjacentPoint.Y >= Map.Count || adjacentPoint.Y < 0) continue;

                const String heightLookup = "abcdefghijklmnopqrstuvwxyz";
                Char c = Map[currentNode.Y][currentNode.X];
                Int32 currentNodeHeight = heightLookup.IndexOf(c);

                c = Map[adjacentPoint.Y][adjacentPoint.X];
                Int32 adjacentNodeHeight = heightLookup.IndexOf(c);

                if (adjacentNodeHeight > currentNodeHeight + 1) continue;

                walkableNodes.Add(new Node(adjacentPoint.X + direction.DeltaX,adjacentPoint.Y + direction.DeltaY,currentNode.Cost + 1,currentNode));
            }


        }

        public Object Sol2(String input)
        {
            return "";
        }
    }
}