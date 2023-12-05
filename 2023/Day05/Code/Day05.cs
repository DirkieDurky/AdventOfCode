namespace Year2023
{
    public class Day05 : IDay
    {
        private class MapRange
        {
            public long RangeStart;
            public long RangeEnd;
            public long Offset;

            public MapRange(long rangeStart, long rangeEnd, long offset)
            {
                RangeStart = rangeStart;
                RangeEnd = rangeEnd;
                Offset = offset;
            }
        }

        public Object Sol1(String input)
        {
            String[] lines = input.Split("\n");
            long[] seeds = lines[0].Split(": ")[1].Split().Select(long.Parse).ToArray();

            List<List<MapRange>> maps = new();

            String[] mapStrings = input.Split("\n\n");
            for (int i = 1; i < mapStrings.Length; i++)
            {
                String mapStr = mapStrings[i];

                List<MapRange> ranges = new();

                String[] mapStrLines = mapStr.Split("\n");
                for (int j = 1; j < mapStrLines.Length; j++)
                {
                    long[] values = mapStrLines[j].Split().Select(long.Parse).ToArray();
                    ranges.Add(new MapRange(values[1], values[1] + values[2] - 1, values[0] - values[1]));
                }

                maps.Add(ranges);
            }

            // foreach (MapRange range in maps[2])
            // {
            //     Console.WriteLine("RangeStart: " + range.RangeStart);
            //     Console.WriteLine("RangeEnd: " + range.RangeEnd);
            //     Console.WriteLine("Offset: " + range.Offset);
            //     Console.WriteLine();
            // }
            // Console.WriteLine();

            // foreach (int seed in seeds)
            // {
            //     Console.Write(seed + " ");
            // }
            // Console.WriteLine();
            foreach (List<MapRange> map in maps)
            {
                for (int i = 0; i < seeds.Length; i++)
                {
                    foreach (MapRange range in map)
                    {
                        if (seeds[i] >= range.RangeStart && seeds[i] <= range.RangeEnd)
                        {
                            seeds[i] += range.Offset;
                            break;
                        }
                    }
                }

                // foreach (int seed in seeds)
                // {
                //     Console.Write(seed + " ");
                // }
                // Console.WriteLine();
            }

            return seeds.Min();
        }

        private class SeedRange
        {
            public long RangeStart;
            public long RangeEnd;

            public SeedRange(long rangeStart, long rangeEnd)
            {
                RangeStart = rangeStart;
                RangeEnd = rangeEnd;
            }
        }

        public Object Sol2(String input)
        {
            String[] lines = input.Split("\n");

            long[] seedValues = lines[0].Split(": ")[1].Split().Select(long.Parse).ToArray();
            List<SeedRange> seedRanges = new();

            for (int i = 0; i < seedValues.Length; i += 2)
            {
                seedRanges.Add(new SeedRange(seedValues[i], seedValues[i] + seedValues[i + 1]));
            }

            String[] mapStrings = input.Split("\n\n");
            List<List<MapRange>> maps = new();

            for (int i = 1; i < mapStrings.Length; i++)
            {
                String mapStr = mapStrings[i];

                List<MapRange> ranges = new();

                String[] mapStrLines = mapStr.Split("\n");
                for (int j = 1; j < mapStrLines.Length; j++)
                {
                    long[] values = mapStrLines[j].Split().Select(long.Parse).ToArray();
                    ranges.Add(new MapRange(values[1], values[1] + values[2] - 1, values[0] - values[1]));
                }

                maps.Add(ranges);
            }

            foreach (List<MapRange> map in maps)
            {
                for (int i = 0; i < seedRanges.Count; i++)
                {
                    foreach (MapRange mapRange in map)
                    {
                        SeedRange seedRange = seedRanges[i];
                        if (mapRange.RangeStart <= seedRange.RangeStart && mapRange.RangeEnd >= seedRange.RangeEnd)
                        {
                            seedRange.RangeStart += mapRange.Offset;
                            seedRange.RangeEnd += mapRange.Offset;
                            break;
                        }
                        else if (mapRange.RangeEnd >= seedRange.RangeStart && mapRange.RangeEnd <= seedRange.RangeEnd)
                        {
                            seedRanges.Insert(0, new SeedRange(seedRange.RangeStart + mapRange.Offset, mapRange.RangeEnd + mapRange.Offset));
                            i++;
                            seedRange.RangeStart = mapRange.RangeEnd + 1;
                            break;
                        }
                        else if (mapRange.RangeStart >= seedRange.RangeStart && mapRange.RangeStart <= seedRange.RangeEnd)
                        {
                            seedRanges.Insert(0, new SeedRange(mapRange.RangeStart + mapRange.Offset, seedRange.RangeEnd + mapRange.Offset));
                            i++;
                            seedRange.RangeEnd = mapRange.RangeStart - 1;
                            break;
                        }
                    }
                    // Console.WriteLine("RangeStart: " + mapRange.RangeStart);
                    // Console.WriteLine("RangeEnd: " + mapRange.RangeEnd);
                    // Console.WriteLine("Offset: " + mapRange.Offset);
                    // Console.WriteLine();

                    // Console.WriteLine("{");
                    // foreach (SeedRange seedRange in seedRanges)
                    // {
                    //     Console.WriteLine(seedRange.RangeStart + " - " + seedRange.RangeEnd);
                    // }
                    // Console.WriteLine("}");
                    // Console.WriteLine();
                }
            }

            return seedRanges.MinBy(x => x.RangeStart)!.RangeStart;
        }
    }
}
