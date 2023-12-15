using System.Drawing;

namespace Year2022;

public class Node2
{
    public int X;
    public int Y;
    public int Cost;
    public int Distance;
    public int CostDistance => Cost + Distance;
    public Node2? Parent;

    public Node2(int x, int y, int cost = 0, Node2? parent = null)
    {
        X = x;
        Y = y;
        Cost = cost;
        List<int> distances = new();
        for (int i = 0; i < Day12.Map.Count; i++)
        {
            for (int j = 0; j < Day12.Map[i].Length; j++)
            {
                if (Day12.Map[i][j] == 'a')
                {
                    distances.Add(Math.Abs(j - X) + Math.Abs(i - Y));
                }
            }
        }

        Distance = distances.Min();
        Parent = parent;
    }
}
