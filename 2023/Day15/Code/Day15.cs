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
            Dictionary<string, Lens>[] boxes = new Dictionary<string, Lens>[256];

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new Dictionary<string, Lens>();
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
                        // Console.WriteLine(label + " edited in box " + boxIndex);
                        boxes[boxIndex][label].FocalLength = focalLength;
                    }
                    else
                    {
                        // Console.WriteLine(label + " added to box " + boxIndex + " with index " + (boxes[boxIndex].Count() + 1));
                        int newIndex = 0;
                        if (boxes[boxIndex].Any()) newIndex = boxes[boxIndex].MaxBy(x => x.Value.Index).Value.Index + 1;
                        boxes[boxIndex].Add(label, new Lens(label, newIndex, focalLength));
                    }
                }
                else
                {
                    string label = step.Replace("-", "");
                    int boxIndex = Hash(label);

                    if (!boxes[boxIndex].ContainsKey(label)) continue;
                    int index = boxes[boxIndex][label].Index;
                    boxes[boxIndex].Remove(label);
                    // Console.WriteLine(label + " removed from box " + boxIndex);
                    foreach ((string currentLabel, Lens lens) in boxes[boxIndex])
                    {
                        if (lens.Index > index)
                        {
                            lens.Index--;
                            // Console.WriteLine("Index of " + currentLabel + " is now " + lens.Index);
                        }
                    }
                }
            }

            int sum = 0;

            for (int i = 0; i < boxes.Length; i++)
            {
                Dictionary<string, Lens> box = boxes[i];
                foreach ((string label, Lens lens) in box)
                {
                    // Console.WriteLine(label + ": " + (i + 1) + " (box " + i + ") * " + (lens.Index + 1) + " (slot) * " + lens.FocalLength + " (focal length) = " + (i + 1) * (lens.Index + 1) * lens.FocalLength);
                    sum += (i + 1) * (lens.Index + 1) * lens.FocalLength;
                }
            }

            return sum;
        }

        class Lens
        {
            public string Label;
            public int FocalLength;
            public int Index;

            public Lens(string label, int index, int value)
            {
                Label = label;
                Index = index;
                FocalLength = value;
            }
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
    }
}
