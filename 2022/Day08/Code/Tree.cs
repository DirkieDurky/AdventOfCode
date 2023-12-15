public class Tree : IEquatable<Tree>
{
    public int X;
    public int Y;

    public Tree(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Tree? tree)
    {
        if (tree == null) throw new Exception();

        return this.X == tree.X && this.Y == tree.Y;
    }

    public override int GetHashCode()
    {
        int hash = 23;
        hash *= 31 + X;
        hash *= 31 + Y;
        return hash;
    }
}
