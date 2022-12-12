// using System.Drawing;
//
// namespace Year2022
// {
//     public class Day12 : IDay
//     {
//         public Node[][] Map = null!;
//
//         public Object Sol1(String input)
//         {
//             String[] lines = input.Split('\n');
//             Map = new Node[lines.Length][];
//             const String heightLookup = "abcdefghijklmnopqrstuvwxyz";
//
//             Point startPoint = default;
//
//             //Parse input into Map
//             for (Int32 i = 0; i < lines.Length; i++)
//             {
//                 String line = lines[i].Trim();
//                 Node[] nodeLine = new Node[lines[0].Length];
//                 for (Int32 j = 0; j < line.Length; j++)
//                 {
//                     Char c = line[j];
//                     Int32 height;
//                     Boolean isStart = false;
//                     Boolean isEnd = false;
//                     switch (c)
//                     {
//                         case 'S':
//                             height = 0;
//                             isStart = true;
//                             startPoint = new Point(j, i);
//                             break;
//                         case 'E':
//                             height = 25;
//                             isEnd = true;
//                             break;
//                         default:
//                             height = heightLookup.IndexOf(c);
//                             break;
//                     }
//
//                     nodeLine[j] = new Node(height, isStart, isEnd);
//                 }
//
//                 // String str = "";
//                 // foreach (Node node in nodeLine)
//                 // {
//                 //     str += heightLookup[node.Height] + " ";
//                 // }
//
//                 // Console.WriteLine(str);
//                 Map[i] = nodeLine;
//             }
//
//             // foreach (Node[] line in Map)
//             // {
//             // String lineStr = "";
//
//             // foreach (Node node in line)
//             // {
//             // lineStr += heightLookup[node.Height];
//             // lineStr += node.Start ? 'S' : node.End ? 'E' : ' ';
//             // }
//
//             // Console.WriteLine(lineStr);
//             // }
//
//             List<List<Point>> possibleRoutes = FindEnd(startPoint, new(), new());
//
//             // foreach (List<Point> route in possibleRoutes)
//             // {
//             //     Console.WriteLine(route.Count);
//             // }
//
//             List<Point> shortestRoute = possibleRoutes.MinBy(route => route.Count)!;
//
//             // Console.WriteLine("Shortest route:");
//             // for (Int32 i = 0; i < shortestRoute.Count; i++)
//             // {
//             //     Point point = shortestRoute[i];
//             //     Point previousPoint = i == 0 ? new Point(0, 0) : shortestRoute[i - 1];
//             //     Direction direction = Direction.GetDirection(point.X - previousPoint.X, point.Y - previousPoint.Y);
//             //     Console.WriteLine($"{point} ({direction.Text})");
//             // }
//
//             return shortestRoute.Count;
//         }
//
//         public List<List<Point>> FindEnd(Point startPoint, List<List<Point>> routes, List<Point> route)
//         {
//             Node startNode = Map[startPoint.Y][startPoint.X];
//             foreach (Direction direction in Direction.Directions)
//             {
//                 Point adjacentPoint = new(startPoint.X + direction.DeltaX, startPoint.Y + direction.DeltaY);
//                 if (adjacentPoint.X >= Map[0].Length || adjacentPoint.X < 0 ||
//                     adjacentPoint.Y >= Map.Length || adjacentPoint.Y < 0) continue;
//                 if (route.Contains(adjacentPoint)) continue;
//                 Node adjacentNode = Map[adjacentPoint.Y][adjacentPoint.X];
//
//                 if (adjacentNode.Height > startNode.Height + 1) continue;
//
//                 if (adjacentNode.End)
//                 {
//                     List<Point> newRoute2 = new();
//                     newRoute2.AddRange(route);
//                     newRoute2.Add(adjacentPoint);
//                     routes.Add(newRoute2);
//                     return routes;
//                 }
//
//                 List<Point> newRoute = new();
//                 newRoute.AddRange(route);
//                 newRoute.Add(adjacentPoint);
//                 routes = FindEnd(adjacentPoint, routes, newRoute);
//             }
//
//             return routes;
//         }
//
//         public Object Sol2(String input)
//         {
//             return "";
//         }
//     }
// }

