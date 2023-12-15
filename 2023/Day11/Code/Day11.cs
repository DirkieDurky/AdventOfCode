using System.Drawing;

namespace Year2023
{
    public class Day11 : IDay
    {
        public long CalculateDistance(string input, int expansionAmount)
        {
            string[] lines = input.Split('\n');

            List<Point> galaxies = new();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#') galaxies.Add(new Point(x, y));
                }
            }

            List<int> rowsWithoutGalaxy = new();
            List<int> columnsWithoutGalaxy = new();

            for (int y = 0; y < lines.Length; y++)
            {
                if (!lines[y].Any(c => c == '#')) rowsWithoutGalaxy.Add(y);
            }

            for (int x = 0; x < lines[0].Length; x++)
            {
                bool isEmpty = true;
                for (int y = 0; y < lines.Length; y++)
                {
                    if (lines[y][x] == '#')
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty) columnsWithoutGalaxy.Add(x);
            }

            long sum = 0;
            Dictionary<Point, List<Point>> doneGalaxies = new();

            foreach (Point galaxy in galaxies)
            {
                foreach (Point otherGalaxy in galaxies)
                {
                    if ((doneGalaxies.ContainsKey(otherGalaxy) && doneGalaxies[otherGalaxy].Contains(galaxy)) || galaxy == otherGalaxy) continue;
                    if (doneGalaxies.ContainsKey(galaxy))
                    {
                        doneGalaxies[galaxy].Add(otherGalaxy);
                    }
                    else
                    {
                        doneGalaxies.Add(galaxy, new() { otherGalaxy });
                    }

                    sum += Math.Abs(galaxy.X - otherGalaxy.X);
                    sum += Math.Abs(galaxy.Y - otherGalaxy.Y);
                    sum += columnsWithoutGalaxy.Count(x => x > Math.Min(galaxy.X, otherGalaxy.X) && x < Math.Max(galaxy.X, otherGalaxy.X)) * expansionAmount;
                    sum += rowsWithoutGalaxy.Count(y => y > Math.Min(galaxy.Y, otherGalaxy.Y) && y < Math.Max(galaxy.Y, otherGalaxy.Y)) * expansionAmount;
                }
            }

            return sum / 2;
        }
        public object Sol1(string input)
        {
            return CalculateDistance(input, 1);
        }

        public object Sol2(string input)
        {
            return CalculateDistance(input, 999999);
        }
    }
}
