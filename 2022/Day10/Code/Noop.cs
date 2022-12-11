namespace Year2022;

public class Noop : ICommand
{
    public Int32 LeftExecutionTime { get; set; }

    public Noop(Int32 leftExecutionTime)
    {
        LeftExecutionTime = leftExecutionTime;
    }

    public void Execute()
    {
    }
}