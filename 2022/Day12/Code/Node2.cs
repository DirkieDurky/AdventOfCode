using System.Drawing;

namespace Year2022;

public class Node2
{
    public Int32 X;
    public Int32 Y;
    public Int32 Cost;
    public Int32 Distance;
    public Int32 CostDistance => Cost + Distance;
    public Node2? Parent;

    public Node2(Int32 x, Int32 y, Int32 cost = 0, Node2? parent = null)
    {
        X = x;
        Y = y;
        Cost = cost;
        List<Int32> distances = new();
        for (Int32 i = 0; i < Day12.Map.Count; i++)
        {
            for (Int32 j = 0; j < Day12.Map[i].Length; j++)
            {
                if (Day12.Map[i][j] == 'a')
                {
                    distances.Add(Math.Abs(j-X)+Math.Abs(i-Y));
                }
            }
        }

        Distance = distances.Min();
        Parent = parent;
    }
}