namespace Year2022;

public class AddX : Command, ICommand
{
    public Int32 AddAmount;

    public AddX(Int32 addAmount, Int32 leftExecutionTime)
    {
        AddAmount = addAmount;
        LeftExecutionTime = leftExecutionTime;
    }

    public void Execute()
    {
        Day10.X += AddAmount;
    }
}