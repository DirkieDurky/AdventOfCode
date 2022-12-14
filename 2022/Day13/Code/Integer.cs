namespace Year2022;

public class Integer : IValue
{
    private Int32 _value;

    public Integer(int value)
    {
        _value = value;
    }

    public Int32 GetValue()
    {
        return _value;
    }
}