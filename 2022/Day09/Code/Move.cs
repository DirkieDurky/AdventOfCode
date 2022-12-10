class Move
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    Direction Dir;

    public Move(Direction direction)
    {
        Dir = direction;
    }

    public static List<Direction> Parse(String[] lines)
    {
        List<Direction> directions = new();

        foreach (String line in lines)
        {
            String[] split = line.Split();
            Direction dir = split[0] switch
            {
                "U" => Direction.Up,
                "D" => Direction.Down,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
            for (Int32 i = 0; i < int.Parse(split[1]); i++)
            {
                directions.Add(dir);
            }
        }

        return directions;
    }
}