namespace Year2022;

public class Packet
{
    public static List<(IValue, IValue)> ParsePairs(string input)
    {
        string[] pairsStr = input.Split("\n\n");
        List<(IValue, IValue)> pairs = new();


        foreach (string pairStr in pairsStr)
        {
            string[] iValues = pairStr.Split('\n');
            IValue[] pair = new IValue[2];
            for (int i = 0; i < iValues.Length; i++)
            {
                string iValue = iValues[i];
                pair[i] = IValue.StrToIValue(iValue);
            }

            pairs.Add((pair[0], pair[1]));
        }

        return pairs;
    }

    public static List<IValue> Parse(string input)
    {
        input = input.Replace("\n\n", "\n");
        string[] lines = input.Split('\n');

        List<IValue> packets = new();

        foreach (string line in lines)
        {
            packets.Add(IValue.StrToIValue(line));
        }

        return packets;
    }
}
