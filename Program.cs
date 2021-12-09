﻿internal class Program
{
    static void Main(String[] args)
    {
        Type[] problems = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IDay).IsAssignableFrom(p) && p.IsClass).ToArray();
        if (args.Length > 0 && args[0] == "all")
        {
            foreach (Type problem in problems)
            {
                PrintProblem((IDay)Activator.CreateInstance(problem)!);
            }
        }
        else if (args.Length > 0 && args[0].Any(char.IsDigit))
        {
            string year;
            string day;
            if (args.Length > 1)
            {
                if (args[0].Length == 4)
                {
                    year = args[0].Substring(2);
                }
                else if (args[0].Length == 2)
                {
                    year = args[0];
                }
                else throw new ArgumentException("Invalid year");
                day = int.Parse(args[1]).ToString("00");
            }
            else
            {
                year = DateTime.Now.Year.ToString().Substring(2);
                day = int.Parse(args[0]).ToString("00");
            }

            // Type[] matchingProblems = problems.Where(
            //     x => String.Equals(x.Namespace, ("Year" + year), StringComparison.Ordinal) &&
            //     String.Equals(x.Name, ("Day" + day), StringComparison.Ordinal)).ToArray();
            // foreach (Type problem in problems)
            // {
            //     Console.WriteLine(problem.FullName);
            // }
            // Console.WriteLine($"Year{year}.Day{day}");
            Type[] matchingProblems = problems.Where(x => x.FullName == $"Year20{year}.Day{day}").ToArray();
            if (matchingProblems.Length > 0)
            {
                if (matchingProblems.Length > 1)
                {
                    Console.WriteLine("Multiple matches found. Running all");
                }
                foreach (Type problem in matchingProblems)
                {
                    PrintProblem((IDay)Activator.CreateInstance(problem)!);
                }
            }
            else
            {
                Console.WriteLine("No matching result found");
            }
        }
        else if (args.Length > 0 && args[0] == "new")
        {
            NewProject(args);
        }
        else if (args.Length > 0 && args[0] == "latest")
        {
            IDay? lastProblem = (IDay)Activator.CreateInstance(problems.Last())!;
            if (lastProblem != null)
            {
                PrintProblem(lastProblem);
            }
        }
        else if (args.Length > 0 && args[0] == "today")
        {
            String year = DateTime.Now.Year.ToString();
            String day = DateTime.Now.Day.ToString("00");
            Type[] matchingProblems = problems.Where(x => x.FullName == $"Year{year}.Day{day}").ToArray();

            if (matchingProblems.Length > 0)
            {
                if (matchingProblems.Length > 1)
                {
                    Console.WriteLine("Multiple matches found. Running all");
                }

                foreach (Type problem in matchingProblems)
                {
                    PrintProblem((IDay)Activator.CreateInstance(problem)!);
                }

            }
            else
            {
                Console.WriteLine("No match found");
            }
        }
        else
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No input found. Please provide an input.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            Main(Console.ReadLine()!.Split(' '));
        }
    }
    public static void PrintProblem(IDay problem)
    {
        string year = problem.GetType().FullName;
        string day = problem.GetType().FullName;
        Console.WriteLine(problem + ":");
        Console.WriteLine("Solution 1:");
        Console.WriteLine(problem.Sol1(File.ReadAllText($@"C:\Users\DirkFreijters\OneDrive - FYN Benelux BV\.dev\AoC\{year}\Day{day}\Input\input.txt")));
        Console.WriteLine("Solution 2:");
        Console.WriteLine(problem.Sol2(File.ReadAllText($@"C:\Users\DirkFreijters\OneDrive - FYN Benelux BV\.dev\AoC\{year}\Day{day}\Input\input.txt")));
    }

    public static async void SetFileContent(Int32 year, String day)
    {
        Directory.CreateDirectory($@"{year}\Day{day}\Code");
        Directory.CreateDirectory($@"{year}\Day{day}\Input");
        File.Create($@"{year}\Day{day}\Input\input.txt");
        File.Create($@"{year}\Day{day}\Input\small input.txt");

        await File.WriteAllTextAsync($@"{year}\Day{day}\Code\main.cs",
$@"namespace Year{year}
{{
    public class Day{day} : IDay
    {{
        public Object Sol1(String input)
        {{
            return """";
        }}

        public Object Sol2(String input)
        {{            
            return """";
        }}
    }}
}}");
    }

    public static void NewProject(String[] args)
    {
        // Int32 newDay = Int32.Parse(System.IO.Directory.GetDirectories(DateTime.Now.Year.ToString(), "Day ??").Last());
        String currentDay;
        if (args.Length >= 2)
        {
            Int32 currentDayInt;
            if (Int32.TryParse(args[1], out currentDayInt))
            {
                currentDay = currentDayInt.ToString("00");
            }
            else
            {
                Console.WriteLine("Invalid day number");
                return;
            }
        }
        else
        {
            // currentDay = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            // DateTime.UtcNow, "Eastern Standard Time").Day.ToString("00");
            currentDay = DateTime.Now.Day.ToString("00");
        }
        Int32 currentYear = DateTime.Now.Year;

        if (Directory.Exists($@"{currentYear}\Day{currentDay}") && !(args.Length >= 3 && args[2] == "--f"))
        {
            Console.WriteLine($"There is already an existing folder for day {currentDay}. Overwrite it? (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                SetFileContent(currentYear, currentDay);
            }
        }
        else
        {
            SetFileContent(currentYear, currentDay);
        }
    }
}