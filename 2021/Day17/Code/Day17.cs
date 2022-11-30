using System.Text.RegularExpressions;

namespace Year2021
{
    public class Day17 : IDay
    {
        public Object Sol1(String input)
        {
            Match match = Regex.Match(input, "target area: x=(-?\\d+)..(-?\\d+), y=(-?\\d+)..(-?\\d+)");
            Int32 lowestX = Int32.Parse(match.Groups[1].Value);
            Int32 highestX = Int32.Parse(match.Groups[2].Value);
            Int32 lowestY = Int32.Parse(match.Groups[3].Value);
            Int32 highestY = Int32.Parse(match.Groups[4].Value);

            Int32 xVelocity = 0;
            Int32 yVelocity = 0;

            List<Int32> resutls = new();

            Int32 highestResult = 0;
            while (true)
            {
                Int32? result = SimulateLaunch(xVelocity, yVelocity, lowestY, highestY);
                if (result != null)
                {
                    highestResult = Math.Max(highestResult, (Int32)result);
                }
                else
                {
                    break;
                }

                yVelocity++;
            }


            return highestResult;
        }

        public Object Sol2(String input)
        {


            return "";
        }

        public Int32? SimulateLaunch(Int32 xVelocity, Int32 yVelocity, Int32 lowestY, Int32 highestY)
        {
            Int32 currentX = 0;
            Int32 currentY = 0;
            //De we start higher or lower than the end point?
            Boolean yStartedHigher = highestY < 0;
            Int32 highestYPos = 0;
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

        public Boolean CanStillHitEndpoint(Int32 currentY, Boolean yStartedHigher, Int32 lowestY, Int32 highestY)
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