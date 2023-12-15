namespace Year2021
{
    public class Day08 : IDay
    {
        public object Sol1(string input)
        {
            string[] outputValues = input.Split('\n').Select(x => x.Split(" | ")[1]).ToArray();
            return outputValues.Sum(values => values.Trim().Split(' ').Count(value => value.Length is 2 or 3 or 4 or 7 or 8));
        }

        public object Sol2(string input)
        {
            string[][] split = input.Split('\n').Select(x => x.Split(" | ")).ToArray();

            int total = 0;

            foreach (string[] line in split)
            {
                string[] inputValues = line[0].Split(' ');
                string?[] segDisplays = new string?[10];
                for (int i = 0; i < 10; i++)
                {
                    foreach (string value in inputValues)
                    {
                        if (value.Length == 2) { segDisplays[1] = value; }
                        else if (value.Length == 4) { segDisplays[4] = value; }
                        else if (value.Length == 3) { segDisplays[7] = value; }
                        else if (value.Length == 7) { segDisplays[8] = value; }
                    }

                    foreach (string value in inputValues)
                    {

                        if (value.Length == 6)
                        {
                            if (segDisplays[4] != null && segDisplays[4]!.All(x => value.Contains(x)))
                            {
                                segDisplays[9] = value;
                            }
                            else if (segDisplays[1] != null && segDisplays[1]!.All(x => value.Contains(x)))
                            {
                                segDisplays[0] = value;
                            }
                            else if (segDisplays[4] != null && segDisplays[1] != default)
                            {
                                segDisplays[6] = value;
                            }
                        }
                        else if (value.Length == 5)
                        {
                            if (segDisplays[1] != null && segDisplays[1]!.All(x => value.Contains(x)))
                            {
                                segDisplays[3] = value;
                            }
                            else if (segDisplays[9] != null && segDisplays[9]!.Count(x => value.Contains(x)) == 4)
                            {
                                segDisplays[2] = value;
                            }
                            else if (segDisplays[1] != null && segDisplays[9] != null)
                            {
                                segDisplays[5] = value;
                            }

                        }
                    }
                }
                string[] outputValues = line[1].Split(' ');
                string output = "";

                foreach (string outputValue in outputValues)
                {
                    for (int i = 0; i < segDisplays.Length; i++)
                    {
                        string segDisplay = segDisplays[i]!;
                        if (outputValue.Length == segDisplay.Length && segDisplay.All(x => outputValue.Contains(x)))
                        {
                            output += i;
                            break;
                        }
                    }
                }

                total += int.Parse(output);
            }


            return total;
        }
    }
}
