namespace Year2023
{
    public class Day02 : IDay
    {
        public class Game
        {
            public int ID;
            public List<Set> sets = new();
        }

        public class Set
        {
            public int redCount = 0;
            public int greenCount = 0;
            public int blueCount = 0;
        }

        public static List<Game> Parse(string input)
        {
            string[] lines = input.Split("\n");
            List<Game> games = new();

            foreach (string game in lines)
            {
                if (game == "") continue;

                string[] split = game.Split(": ");

                Game newGame = new()
                {
                    ID = int.Parse(split[0].Split(" ")[1])
                };

                string[] sets = split[1].Split("; ");

                foreach (string set in sets)
                {
                    string[] cubeAmounts = set.Split(", ");
                    Set newSet = new();

                    foreach (string cubeAmount in cubeAmounts)
                    {
                        string[] cube = cubeAmount.Split(" ");

                        switch (cube[1])
                        {
                            case "red":
                                newSet.redCount = int.Parse(cube[0]);
                                break;
                            case "green":
                                newSet.greenCount = int.Parse(cube[0]);
                                break;
                            case "blue":
                                newSet.blueCount = int.Parse(cube[0]);
                                break;
                        }
                    }

                    newGame.sets.Add(newSet);
                }

                games.Add(newGame);
            }

            return games;
        }

        public object Sol1(string input)
        {
            int sum = 0;
            List<Game> games = Parse(input);

            foreach (Game game in games)
            {
                // Console.WriteLine(game.ID);
                // Console.WriteLine("{");
                // foreach (Set set in game.sets)
                // {
                //     Console.WriteLine("    {");
                //     Console.WriteLine("        Red: " + set.redCount);
                //     Console.WriteLine("        Green: " + set.greenCount);
                //     Console.WriteLine("        Blue: " + set.blueCount);
                //     Console.WriteLine("    }");
                // }

                if (!game.sets.Any(x => x.redCount > 12 || x.greenCount > 13 || x.blueCount > 14))
                {
                    sum += game.ID;
                    // Console.WriteLine("Possible");
                }
                // else
                // {
                //     Console.WriteLine("Impossible");
                // }
                // Console.WriteLine();
            }

            return sum;
        }

        public object Sol2(string input)
        {
            int sum = 0;
            List<Game> games = Parse(input);

            foreach (Game game in games)
            {
                sum += game.sets.MaxBy(x => x.redCount)!.redCount * game.sets.MaxBy(x => x.greenCount)!.greenCount * game.sets.MaxBy(x => x.blueCount)!.blueCount;
            }

            return sum;
        }
    }
}
