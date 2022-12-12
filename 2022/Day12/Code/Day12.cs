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
            const String heightLookup = "abcdefghijklmnopqrstuvwxyz";

            Int32 startY = Map.FindIndex(x => x.Contains("S"));
            Int32 startX = Map[startY].IndexOf("S", StringComparison.Ordinal);
            Node start = new(startX, startY);

            Int32 endY = Map.FindIndex(x => x.Contains("E"));
            Int32 endX = Map[startY].IndexOf("E", StringComparison.Ordinal);
            Node end = new(endX, endY);

            List<Node> activeNodes = new();
            List<Node> visitedNodes = new();


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
                Int32 height;
            }
        }

        public Object Sol2(String input)
        {
            return "";
        }
    }
}