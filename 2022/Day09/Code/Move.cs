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

    public static List<Direction> Parse(string[] lines)
    {
        List<Direction> directions = new();

        foreach (string line in lines)
        {
            string[] split = line.Split();
            Direction dir = split[0] switch
            {
                "U" => Direction.Up,
                "D" => Direction.Down,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
            for (int i = 0; i < int.Parse(split[1]); i++)
            {
                directions.Add(dir);
            }
        }

        return directions;
    }
}
