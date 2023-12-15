namespace Year2022;

public class Noop : ICommand
{
    public int LeftExecutionTime { get; set; }

    public Noop(int leftExecutionTime)
    {
        LeftExecutionTime = leftExecutionTime;
    }

    public void Execute()
    {
    }
}
