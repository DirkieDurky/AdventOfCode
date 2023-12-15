namespace Year2022;

public interface ICommand
{
    public int LeftExecutionTime { get; set; }
    void Execute();
}
