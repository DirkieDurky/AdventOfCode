using System.Drawing;

namespace Year2022;

public class Node
{
    public int X;
    public int Y;
    public int Cost;
    public int Distance;
    public int CostDistance => Cost + Distance;
    public Node? Parent;

    public Node(int x, int y, int cost = 0, Node? parent = null)
    {
        X = x;
        Y = y;
        Cost = cost;
        Distance = Math.Abs(Day12.End.X - X) + Math.Abs(Day12.End.Y - Y);
        Parent = parent;
    }
}
