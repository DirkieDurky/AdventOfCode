using System.Reflection;
public class Tree
{
    public Int32 X;
    public Int32 Y;

    public Tree(Int32 x, Int32 y)
    {
        X = x;
        Y = y;
    }

    public override Boolean Equals(Object? obj)
    {
        if (obj == null) throw new Exception();

        Tree tree = (Tree)obj;

        return this.X == tree.X && this.Y == tree.Y;
    }

    public override int GetHashCode()
    {
        Int32 hash = 23;
        hash *= 31 + X;
        hash *= 31 + Y;
        return hash;
    }
}