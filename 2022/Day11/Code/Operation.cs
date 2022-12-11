namespace Year2022;

public struct Operation
{
    public Char Operator;
    public Int32 Value;

    public Operation(Char @operator, Int32 value)
    {
        Operator = @operator;
        Value = value;
    }

    public Operation(String str)
    {
        String[] split = str.Split();

        if (split[3] == "*" && split[4] == "old")
        {
            Operator = '^';
            Value = 2;
            return;
        }

        Operator = split[3][0];
        Value = Int32.Parse(split[4]);
    }
}