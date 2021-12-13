
using System.Data.Common;

namespace Year2021
{
    public class Day12 : IDay
    {
        public Object Sol1(String input)
        {
            String[][] connections = input.Split("\n").Select(x => x.Split('-').ToArray()).ToArray();

            Dictionary<String, Int32> initialVisitedCount = new();
            foreach (String[] connection in connections)
            {
                foreach (String location in connection)
                {
                    //Console.WriteLine(location);
                    if (location == "start")
                    {
                        initialVisitedCount.TryAdd("start", 1);
                    }
                    else
                    {
                        initialVisitedCount.TryAdd(location, 0);
                    }
                }
            }

            List<(List<String>, Dictionary<String, Int32>)> paths = GetPaths(new List<String> { "start" }, initialVisitedCount);
            List<(List<String>, Dictionary<String, Int32>)> filtered = paths.Where(x => x.Item1.Last() == "end").ToList();

            foreach ((List<String>? path, Dictionary<String, Int32> visitedCount) in filtered)
            {
                String output = "";
                for (Int32 i = 0; i < path.Count; i++)
                {
                    output += path[i];
                    if (i != path.Count - 1) output += " -> ";
                }

                Console.WriteLine(output);
            }

            return filtered.Count;

            List<(List<String>, Dictionary<String, Int32>)> GetPaths(List<String> currentPath, Dictionary<String, Int32> visitedCount)
            {
                if (currentPath.Last() == "end") return new List<(List<String>, Dictionary<String, Int32>)> { (currentPath,visitedCount) };
                List<(List<String>, Dictionary<String, Int32>)> possiblePaths = new();
                foreach (String[] connection in connections)
                {
                    if (connection[0] == currentPath.Last() && (Char.IsUpper(connection[1][0]) || visitedCount[connection[1]] < 1))
                    {
                        List<String> tmpPath = new();
                        tmpPath.AddRange(currentPath);
                        tmpPath.Add(connection[1]);
                        Dictionary<String, Int32> tmpVisitedCount = new(visitedCount);
                        tmpVisitedCount[connection[1]]++;
                        possiblePaths.Add((tmpPath, tmpVisitedCount));
                    }
                    else if (connection[1] == currentPath.Last() && (Char.IsUpper(connection[0][0]) || visitedCount[connection[0]] < 1))
                    {
                        List<String> tmpPath = new();
                        tmpPath.AddRange(currentPath);
                        tmpPath.Add(connection[0]);
                        Dictionary<String, Int32> tmpVisitedCount = new(visitedCount);
                        tmpVisitedCount[connection[0]]++;
                        possiblePaths.Add((tmpPath, tmpVisitedCount));
                    }
                }

                List<(List<String>, Dictionary<String, Int32>)> outputPossiblePaths = new();
                //outputPossiblePaths.AddRange(possiblePaths);
                foreach ((List<String> path, Dictionary<String, Int32> visitedCount) item in possiblePaths)
                {
                    outputPossiblePaths.AddRange(GetPaths(item.path, item.visitedCount));
                }
                return outputPossiblePaths;
            }
        }

        public Object Sol2(String input)
        {
            String[][] connections = input.Split("\n").Select(x => x.Split('-').ToArray()).ToArray();

            Dictionary<String, Int32> initialVisitedCount = new();
            foreach (String[] connection in connections)
            {
                foreach (String location in connection)
                {
                    //Console.WriteLine(location);
                    if (location == "start")
                    {
                        initialVisitedCount.TryAdd("start", 1);
                    }
                    else
                    {
                        initialVisitedCount.TryAdd(location, 0);
                    }
                }
            }

            List<(List<String>, Dictionary<String, Int32>)> paths = GetPaths(new List<String> { "start" }, initialVisitedCount,null);
            List<(List<String>, Dictionary<String, Int32>)> filtered = paths.Where(x => x.Item1.Last() == "end").ToList();

            foreach ((List<String>? path, Dictionary<String, Int32> visitedCount) in filtered)
            {
                String output = "";
                for (Int32 i = 0; i < path.Count; i++)
                {
                    output += path[i];
                    if (i != path.Count - 1) output += " -> ";
                }

                Console.WriteLine(output);
            }

            return filtered.Count;
            

            List<(List<String>, Dictionary<String, Int32>)> GetPaths(List<String> currentPath, Dictionary<String, Int32> visitedCount, String? locationToVisitTwice)
            {
                //All the time when locationToVisitTwice == "d" currentPath.Last() is also "d"
                //WHYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY
                if (locationToVisitTwice == "d")
                {

                }
                if (currentPath.Last() == "end") return new List<(List<String>, Dictionary<String, Int32>)> {(currentPath, visitedCount)};
                List<(List<String>, Dictionary<String, Int32>, String?)> possiblePaths = new();
                foreach (String[] connection in connections)
                {
                    for (Int32 i = 0; i < 2; i++)
                    {
                        Int32 j = i == 0 ? 1 : 0;
                        if (connection[i] == currentPath.Last() &&
                            (Char.IsUpper(connection[j][0]) || visitedCount[connection[j]] < 1 ||
                             (connection[j] == locationToVisitTwice && visitedCount[connection[j]] < 2)))
                        {
                            List<String> tmpPath = new();
                            tmpPath.AddRange(currentPath);
                            tmpPath.Add(connection[j]);
                            Dictionary<String, Int32> tmpVisitedCount = new(visitedCount);
                            tmpVisitedCount[connection[j]]++;
                            if (Char.IsLower(connection[j][0]) && locationToVisitTwice == null)
                            {
                                possiblePaths.Add((currentPath, tmpVisitedCount, connection[j]));
                                possiblePaths.Add((currentPath, tmpVisitedCount, null));
                            }
                            else
                            {
                                possiblePaths.Add((tmpPath, tmpVisitedCount, locationToVisitTwice));
                            }
                        }
                    }
                }

                List<(List<String>, Dictionary<String, Int32>)> outputPossiblePaths = new();
                //outputPossiblePaths.AddRange(possiblePaths);
                foreach ((List<String> path, Dictionary<String, Int32> visitedCount, String? locationToVisitTwice) item
                         in possiblePaths)
                {
                    outputPossiblePaths.AddRange(GetPaths(item.path, item.visitedCount, item.locationToVisitTwice));
                }

                return outputPossiblePaths;
            }
        }
    }
}