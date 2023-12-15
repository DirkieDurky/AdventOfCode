namespace Year2022;

public struct Operation
{
    public char Operator;
    public int Value;

    public Operation(char @operator, int value)
    {
        Operator = @operator;
        Value = value;
    }

    public Operation(string str)
    {
        string[] split = str.Split();

        if (split[3] == "*" && split[4] == "old")
        {
            Operator = '^';
            Value = 2;
            return;
        }

        Operator = split[3][0];
        Value = int.Parse(split[4]);
    }
}
