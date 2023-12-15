namespace Year2022
{
    public class Day13 : IDay
    {
        public object Sol1(string input)
        {
            List<(IValue, IValue)> pairs = Packet.ParsePairs(input);

            int indexSum = 0;

            for (int i = 0; i < pairs.Count; i++)
            {
                bool? isInRightOrder = IsInRightOrder(pairs[i]);
                if (isInRightOrder == null) throw new Exception("Pairs are equal");
                indexSum += (bool)isInRightOrder ? i + 1 : 0;
                // Console.WriteLine(i);
                // Console.WriteLine((Boolean) isInRightOrder);
                // Console.WriteLine();
            }

            return indexSum;
        }

        public static bool? IsInRightOrder((IValue, IValue) pair)
        {
            //Handle case where both values are Integers
            if (pair is { Item1: Integer, Item2: Integer })
            {
                int item1Int = ((Integer)pair.Item1).GetValue();
                int item2Int = ((Integer)pair.Item2).GetValue();

                if (item1Int == item2Int) return null;
                return item1Int < item2Int;
            }

            //Convert Integers to a List with one element
            List<IValue> item1 =
                ((List)((pair.Item1 is Integer) ? new List(new List<IValue>() { pair.Item1 }) : pair.Item1))
                .GetValue();
            List<IValue> item2 =
                ((List)((pair.Item2 is Integer) ? new List(new List<IValue>() { pair.Item2 }) : pair.Item2))
                .GetValue();

            //Handle case where both values are Lists
            for (int i = 0; i < Math.Max(item1.Count, item2.Count); i++)
            {
                if (i >= item1.Count) return true;
                if (i >= item2.Count) return false;
                bool? isInRightOrder = IsInRightOrder((item1[i], item2[i]));
                if (isInRightOrder != null) return isInRightOrder;
            }

            return null;
        }

        public static string FindSubList(string str, int startIndex)
        {
            int openingCount = 0;
            for (int i = startIndex; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '[') openingCount++;
                if (c == ']') openingCount--;
                if (openingCount == 0) return str.Substring(startIndex, i - startIndex + 1);
            }

            throw new Exception("No list found in string");
        }

        public object Sol2(string input)
        {
            List<IValue> packets = Packet.Parse(input);
            List additionalPacket1 = new(new List<IValue> { new List(new List<IValue> { new Integer(2) }) });
            List additionalPacket2 = new(new List<IValue> { new List(new List<IValue> { new Integer(6) }) });
            packets.Add(additionalPacket1);
            packets.Add(additionalPacket2);

            packets.Sort();

            int additionalPacket1Index = packets.IndexOf(additionalPacket1);
            int additionalPacket2Index = packets.IndexOf(additionalPacket2);

            // foreach (IValue packet in packets)
            // {
            //     Console.WriteLine(IValue.ToString(packet));
            // }

            // Console.WriteLine($"{additionalPacket1Index} {additionalPacket2Index}");

            return (additionalPacket1Index + 1) * (additionalPacket2Index + 1);
        }
    }
}
