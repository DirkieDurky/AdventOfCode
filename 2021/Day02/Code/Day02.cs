namespace Year2021
{
    public class Day02 : IDay
    {
        public Object Sol1(String input)
        {
            Int32 hor = 0;
            Int32 depth = 0;
            foreach (String line in input.Split('\n'))
            {
                String command = line.Split(' ')[0];
                Int32 units = Int32.Parse(line.Split(' ')[1]);
                switch (command)
                {
                    case "forward": hor += units; break;
                    case "down": depth += units; break;
                    case "up": depth -= units; break;
                }
            }

            return hor * depth;
        }

        public Object Sol2(String input)
        {
            Int32 hor = 0;
            Int32 depth = 0;
            Int32 aim = 0;
            foreach (String line in input.Split('\n'))
            {
                String command = line.Split(' ')[0];
                Int32 units = Int32.Parse(line.Split(' ')[1]);
                switch (command)
                {
                    case "forward": hor += units; depth += aim * units; break;
                    case "down": aim += units; break;
                    case "up": aim -= units; break;
                }
            }

            return hor * depth;
        }
    }
}