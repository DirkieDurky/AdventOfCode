using System.Diagnostics;
using System.Text.RegularExpressions;

internal class Program
{
    private static String RootFolder = default!;

    static void Main(String[] args)
    {
        RootFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
        // RootFolder = @"D:\dev\AoC\";
        Console.ForegroundColor = ConsoleColor.Gray;

        Type[] problems = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IDay).IsAssignableFrom(p) && p.IsClass)
            .ToArray();
        IDay[] days = problems.Select(x => (IDay) Activator.CreateInstance(x)!).ToArray();

        List<(Func<String, Object>, Int32)> matchingFuncs = new();
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
            matchingFuncs.AddRange(days.SelectMany(x => new (Func<String, Object>, Int32)[] {(x.Sol1, 1), (x.Sol2, 2)})
                .ToArray());
        }
        else if (args.All(arg => Regex.IsMatch(arg,
                     @"^((\d{4}|\d{2}|latest)(\/(\d{1,2}|latest|today))?(-\d)?)$|^(((\d{1,2}|latest|today))(-\d)?)$",
                     RegexOptions.Multiline)))
        {
            foreach (String arg in args)
            {
                Int32 year = 0;
                Int32 day = 0;
                Int32 part = 0;

                String[] split = arg.Split('/', '-');
                if (arg.Contains('/') && arg.Contains('-'))
                {
                    if (split[0] == "latest")
                    {
                        year = Int32.Parse(Directory.GetDirectories(RootFolder).Last());
                    }
                    else if (split[0].Length == 4)
                    {
                        year = Int32.Parse(split[0]);
                    }
                    else if (split[0].Length == 2)
                    {
                        year = Int32.Parse("20" + split[0]);
                    }

                    if (split[1] == "latest")
                    {
                        day = Int32.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = Int32.Parse(split[1]);
                    }

                    part = Int32.Parse(split[2]);
                }
                else if (arg.Contains('/'))
                {
                    if (split[0] == "latest")
                    {
                        year = Int32.Parse(Directory.GetDirectories(RootFolder).Last());
                    }
                    else if (split[0].Length == 4)
                    {
                        year = Int32.Parse(split[0]);
                    }
                    else if (split[0].Length == 2)
                    {
                        year = Int32.Parse("20" + split[0]);
                    }

                    if (split[1] == "latest")
                    {
                        day = Int32.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = Int32.Parse(split[1]);
                    }
                }
                else if (arg.Contains('-'))
                {
                    year = DateTime.Now.Year;
                    if (split[1] == "latest")
                    {
                        day = Int32.Parse(Directory.GetDirectories($"{RootFolder}Year{year}").Last());
                    }
                    else if (split[1] == "today")
                    {
                        day = DateTime.Now.Day;
                    }
                    else
                    {
                        day = Int32.Parse(split[0]);
                    }

                    part = Int32.Parse(split[1]);
                }
                else
                {
                    if (split[0] == "latest")
                    {
                        year = DateTime.Now.Year;
                        day = Int32.Parse(split[0]);
                    }

                    if (split[0].Length == 4)
                    {
                        year = Int32.Parse(split[0]);
                    }
                    else if (split[0].Length is 1 or 2)
                    {
                        year = DateTime.Now.Year;
                        day = Int32.Parse(split[0]);
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
                            .Select(x => (IDay) Activator.CreateInstance(x)!).ToArray()
                            .SelectMany(x => new (Func<String, Object>, Int32)[] {(x.Sol1, 1), (x.Sol2, 2)})
                    );
                }
                else
                {
                    IDay[] matchingDays = problems.Where(x =>
                            x.FullName!.Split('.')[0] == "Year" + year && x.FullName.Split('.')[1] == $"Day{day:00}")
                        .Select(x => (IDay) Activator.CreateInstance(x)!).ToArray();

                    if (part == 1)
                    {
                        matchingFuncs.AddRange(matchingDays.SelectMany(x => new (Func<String, Object>, Int32)[]
                            {(x.Sol1, 1)}));
                    }
                    else if (part == 2)
                    {
                        matchingFuncs.AddRange(matchingDays.SelectMany(x => new (Func<String, Object>, Int32)[]
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

    public static Int32 PrintProblem(Func<String, Object> problem, Int32 solutionNumber)
    {
        Int32 totalElapsedTime = 0;
        Stopwatch stopwatch = new();
        String[] fullName = problem.Target!.GetType().FullName!.Split('.');
        String year = fullName[0][4..];
        String day = fullName[1][3..];
        Console.WriteLine($"\n{year}/{day}-{solutionNumber}:");
        try
        {
            String input = File.ReadAllText($@"{RootFolder}{year}\Day{day}\Input\used input.txt");
            stopwatch.Start();
            Object output = problem.Invoke(input);
            stopwatch.Stop();
            Print(ConsoleColor.White, output);
            PrintLine(ConsoleColor.DarkGray, $" : {stopwatch.ElapsedMilliseconds}ms");
            totalElapsedTime += (Int32) stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
        }
        catch (Exception e)
        {
            PrintLine(ConsoleColor.Red, $"Unhandled exception. {e.GetType()}: {e.Message}\n{e.StackTrace}");
        }

        return totalElapsedTime;
    }

    public static void PrintProblems((Func<String, Object>, Int32)[] problems, Boolean printTotal)
    {
        Int32 totalElapsedTime = 0;
        foreach ((Func<String, Object>, Int32) problem in problems)
        {
            totalElapsedTime += PrintProblem(problem.Item1, problem.Item2);
        }

        if (printTotal)
        {
            Console.Write("\nTotal time: ");
            PrintLine(ConsoleColor.DarkGray, $"{totalElapsedTime}ms");
        }
    }

    public static void Print(ConsoleColor color, Object message)
    {
        ConsoleColor previousConsoleColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = previousConsoleColor;
    }

    public static void PrintLine(ConsoleColor color, Object message)
    {
        ConsoleColor previousConsoleColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = previousConsoleColor;
    }

    public static async void SetFileContent(Int32 year, String day)
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
            if (Int32.TryParse(args[1], out Int32 currentDayInt))
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