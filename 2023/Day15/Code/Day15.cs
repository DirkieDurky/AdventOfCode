namespace Year2023
{
    public class Day15 : IDay
    {
        public object Sol1(string input)
        {
            string[] steps = input.Split(',');

            int sum = 0;

            foreach (string step in steps)
            {
                sum += Hash(step);
            }

            return sum;
        }

        public object Sol2(string input)
        {
            string[] steps = input.Split(',');
            Dictionary<string, int>[] boxes = new Dictionary<string, int>[256];

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new Dictionary<string, int>();
            }

            foreach (string step in steps)
            {
                if (step.Contains('='))
                {
                    string[] split = step.Split('=');
                    string label = split[0];
                    int boxIndex = Hash(split[0]);
                    int focalLength = int.Parse(split[1]);
                    if (boxes[boxIndex].ContainsKey(label))
                    {
                        boxes[boxIndex][label] = focalLength;
                    }
                    else
                    {
                        boxes[boxIndex] = ReverseDictionary(boxes[boxIndex]);
                        boxes[boxIndex].Add(label, focalLength);
                        boxes[boxIndex] = ReverseDictionary(boxes[boxIndex]);
                    }
                }
                else
                {
                    string label = step.Replace("-", "");
                    int boxIndex = Hash(label);

                    boxes[boxIndex].Remove(label);
                }
            }

            int sum = 0;

            for (int i = 0; i < boxes.Length; i++)
            {
                Dictionary<string, int> box = boxes[i];
                box = ReverseDictionary(box);
                foreach ((string label, int value) in box)
                {
                    Console.WriteLine(label + ": box " + (i + 1) + " * " + (box.Keys.ToList().IndexOf(label) + 1) + " * " + value + " = " + (i + 1) * (box.Keys.ToList().IndexOf(label) + 1) * value);
                    sum += (i + 1) * (box.Keys.ToList().IndexOf(label) + 1) * value;
                }
            }

            return sum;
        }

        internal int Hash(string str)
        {
            int value = 0;
            foreach (char c in str)
            {
                value += c;
                value *= 17;
                value = value % 256;
            }
            return value;
        }

        internal Dictionary<string, int> ReverseDictionary(Dictionary<string, int> dictionary)
        {
            Dictionary<string, int> newDictionary = new();

            for (int i = dictionary.Count() - 1; i >= 0; i--)
            {
                KeyValuePair<string, int> keyValuePair = dictionary.ElementAt(i);
                newDictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return newDictionary;
        }
    }
}
