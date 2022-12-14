namespace Year2022;

public class List : IValue
{
    private List<IValue> _value;

    public List(List<IValue> value)
    {
        _value = value;
    }

    public List<IValue> GetValue()
    {
        return _value;
    }
}