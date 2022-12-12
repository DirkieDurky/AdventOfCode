using System.Drawing;

namespace Year2022;

public class Node
{
    public Int32 X;
    public Int32 Y;
    public Int32 Cost;
    public Int32 Distance;
    public Int32 CostDistance => Cost + Distance;
    public Node? Parent;

    public Node(Int32 x, Int32 y, Int32 cost = 0, Node? parent = null)
    {
        X = x;
        Y = y;
        Cost = cost;
        Distance = Math.Abs(Day12.End.X - X) + Math.Abs(Day12.End.Y - Y);
        Parent = parent;
    }
}