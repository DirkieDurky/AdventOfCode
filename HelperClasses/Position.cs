namespace Advent_of_Code.HelperClasses
{
    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public static Position Diff(Position a, Position b)
        {
            return new Position(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
        }

        public override bool Equals(object? other) =>
           other != null && GetType() == other.GetType() && Equals((Position)other);

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 23;

            hashCode = hashCode * 31 + X;
            hashCode = hashCode * 31 + Y;

            return hashCode;
        }

        public static bool operator ==(Position a, Position b) => a.Equals(b);

        public static bool operator !=(Position a, Position b)
        {
            return !a.Equals(b);
        }
    }
}
