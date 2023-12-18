namespace HelperClasses;

public class Direction
{
    public static Direction None => new Direction(null, 0, 0, "NONE");

    public static Direction Up => new Direction(DirectionEnum.Up, 0, -1, "UP");
    public static Direction Down => new Direction(DirectionEnum.Down, 0, 1, "DOWN");
    public static Direction Left => new Direction(DirectionEnum.Left, -1, 0, "LEFT");
    public static Direction Right => new Direction(DirectionEnum.Right, 1, 0, "RIGHT");

    public static List<Direction> Directions = new List<Direction> { Up, Down, Left, Right };

    public DirectionEnum? DirectionE { get; }
    public int DeltaX { get; }
    public int DeltaY { get; }
    public string Text { get; }

    private Direction(DirectionEnum? directionE, int deltaX, int deltaY, string text)
    {
        DirectionE = directionE;
        DeltaX = deltaX;
        DeltaY = deltaY;
        Text = text;
    }

    public override bool Equals(object? other) =>
       other != null && GetType() == other.GetType() && Equals((Direction)other);

    public bool Equals(Direction other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;

    public override int GetHashCode()
    {
        return DeltaX * 2 + DeltaY;
    }

    public static Direction operator -(Direction direction)
    {
        DirectionEnum? directionE = direction.DirectionE switch
        {
            DirectionEnum.Up => DirectionEnum.Down,
            DirectionEnum.Down => DirectionEnum.Up,
            DirectionEnum.Left => DirectionEnum.Right,
            DirectionEnum.Right => DirectionEnum.Left,
            _ => null,
        };

        string text = direction.Text switch
        {
            "UP" => "DOWN",
            "DOWN" => "UP",
            "LEFT" => "RIGHT",
            "RIGHT" => "LEFT",
            _ => direction.Text,
        };
        return new Direction(directionE, -direction.DeltaX, -direction.DeltaY, text);
    }

    public enum DirectionEnum
    {
        Up,
        Right,
        Down,
        Left,
    }

    public static Direction FindDirectionByDirectionEnum(DirectionEnum directionEnum)
    {
        switch (directionEnum)
        {
            case DirectionEnum.Up:
                return Up;
            case DirectionEnum.Down:
                return Down;
            case DirectionEnum.Right:
                return Right;
            case DirectionEnum.Left:
                return Left;
        }

        return None;
    }
}
