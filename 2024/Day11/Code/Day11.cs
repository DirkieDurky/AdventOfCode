using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Year2022;
using static System.Formats.Asn1.AsnWriter;

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
                //Console.WriteLine($"{i}: {stones.Count} stones");
            }

            return stones.Count;
        }
        public object Sol2(string input)
        {
            Dictionary<long, long> stones = new Dictionary<long, long>();
            foreach (long stone in input.Split(" ").Select(long.Parse))
            {
                stones.Add(stone, 1);
            }

            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> newStones = new Dictionary<long, long>();
                foreach (var stone in stones)
                {
                    long valueLength = (long)(Math.Log10(stone.Key) + 1);
                    if (stone.Key == 0)
                    {
                        newStones[1] = (newStones.ContainsKey(1) ? newStones[1] : 0) + stone.Value;
                    }
                    else if (valueLength % 2 == 0)
                    {
                        if (valueLength > 10)
                        {

                        }
                        long halvingNumber = (long)Math.Pow(10, valueLength / 2);
                        long leftPart = stone.Key / halvingNumber;
                        long rightPart = stone.Key % halvingNumber;

                        newStones[leftPart] = (newStones.ContainsKey(leftPart) ? newStones[leftPart] : 0) + stone.Value;
                        newStones[rightPart] = (newStones.ContainsKey(rightPart) ? newStones[rightPart] : 0) + stone.Value;
                    }
                    else
                    {
                        long newValue = stone.Key * 2024;
                        newStones[newValue] = (newStones.ContainsKey(newValue) ? newStones[newValue] : 0) + stone.Value;
                    }
                }

                stones = newStones;

                //Console.WriteLine($"After {i + 1} blinks: {stones.Sum(x => x.Value)} stones");
                //Console.WriteLine(String.Join(" ", stones.Keys));
            }

            return stones.Sum(x => x.Value);
        }

        void AddOrSet1(Dictionary<long, int> dictionary, long key)
        {
            if (dictionary.ContainsKey(key)) dictionary[key]++;
            else dictionary[key] = 1;
        }

        void ChangeKey(Dictionary<long, int> dictionary, long from, long to)
        {
            dictionary[to] = dictionary[from];
        }

        public object Sol2Attempt2(string input)
        {
            LinkedList<long> stones = new LinkedList<long>(input.Split(" ").Select(long.Parse));

            for (int i = 0; i < 75; i++)
            {
                for (LinkedListNode<long> stone = stones.First!; stone != null; stone = stone.Next!)
                {
                    int stoneValueLength = (int)(Math.Log10(stone.Value) + 1);
                    if (stone.Value == 0) stone.Value = 1;
                    else if (stoneValueLength % 2 == 0)
                    {
                        int halvingNumber = (int)Math.Pow(10, (stoneValueLength - 1));
                        stones.AddAfter(stone, stone.Value % halvingNumber);
                        stone.Value = stone.Value / halvingNumber;
                        stone = stone.Next!;
                        if (stone == null) break;
                    }
                    else
                    {
                        stone.Value *= 2024;
                    }
                }

                //Console.WriteLine(String.Join(" ", stones));
                Console.WriteLine($"{i}: {stones.Count} stones");
            }

            return stones.Count;
        }

        public object Sol2Attempt(string input)
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