namespace Year2021
{
    public class Day06 : IDay
    {
        public Object Sol1(String input)
        {
            Int32[] lines = input.Split(',').Select(i => Int32.Parse(i)).ToArray();
            List<Int32> fishes = new();
            foreach (Int32 line in lines)
            {
                fishes.Add(line);
            }
            for (Int32 i = 0; i < 80; i++)
            {
                Int32 count = fishes.Count;
                for (Int32 j = 0; j < count; j++)
                {
                    if (fishes[j] == 0)
                    {
                        fishes.Add(8);
                        fishes[j] = 6;
                    }
                    else
                    {
                        fishes[j]--;
                    }
                }
            }
            return fishes.Count;
        }

        public Object Sol2(String input)
        {
            Int64[] lines = input.Split(',').Select(i => Int64.Parse(i)).ToArray();

            var groups = lines
            .GroupBy(s => s)
            .Select(s => new
            {
                Stuff = s.Key,
                Count = s.Count()
            });

            Dictionary<Int64, Int64> dictionary = groups.ToDictionary(g => g.Stuff, g => (Int64)g.Count);

            Dictionary<Int64, Int64> fish = new() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };

            foreach (KeyValuePair<Int64, Int64> keyValuePair in dictionary)
            {
                fish[keyValuePair.Key] = keyValuePair.Value;
            }

            // Console.WriteLine(String.Join(',', fish));

            for (Int64 i = 0; i < 256; i++)
            {
                fish[9] += fish[0];
                fish[7] += fish[0];
                for (Int32 j = 0; j < 9; j++)
                {
                    fish[j] = fish[j + 1];
                }
                fish[9] = 0;
                // Console.WriteLine(String.Join(',', fish));
            }
            return fish.Sum(x => x.Value);
        }
    }
}