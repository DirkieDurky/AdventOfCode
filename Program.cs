using System.Diagnostics;
using System.Text.RegularExpressions;

internal class Program
{
    private static string RootFolder = default!;

    static void Main(string[] args)
    {
        RootFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
        // RootFolder = @"D:\dev\AoC\";
        // RootFolder = @"C:\DirkData\personal\dev\adventofcode\";
        Console.ForegroundColor = ConsoleColor.Gray;

        Type[] problems = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IDay).IsAssignableFrom(p) && p.IsClass)
            .ToArray();
        IDay[] days = problems.Select(x => (IDay)Activator.CreateInstance(x)!).ToArray();

        List<(Func<string, object>, int)> matchingFuncs = new();
        if (args.Length == 0)
        {
            Console.WriteLine("No input found. Please provide an input.");
            Main(Console.ReadLine()!.Split(' '));
            return;
        }

        if (args[0].Equals("help", StringComparison.OrdinalIgnoreCase) ||
            args[0].Equals("?", StringComparison.OrdinalIgnoreCase))
        {
            PrintHelp(ConsoleColor.Gray);
            Main(Console.ReadLine()!.Split(' '));
            return;
        }

        if (args[0].Equals("new", StringComparison.OrdinalIgnoreCase))
        {
            NewProject(args);
            return;
        }
        else if (args[0].Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            matchingFuncs.AddRange(days.SelectMany(x => new (Func<string, object>, int)[] { (x.Sol1, 1), (x.Sol2, 2) })
                .ToArray());
        }
        else if (args.All(arg => Regex.IsMatch(arg,
                     @"^((\d{4}|\d{2}|latest)(\/(\d{1,2}|latest|today))?(-\d)?)$|^(((\d{1,2}|latest|today))(-\d)?)$",
                     RegexOptions.Multiline)))
        {
            foreach (string arg in args)
            {
                int year = 0;
                int day = 0;
                int part = 0;

                string[] split = arg.Split('/', '-');
                if (arg.Contains('/') && arg.Contains('-'))
                {
                    if (split[0] == "latest")
                    {
                        year = int.Parse(Directory.GetDirectories(RootFolder).Last());
                    }
                    else if (split[0].Length == 4)
                    {
                        year = int.Parse(split[0]);
                    }
                    else if (split[0].Length == 2)
                    {
                        year = int.Parse("20" + split[0]);
                    }

                    if (split[1] == "latest")
                    {
                        day = int.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = int.Parse(split[1]);
                    }

                    part = int.Parse(split[2]);
                }
                else if (arg.Contains('/'))
                {
                    if (split[0] == "latest")
                    {
                        year = int.Parse(Directory.GetDirectories(RootFolder).Last());
                    }
                    else if (split[0].Length == 4)
                    {
                        year = int.Parse(split[0]);
                    }
                    else if (split[0].Length == 2)
                    {
                        year = int.Parse("20" + split[0]);
                    }

                    if (split[1] == "latest")
                    {
                        day = int.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = int.Parse(split[1]);
                    }
                }
                else if (arg.Contains('-'))
                {
                    year = DateTime.Now.Year;
                    if (split[1] == "latest")
                    {
                        day = int.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = int.Parse(split[0]);
                    }

                    part = int.Parse(split[1]);
                }
                else
                {
                    if (split[0] == "latest")
                    {
                        year = DateTime.Now.Year;
                        day = int.Parse(split[0]);
                    }

                    if (split[0].Length == 4)
                    {
                        year = int.Parse(split[0]);
                    }
                    else if (split[0].Length is 1 or 2)
                    {
                        year = DateTime.Now.Year;
                        day = int.Parse(split[0]);
                    }
                }

                if (year == 0)
                {
                    PrintLine(ConsoleColor.Red, "Invalid input. Valid options are:");
                    PrintHelp(ConsoleColor.Red);
                    Main(Console.ReadLine()!.Split(' '));
                    return;
                }

                if (part == 0)
                {
                    matchingFuncs.AddRange(
                        problems.Where(x =>
                                x.FullName!.Split('.')[0] == "Year" + year &&
                                (x.FullName.Split('.')[1] == $"Day{day:00}" || day == 0))
                            .Select(x => (IDay)Activator.CreateInstance(x)!).ToArray()
                            .SelectMany(x => new (Func<string, object>, int)[] { (x.Sol1, 1), (x.Sol2, 2) })
                    );
                }
                else
                {
                    IDay[] matchingDays = problems.Where(x =>
                            x.FullName!.Split('.')[0] == "Year" + year && x.FullName.Split('.')[1] == $"Day{day:00}")
                        .Select(x => (IDay)Activator.CreateInstance(x)!).ToArray();

                    if (part == 1)
                    {
                        matchingFuncs.AddRange(matchingDays.SelectMany(x => new (Func<string, object>, int)[]
                            {(x.Sol1, 1)}));
                    }
                    else if (part == 2)
                    {
                        matchingFuncs.AddRange(matchingDays.SelectMany(x => new (Func<string, object>, int)[]
                            {(x.Sol2, 2)}));
                    }
                    else
                    {
                        PrintLine(ConsoleColor.Red, "Invalid part");
                        Main(Console.ReadLine()!.Split(' '));
                        return;
                    }
                }
            }
        }
        else
        {
            PrintLine(ConsoleColor.Red, "Invalid input. Valid options are:");
            PrintHelp(ConsoleColor.Red);
            Main(Console.ReadLine()!.Split(' '));
            return;
        }

        if (matchingFuncs.Count > 0)
        {
            PrintProblems(matchingFuncs.ToArray(), matchingFuncs.Count > 1);
        }
        else
        {
            PrintLine(ConsoleColor.Yellow, "No matches found");
            Main(Console.ReadLine()!.Split(' '));
            return;
        }
    }

    public static double PrintProblem(Func<string, object> problem, int solutionNumber)
    {
        double totalElapsedTime = 0;
        Stopwatch stopwatch = new();
        string[] fullName = problem.Target!.GetType().FullName!.Split('.');
        string year = fullName[0][4..];
        string day = fullName[1][3..];
        Console.WriteLine($"\n{year}/{day}-{solutionNumber}:");
            string input = File.ReadAllText($@"{RootFolder}{year}\Day{day}\Input\used input.txt");
            stopwatch.Start();
            object output = problem.Invoke(input);
            stopwatch.Stop();
            Print(ConsoleColor.White, output);
            PrintLine(ConsoleColor.DarkGray, $" : {stopwatch.Elapsed.TotalMilliseconds}ms");
            totalElapsedTime += stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Reset();

        return totalElapsedTime;
    }

    public static void PrintProblems((Func<string, object>, int)[] problems, bool printTotal)
    {
        double totalElapsedTime = 0;
        foreach ((Func<string, object>, int) problem in problems)
        {
            totalElapsedTime += PrintProblem(problem.Item1, problem.Item2);
        }

        if (printTotal)
        {
            Console.Write("\nTotal time: ");
            PrintLine(ConsoleColor.DarkGray, $"{Math.Round(totalElapsedTime, 4)}ms");
        }
        // Console.ReadLine();
    }

    public static void Print(ConsoleColor color, object message)
    {
        ConsoleColor previousConsoleColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = previousConsoleColor;
    }

    public static void PrintLine(ConsoleColor color, object message)
    {
        ConsoleColor previousConsoleColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = previousConsoleColor;
    }

    public static async void SetFileContent(int year, string day)
    {
        Directory.CreateDirectory($@"{RootFolder}{year}\Day{day}\Code");
        Directory.CreateDirectory($@"{RootFolder}{year}\Day{day}\Input");
        File.Create($@"{RootFolder}{year}\Day{day}\Input\original input.txt");
        File.Create($@"{RootFolder}{year}\Day{day}\Input\testing input.txt");
        File.Create($@"{RootFolder}{year}\Day{day}\Input\used input.txt");

        await File.WriteAllTextAsync($@"{RootFolder}{year}\Day{day}\Code\Day{day}.cs",
            $@"namespace Year{year}
{{
    public class Day{day} : IDay
    {{
        public object Sol1(string input)
        {{


            return 0;
        }}

        public object Sol2(string input)
        {{      


            return 0;
        }}
    }}
}}");
    }

    public static void NewProject(string[] args)
    {
        // Int32 newDay = Int32.Parse(System.IO.Directory.GetDirectories(DateTime.Now.Year.ToString(), "Day ??").Last());
        string currentDay;
        if (args.Length >= 2)
        {
            if (int.TryParse(args[1], out int currentDayInt))
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

        int currentYear = DateTime.Now.Year;

        if (Directory.Exists($@"{RootFolder}{currentYear}\Day{currentDay}") && !(args.Length >= 3 && args[2] == "--f"))
        {
            Console.WriteLine($"There is already an existing folder for day {currentDay}. Overwrite it? (y/N)");
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

    public static void PrintHelp(ConsoleColor color)
    {
        PrintLine(color,
            $@"[YYYY]/[DD]-[P]
[YYYY]/[DD]
[YYYY]
[DD]-[P]
[DD]
You can use the 2 number variant of a year (e.g {DateTime.Now.Year.ToString()[2..]} instead of {DateTime.Now.Year})
You can use the ""today"" keyword to refer to today insted of manually typing out todays date
You can use the ""latest"" keyword to refer to the latest year or date");
    }
}
