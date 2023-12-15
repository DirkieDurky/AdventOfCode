
using System.Data.Common;

namespace Year2021
{
    public class Day12 : IDay
    {
        public object Sol1(string input)
        {
            string[][] connections = input.Split("\n").Select(x => x.Split('-').ToArray()).ToArray();

            Dictionary<string, int> initialVisitedCount = new();
            foreach (string[] connection in connections)
            {
                foreach (string location in connection)
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

            List<(List<string>, Dictionary<string, int>)> paths = GetPaths(new List<string> { "start" }, initialVisitedCount);
            List<(List<string>, Dictionary<string, int>)> filtered = paths.Where(x => x.Item1.Last() == "end").ToList();

            foreach ((List<string>? path, Dictionary<string, int> visitedCount) in filtered)
            {
                string output = "";
                for (int i = 0; i < path.Count; i++)
                {
                    output += path[i];
                    if (i != path.Count - 1) output += " -> ";
                }

                Console.WriteLine(output);
            }

            return filtered.Count;

            List<(List<string>, Dictionary<string, int>)> GetPaths(List<string> currentPath, Dictionary<string, int> visitedCount)
            {
                if (currentPath.Last() == "end") return new List<(List<string>, Dictionary<string, int>)> { (currentPath, visitedCount) };
                List<(List<string>, Dictionary<string, int>)> possiblePaths = new();
                foreach (string[] connection in connections)
                {
                    if (connection[0] == currentPath.Last() && (char.IsUpper(connection[1][0]) || visitedCount[connection[1]] < 1))
                    {
                        List<string> tmpPath = new();
                        tmpPath.AddRange(currentPath);
                        tmpPath.Add(connection[1]);
                        Dictionary<string, int> tmpVisitedCount = new(visitedCount);
                        tmpVisitedCount[connection[1]]++;
                        possiblePaths.Add((tmpPath, tmpVisitedCount));
                    }
                    else if (connection[1] == currentPath.Last() && (char.IsUpper(connection[0][0]) || visitedCount[connection[0]] < 1))
                    {
                        List<string> tmpPath = new();
                        tmpPath.AddRange(currentPath);
                        tmpPath.Add(connection[0]);
                        Dictionary<string, int> tmpVisitedCount = new(visitedCount);
                        tmpVisitedCount[connection[0]]++;
                        possiblePaths.Add((tmpPath, tmpVisitedCount));
                    }
                }

                List<(List<string>, Dictionary<string, int>)> outputPossiblePaths = new();
                //outputPossiblePaths.AddRange(possiblePaths);
                foreach ((List<string> path, Dictionary<string, int> visitedCount) item in possiblePaths)
                {
                    outputPossiblePaths.AddRange(GetPaths(item.path, item.visitedCount));
                }
                return outputPossiblePaths;
            }
        }

        public object Sol2(string input)
        {
            string[][] connections = input.Split("\n").Select(x => x.Split('-').ToArray()).ToArray();

            Dictionary<string, int> initialVisitedCount = new();
            foreach (string[] connection in connections)
            {
                foreach (string location in connection)
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

            List<(List<string>, Dictionary<string, int>)> paths = GetPaths(new List<string> { "start" }, initialVisitedCount, null);
            List<(List<string>, Dictionary<string, int>)> filtered = paths.Where(x => x.Item1.Last() == "end").ToList();

            foreach ((List<string>? path, Dictionary<string, int> visitedCount) in filtered)
            {
                string output = "";
                for (int i = 0; i < path.Count; i++)
                {
                    output += path[i];
                    if (i != path.Count - 1) output += " -> ";
                }

                Console.WriteLine(output);
            }

            return filtered.Count;


            List<(List<string>, Dictionary<string, int>)> GetPaths(List<string> currentPath, Dictionary<string, int> visitedCount, string? locationToVisitTwice)
            {
                if (currentPath.Last() == "end") return new List<(List<string>, Dictionary<string, int>)> { (currentPath, visitedCount) };
                List<(List<string>, Dictionary<string, int>, string?)> possiblePaths = new();
                foreach (string[] connection in connections)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int j = i == 0 ? 1 : 0;
                        if (connection[i] == currentPath.Last() &&
                            (char.IsUpper(connection[j][0]) || visitedCount[connection[j]] < 1 ||
                             (connection[j] == locationToVisitTwice && visitedCount[connection[j]] < 2)))
                        {
                            List<string> tmpPath = new();
                            tmpPath.AddRange(currentPath);
                            tmpPath.Add(connection[j]);
                            Dictionary<string, int> tmpVisitedCount = new(visitedCount);
                            tmpVisitedCount[connection[j]]++;
                            if (char.IsLower(connection[j][0]) && locationToVisitTwice == null)
                            {
                                possiblePaths.Add((currentPath, tmpVisitedCount, connection[j]));
                            }
                            else
                            {
                                possiblePaths.Add((tmpPath, tmpVisitedCount, locationToVisitTwice));
                            }
                        }
                    }
                }

                List<(List<string>, Dictionary<string, int>)> outputPossiblePaths = new();
                //outputPossiblePaths.AddRange(possiblePaths);
                foreach ((List<string> path, Dictionary<string, int> visitedCount, string? locationToVisitTwice) item
                         in possiblePaths)
                {
                    outputPossiblePaths.AddRange(GetPaths(item.path, item.visitedCount, item.locationToVisitTwice));
                }

                return outputPossiblePaths;
            }
        }
    }
}
