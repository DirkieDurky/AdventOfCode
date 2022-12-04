namespace Year2022
{
    public class Day02 : IDay
    {
        public Object Sol1(String input)
        {
            String lookup = "ABCXYZ";
            Dictionary<Int32, Int32> winningTable = new() {
                {1,3},
                {3,2},
                {2,1}
            };

            Int32 score = 0;
            String[] matches = input.Split('\n');

            foreach (String match in matches)
            {
                Int32 matchScore = 0;
                Char[] split = match.Split(' ').Select(x => x[0]).ToArray();
                Int32 opponentMove = lookup.IndexOf(split[0]) % 3 + 1;
                Int32 myMove = lookup.IndexOf(split[1]) % 3 + 1;

                matchScore += myMove;

                Boolean? matchResult = null;

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

        public Object Sol2(String input)
        {
            String lookup = "ABCXYZ";
            Dictionary<Int32, Int32> winningTable = new() {
                {1,3},
                {3,2},
                {2,1}
            };
            Dictionary<Int32, Int32> losingTable = new() {
                {1,2},
                {2,3},
                {3,1}
            };

            Int32 score = 0;
            String[] matches = input.Split('\n');

            foreach (String match in matches)
            {
                Int32 matchScore = 0;
                Char[] split = match.Split(' ').Select(x => x[0]).ToArray();
                Int32 opponentMove = lookup.IndexOf(split[0]) % 3 + 1;
                Int32 desiredResult = lookup.IndexOf(split[1]) % 3 + 1;

                Int32 myMove = desiredResult switch
                {
                    1 => winningTable[opponentMove],
                    2 => opponentMove,
                    3 => losingTable[opponentMove],
                    _ => throw new Exception("desiredResult got an unexpected value")
                };

                matchScore += myMove;

                Boolean? matchResult = null;

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