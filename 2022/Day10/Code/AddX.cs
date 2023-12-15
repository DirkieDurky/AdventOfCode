namespace Year2022;

public class AddX : Command, ICommand
{
    public int AddAmount;

    public AddX(int addAmount, int leftExecutionTime)
    {
        AddAmount = addAmount;
        LeftExecutionTime = leftExecutionTime;
    }

    public void Execute()
    {
        Day10.X += AddAmount;
    }
}
