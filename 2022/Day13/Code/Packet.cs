namespace Year2022;

public class Packet
{
    public static List<(IValue, IValue)> ParsePairs(String input)
    {
        String[] pairsStr = input.Split("\n\n");
        List<(IValue, IValue)> pairs = new();


        foreach (String pairStr in pairsStr)
        {
            String[] iValues = pairStr.Split('\n');
            IValue[] pair = new IValue[2];
            for (Int32 i = 0; i < iValues.Length; i++)
            {
                String iValue = iValues[i];
                pair[i] = IValue.StrToIValue(iValue);
            }

            pairs.Add((pair[0], pair[1]));
        }

        return pairs;
    }

    public static List<IValue> Parse(String input)
    {
        input = input.Replace("\n\n", "\n");
        String[] lines = input.Split('\n');

        List<IValue> packets = new();

        foreach (String line in lines)
        {
            packets.Add(IValue.StrToIValue(line));
        }

        return packets;
    }
}