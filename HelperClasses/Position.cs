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

        public static Position operator +(Position a, Direction b)
        {
            return new Position(a.X + b.DeltaX, a.Y + b.DeltaY);
        }

        public static Position operator -(Position a, Direction b)
        {
            return new Position(a.X - b.DeltaX, a.Y - b.DeltaY);
        }

        public static Position Diff(Position a, Position b)
        {
            return new Position(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
        }

        public bool IsInBounds<T>(Map<T> map)
        {
            if (X < 0 || X >= map.Width) return false;
            if (Y < 0 || Y >= map.Height) return false;
            return true;
        }

        public int CountNeighbors<T>(Map<T> map, List<Direction> directions)
        {
            int count = 0;

            foreach (Direction direction in directions)
            {
                if ((this + direction).IsInBounds(map)) count++;
            }

            return count;
        }

        public int CountEqualNeighbors<T>(Map<T> map, List<Direction> directions)
        {
            int count = 0;

            foreach (Direction direction in directions)
            {
                if (!HasNeighbor(map, direction)) continue;
                if (map[this + direction]!.Equals(map[this])) count++;
            }

            return count;
        }

        public bool HasNeighbor<T>(Map<T> map, Direction side)
        {
            return (this + side).IsInBounds(map);
        }

        public bool HasEqualNeighbor<T>(Map<T> map, Direction side)
        {
            if (!HasNeighbor(map, side)) return false;
            return map[this + side]!.Equals(map[this]);
        }

        public bool HasOpposingNeighbor<T>(Map<T> map, Direction side)
        {
            if (!HasNeighbor(map, side)) return false;
            return !map[this + side]!.Equals(map[this]);
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
