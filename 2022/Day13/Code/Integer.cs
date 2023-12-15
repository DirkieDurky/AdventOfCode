namespace Year2022;

public class Integer : IValue
{
    private int _value;

    public Integer(int value)
    {
        _value = value;
    }

    public int GetValue()
    {
        return _value;
    }
}
