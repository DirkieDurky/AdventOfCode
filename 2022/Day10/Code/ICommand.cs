namespace Year2022;

public interface ICommand
{
    public Int32 LeftExecutionTime { get; set; }
    void Execute();
}