namespace Year2021
{
    public class Day06 : IDay
    {
        public Object Sol1(String input)
        {
            int[] lines = input.Split(',').Select(i => Int32.Parse(i)).ToArray();
            List<int> fishes = new();
            foreach (int line in lines)
            {
                fishes.Add(line);
            }
            for (int i = 0; i < 80; i++)
            {
                int count = fishes.Count;
                for (int j = 0; j < count; j++)
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
            long[] lines = input.Split(',').Select(i => long.Parse(i)).ToArray();

            var groups = lines
            .GroupBy(s => s)
            .Select(s => new
            {
                Stuff = s.Key,
                Count = s.Count()
            });

            Dictionary<long, long> dictionary = groups.ToDictionary(g => g.Stuff, g => (long)g.Count);

            Dictionary<long, long> fish = new() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };

            foreach (KeyValuePair<long, long> keyValuePair in dictionary)
            {
                fish[keyValuePair.Key] = keyValuePair.Value;
            }

            // Console.WriteLine(String.Join(',', fish));

            for (long i = 0; i < 256; i++)
            {
                fish[9] += fish[0];
                fish[7] += fish[0];
                for (int j = 0; j < 9; j++)
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