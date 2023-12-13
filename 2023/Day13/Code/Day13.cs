using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace Year2023
{
    public class Day13 : IDay
    {
        //(int reflectionIndex, int reflectionSize, bool orientation: (0 => horizontal, 1 => vertical))
        internal List<(int, int, bool)> GetReflectionPoints(String[] map)
        {
            List<(int, int, bool)> reflectionPoints = new();

            List<(int, int)> horizontalReflections = GetHorizontalReflectionPoints(map);

            foreach ((int, int) horizontalReflection in horizontalReflections)
            {
                reflectionPoints.Add((horizontalReflection.Item1, horizontalReflection.Item2, true));
            }

            String[] transposedLines = new String[map[0].Length];

            for (int x = 0; x < map[0].Length; x++)
            {
                StringBuilder line = new();
                for (int y = map.Length - 1; y >= 0; y--)
                {
                    line.Append(map[y][x]);
                }
                transposedLines[x] = line.ToString();
            }

            List<(int, int)> verticalReflections = GetHorizontalReflectionPoints(transposedLines);

            foreach ((int, int) verticalReflection in verticalReflections)
            {
                reflectionPoints.Add((verticalReflection.Item1, verticalReflection.Item2, false));
            }

            // if (horizontalReflection.Item1 is null && verticalReflection.Item1 is null) throw new Exception("No reflection point found!");

            return reflectionPoints;
        }

        internal List<(int, int)> GetHorizontalReflectionPoints(String[] map)
        {
            String? lastLine = null;

            List<(int, int)> reflectionPoints = new();

            for (int i = 0; i < map.Length; i++)
            {
                String line = map[i];
                if (lastLine is not null && line == lastLine)
                {
                    bool isReflection = true;
                    int currentReflectionSize = 1;
                    int j = 1;

                    while (i - 1 - j >= 0 && i + j < map.Length)
                    {
                        if (map[i - 1 - j] != map[i + j])
                        {
                            isReflection = false;
                            break;
                        }
                        currentReflectionSize++;
                        j++;
                    }

                    if (!isReflection) continue;
                    reflectionPoints.Add((i, currentReflectionSize));
                }
                lastLine = line;
            }

            return reflectionPoints;
        }
        public Object Sol1(String input)
        {
            String[] maps = input.Split("\n\n");

            int sum = 0;

            foreach (String map in maps)
            {
                String[] lines = map.Split('\n');

                (int, int, bool) reflection = GetReflectionPoints(lines)!.First();

                // ShowReflectionPoint(lines, reflection);

                if (reflection.Item3)
                {
                    sum += reflection.Item1! * 100;
                }
                else
                {
                    sum += reflection.Item1!;
                }
            }

            return sum;
        }

        internal (int, int, bool) GetReflectionPointAfterFixingSpeck(String[] lines)
        {
            (int, int, bool) originalReflection = GetReflectionPoints(lines)!.First();

            // Console.WriteLine("Before:");
            // ShowReflectionPoint(lines, originalReflection);

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    String[] newMap = (String[])lines.Clone();

                    StringBuilder temp = new(newMap[y]);
                    temp[x] = temp[x] == '.' ? '#' : '.';
                    newMap[y] = temp.ToString();

                    IEnumerable<(int, int, bool)> newReflectionPoints = GetReflectionPoints(newMap).Where(x => x != originalReflection);

                    if (!newReflectionPoints.Any()) continue;
                    (int, int, bool)? newReflectionPoint = newReflectionPoints.First();

                    // Console.WriteLine("After:");
                    // ShowReflectionPoint(newMap, ((int, int, bool))newReflectionPoint);
                    return ((int, int, bool))newReflectionPoint;
                }
            }

            throw new Exception("Nothing found");
        }

        public Object Sol2(String input)
        {
            String[] maps = input.Split("\n\n");

            int sum = 0;

            foreach (String map in maps)
            {
                String[] lines = map.Split('\n');

                (int, int, bool) reflection = GetReflectionPointAfterFixingSpeck(lines);

                // ShowReflectionPoint(lines, reflection);

                if (reflection.Item3)
                {
                    sum += reflection.Item1! * 100;
                }
                else
                {
                    sum += reflection.Item1!;
                }
            }

            return sum;
        }

        internal void ShowReflectionPoint(String[] map, (int, int, bool) reflection)
        {
            if (reflection.Item3)
            {
                Console.WriteLine("Horizontal: " + reflection.Item1!);
                for (int i = 0; i < map.Length; i++)
                {
                    if (i == reflection.Item1! - 1)
                    {
                        Console.Write("v");
                    }
                    else if (i == reflection.Item1!)
                    {
                        Console.Write("^");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write(map[i]);
                    if (i == reflection.Item1! - 1)
                    {
                        Console.WriteLine("v");
                    }
                    else if (i == reflection.Item1!)
                    {
                        Console.WriteLine("^");
                    }
                    else
                    {
                        Console.WriteLine(" ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Vertical: " + reflection.Item1!);
                for (int i = 0; i < map[0].Length; i++)
                {
                    if (i == reflection.Item1! - 1)
                    {
                        Console.Write(">");
                    }
                    else if (i == reflection.Item1!)
                    {
                        Console.Write("<");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
                for (int i = 0; i < map.Length; i++)
                {
                    Console.WriteLine(map[i]);
                }
                for (int i = 0; i < map[0].Length; i++)
                {
                    if (i == reflection.Item1! - 1)
                    {
                        Console.Write(">");
                    }
                    else if (i == reflection.Item1!)
                    {
                        Console.Write("<");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
