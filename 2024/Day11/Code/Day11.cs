using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Year2024
{
    public class Day11 : IDay
    {
        public object Sol1(string input)
        {
            List<BigInteger> stones = input.Split(" ").Select(BigInteger.Parse).ToList();

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < stones.Count; j++)
                {
                    string stoneStr = stones[j].ToString();
                    if (stones[j] == 0) stones[j] = 1;
                    //int valueLength = ((int)Math.Log10(stones[j]) + 1);
                    //if (valueLength % 2 == 0)
                    //{
                    //    int halvingNumber = (int)Math.Pow(10, (valueLength - 1));
                    //    stones.Insert(j + 1, stones[j] % halvingNumber);
                    //    stones[j] = stones[j] / halvingNumber;
                    //}
                    else if (stoneStr.Length % 2 == 0)
                    {
                        stones.Insert(j + 1, int.Parse(stoneStr.Substring(stoneStr.Length / 2)));
                        stones[j] = int.Parse(stoneStr.Substring(0, stoneStr.Length / 2));
                        j++;
                    }
                    else
                    {
                        stones[j] *= 2024;
                    }
                }

                //Console.WriteLine(String.Join(" ", stones));
                Console.WriteLine($"{i}: {stones.Count} stones");
            }

            return stones.Count;
        }
        public object Sol2(string input)
        {
            List<Stone> stones = input.Split(" ").Select(long.Parse).Select(x => new Stone(x, 0)).ToList();

            for (int i = 0; i < 75; i++)
            {
                for (int j = 0; j < stones.Count; j++)
                {
                    long stoneValue = (long)(stones[j].Value * Math.Pow(2024, stones[j].HowManyTimesToMultiplyBy2024));
                    string stoneStr = stoneValue.ToString();
                    if (stones[j].Value == 0) stones[j].Value = 1;
                    //int valueLength = ((int)Math.Log10(stones[j]) + 1);
                    //if (valueLength % 2 == 0)
                    //{
                    //    int halvingNumber = (int)Math.Pow(10, (valueLength - 1));
                    //    stones.Insert(j + 1, stones[j] % halvingNumber);
                    //    stones[j] = stones[j] / halvingNumber;
                    //}
                    else if (stoneStr.Length % 2 == 0)
                    {
                        stones.Insert(j + 1, new Stone(int.Parse(stoneStr.Substring(stoneStr.Length / 2)), 0));
                        stones[j] = new Stone(int.Parse(stoneStr.Substring(0, stoneStr.Length / 2)), 0);
                        j++;
                    }
                    else
                    {
                        stones[j].HowManyTimesToMultiplyBy2024++;
                    }
                }

                Console.WriteLine($"{i}: {stones.Count} stones");
                //Console.WriteLine(String.Join(" ", stones.Select(s => s.Value * Math.Pow(2024, s.HowManyTimesToMultiplyBy2024))));
            }

            return stones.Count;
        }
    }

    class Stone
    {
        public long Value { get; set; }
        public int HowManyTimesToMultiplyBy2024 { get; set; } = 0;

        public Stone(long value, int howManyTimesToMultiplyBy2024)
        {
            Value = value;
            HowManyTimesToMultiplyBy2024 = howManyTimesToMultiplyBy2024;
        }
    }
}