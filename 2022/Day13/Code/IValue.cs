using System.Text;
using System.Text.RegularExpressions;

namespace Year2022;

public abstract class IValue : IComparable
{
    public int CompareTo(object? obj)
    {
        if (obj == null) throw new ArgumentException("Object is null");
        IValue value = (obj as IValue)!;
        if (value == null) throw new ArgumentException("Object is not a IValue");

        bool? isInRightOrder = Day13.IsInRightOrder((this, value));
        return isInRightOrder == null ? 0 : (bool)isInRightOrder ? -1 : 1;
    }

    public static IValue StrToIValue(string listStr)
    {
        if (listStr[0] != '[')
            return new Integer(int.Parse(listStr));

        listStr = listStr[1..^1];

        List<IValue> iValues = new();
        for (int i = 0; i < listStr.Length; i++)
        {
            char c = listStr[i];
            if (c == '[')
            {
                string foundSubList = Day13.FindSubList(listStr, i);
                iValues.Add(StrToIValue(foundSubList));
                i += foundSubList.Length - 1;
            }
            else if (char.IsDigit(c))
            {
                string newStr = listStr[i..];
                iValues.Add(new Integer(int.Parse(Regex.Match(newStr, @"\d+").ToString())));
            }
        }

        return new List(iValues);
    }

    public static string ToString(IValue iValue)
    {
        if (iValue is Integer) return ((Integer)iValue).GetValue().ToString();

        StringBuilder result = new("[");
        List<IValue> list = ((List)iValue).GetValue();

        for (int i = 0; i < list.Count; i++)
        {
            IValue value = list[i];
            result.Append(ToString(value));
            if (i < list.Count - 1)
                result.Append(',');
        }

        result.Append(']');
        return result.ToString();
    }
}
