namespace Year2021
{
    public class Day02 : IDay
    {
        public Object Sol1(String input)
        {
            int hor = 0;
            int depth = 0;
            foreach (string line in input.Split('\n'))
            {
                string command = line.Split(' ')[0];
                int units = int.Parse(line.Split(' ')[1]);
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
            int hor = 0;
            int depth = 0;
            int aim = 0;
            foreach (string line in input.Split('\n'))
            {
                string command = line.Split(' ')[0];
                int units = int.Parse(line.Split(' ')[1]);
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