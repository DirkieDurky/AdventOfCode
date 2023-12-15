namespace HelperClasses;

public class Direction
{
    public static Direction None => new Direction(0, 0, "NONE");

    public static Direction Up => new Direction(0, -1, "UP");
    public static Direction Down => new Direction(0, 1, "DOWN");
    public static Direction Left => new Direction(-1, 0, "LEFT");
    public static Direction Right => new Direction(1, 0, "RIGHT");

    public static List<Direction> Directions = new List<Direction> { Up, Down, Left, Right };

    public Int32 DeltaX { get; }
    public Int32 DeltaY { get; }
    public String Text { get; }

    private Direction(Int32 deltaX, Int32 deltaY, String text)
    {
        DeltaX = deltaX;
        DeltaY = deltaY;
        Text = text;
    }

    public override Boolean Equals(Object? other) =>
       other != null && GetType() == other.GetType() && Equals((Direction)other);

    public Boolean Equals(Direction other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;

    public override Int32 GetHashCode()
    {
        return DeltaX * 2 + DeltaY;
    }

    public static Direction operator -(Direction direction)
    {
        return new Direction(-direction.DeltaX, -direction.DeltaY, direction.Text switch
        {
            "UP" => "DOWN",
            "DOWN" => "UP",
            "LEFT" => "RIGHT",
            "RIGHT" => "LEFT",
            _ => direction.Text,
        });
    }

    public enum DirectionEnum
    {
        Up,
        Right,
        Down,
        Left,
    }
}
