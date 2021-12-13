namespace Year2021
{
    public class Day08 : IDay
    {
        public Object Sol1(String input)
        {
            String[] outputValues = input.Split('\n').Select(x => x.Split(" | ")[1]).ToArray();
            return outputValues.Sum(values => values.Trim().Split(' ').Count(value => value.Length is 2 or 3 or 4 or 7 or 8));
        }

        public Object Sol2(String input)
        {
            String[][] split = input.Split('\n').Select(x => x.Split(" | ")).ToArray();
            
            Int32 total = 0;

            foreach (String[] line in split)
            {
                String[] inputValues = line[0].Split(' ');
                String?[] segDisplays = new String?[10];
                for (Int32 i = 0; i < 10; i++)
                {
                    foreach (String value in inputValues)
                    {
                        if (value.Length == 2) { segDisplays[1] = value; }
                        else if (value.Length == 4) { segDisplays[4] = value; }
                        else if (value.Length == 3) { segDisplays[7] = value; }
                        else if (value.Length == 7) { segDisplays[8] = value; }
                    }

                    foreach (String value in inputValues)
                    {

                        if (value.Length == 6)
                        {
                            if (segDisplays[4] != null && segDisplays[4]!.All(x=>value.Contains(x)))
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
                String[] outputValues = line[1].Split(' ');
                String output = "";

                foreach (String outputValue in outputValues)
                {
                    for (Int32 i = 0; i < segDisplays.Length; i++)
                    {
                        String segDisplay = segDisplays[i]!;
                        if (outputValue.Length == segDisplay.Length && segDisplay.All(x => outputValue.Contains(x)))
                        {
                            output += i;
                            break;
                        }
                    }
                }

                total += Int32.Parse(output);
            }


            return total;
        }
    }
}