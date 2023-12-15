namespace Year2022
{
    public class Day02 : IDay
    {
        public object Sol1(string input)
        {
            string lookup = "ABCXYZ";
            Dictionary<int, int> winningTable = new() {
                {1,3},
                {3,2},
                {2,1}
            };

            int score = 0;
            string[] matches = input.Split('\n');

            foreach (string match in matches)
            {
                int matchScore = 0;
                char[] split = match.Split(' ').Select(x => x[0]).ToArray();
                int opponentMove = lookup.IndexOf(split[0]) % 3 + 1;
                int myMove = lookup.IndexOf(split[1]) % 3 + 1;

                matchScore += myMove;

                bool? matchResult = null;

                if (winningTable[myMove] == opponentMove) matchResult = true;
                if (winningTable[opponentMove] == myMove) matchResult = false;

                switch (matchResult)
                {
                    case true:
                        matchScore += 6;
                        break;
                    case null:
                        matchScore += 3;
                        break;
                    case false:
                        matchScore += 0;
                        break;
                }

                score += matchScore;
            }

            return score;
        }

        public object Sol2(string input)
        {
            string lookup = "ABCXYZ";
            Dictionary<int, int> winningTable = new() {
                {1,3},
                {3,2},
                {2,1}
            };
            Dictionary<int, int> losingTable = new() {
                {1,2},
                {2,3},
                {3,1}
            };

            int score = 0;
            string[] matches = input.Split('\n');

            foreach (string match in matches)
            {
                int matchScore = 0;
                char[] split = match.Split(' ').Select(x => x[0]).ToArray();
                int opponentMove = lookup.IndexOf(split[0]) % 3 + 1;
                int desiredResult = lookup.IndexOf(split[1]) % 3 + 1;

                int myMove = desiredResult switch
                {
                    1 => winningTable[opponentMove],
                    2 => opponentMove,
                    3 => losingTable[opponentMove],
                    _ => throw new Exception("desiredResult got an unexpected value")
                };

                matchScore += myMove;

                bool? matchResult = null;

                if (winningTable[myMove] == opponentMove) matchResult = true;
                if (winningTable[opponentMove] == myMove) matchResult = false;

                switch (matchResult)
                {
                    case true:
                        matchScore += 6;
                        break;
                    case null:
                        matchScore += 3;
                        break;
                    case false:
                        matchScore += 0;
                        break;
                }

                score += matchScore;
            }

            return score;
        }
    }
}
