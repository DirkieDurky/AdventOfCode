namespace Year2022
{
    public class Day13 : IDay
    {
        public Object Sol1(String input)
        {
            String[] pairsStr = input.Split("\n\n");
            List<(IValue,IValue)> pairs = new();
            
            
            foreach (String pairStr in pairsStr)
            {
                String[] iValues = pairStr.Split('\n');
                IValue[] pair = new IValue[2];
                for (Int32 i = 0; i < iValues.Length; i++)
                {
                    String iValue = iValues[i];
                    Console.WriteLine($"iValue: {iValue}");
                    pair[i] = StrToIValue(iValue);
                }

                pairs.Add((pair[0],pair[1]));
            }
            
            Console.WriteLine(((Integer)((List)pairs[0].Item1).GetValue()[0]).GetValue());

            return "";
        }

        public static IValue StrToIValue(String listStr)
        {
            if (listStr[0] != '[' || listStr[^1] != ']') 
                return new Integer(Int32.Parse(listStr));
            
            listStr = listStr.Replace("[", "").Replace("]", "");
            IEnumerable<String> listItems = listStr.Split(',');
            List<IValue> iValueList= new();
            if (listItems.First() == "") return new List(iValueList);
            foreach (String listItem in listItems)
            {
                iValueList.Add(StrToIValue(listItem));
            }

            return new List(iValueList);
        }

        public Object Sol2(String input)
        {      


            return "";
        }
    }
}