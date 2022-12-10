public class Tree : IEquatable<Tree>
{
    public Int32 X;
    public Int32 Y;

    public Tree(Int32 x, Int32 y)
    {
        X = x;
        Y = y;
    }

    public Boolean Equals(Tree? tree)
    {
        if (tree == null) throw new Exception();

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