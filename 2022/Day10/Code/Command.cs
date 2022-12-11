namespace Year2022;

public abstract class Command
{
    public Int32 LeftExecutionTime { get; set; }

    public void NextTick()
    {
        LeftExecutionTime--;
    }
}