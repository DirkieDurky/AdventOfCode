using System.Text.RegularExpressions;

namespace Year2021
{
    public class Day17 : IDay
    {
        public object Sol1(string input)
        {
            Match match = Regex.Match(input, "target area: x=(-?\\d+)..(-?\\d+), y=(-?\\d+)..(-?\\d+)");
            int lowestX = int.Parse(match.Groups[1].Value);
            int highestX = int.Parse(match.Groups[2].Value);
            int lowestY = int.Parse(match.Groups[3].Value);
            int highestY = int.Parse(match.Groups[4].Value);

            int xVelocity = 0;
            int yVelocity = 0;

            List<int> resutls = new();

            int highestResult = 0;
            while (true)
            {
                int? result = SimulateLaunch(xVelocity, yVelocity, lowestY, highestY);
                if (result != null)
                {
                    highestResult = Math.Max(highestResult, (int)result);
                }
                else
                {
                    break;
                }

                yVelocity++;
            }


            return highestResult;
        }

        public object Sol2(string input)
        {


            return "";
        }

        public int? SimulateLaunch(int xVelocity, int yVelocity, int lowestY, int highestY)
        {
            int currentX = 0;
            int currentY = 0;
            //De we start higher or lower than the end point?
            bool yStartedHigher = highestY < 0;
            int highestYPos = 0;
            while (currentY < lowestY || currentY > highestY)
            {
                if (!CanStillHitEndpoint(currentY, yStartedHigher, lowestY, highestY)) return null;

                currentX += xVelocity;
                currentY += yVelocity;

                highestYPos = Math.Max(highestYPos, currentY);

                xVelocity = xVelocity < 0 ? xVelocity + 1 : xVelocity > 0 ? xVelocity - 1 : 0;
                yVelocity--;
            }

            return highestYPos;
        }

        public bool CanStillHitEndpoint(int currentY, bool yStartedHigher, int lowestY, int highestY)
        {
            if (yStartedHigher)
            {
                if (currentY < lowestY) return false;
            }
            else if (currentY > highestY)
            {
                return false;
            }

            return true;
        }
    }
}
