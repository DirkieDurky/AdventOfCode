namespace Year2022;

public abstract class Command
{
    public int LeftExecutionTime { get; set; }

    public void NextTick()
    {
        LeftExecutionTime--;
    }
}
