﻿namespace Year2022;

public class Direction
{
    public static Direction None => new Direction(0, 0, "NONE");

    public static Direction Up => new Direction(0, -1, "UP");
    public static Direction Down => new Direction(0, 1, "DOWN");
    public static Direction Left => new Direction(-1, 0, "LEFT");
    public static Direction Right => new Direction(1, 0, "RIGHT");

    public static List<Direction> Directions = new() { Up, Down, Left, Right };

    public static Direction GetDirection(int deltaX, int deltaY)
    {
        return Directions.First(dir => dir.DeltaX == deltaX && dir.DeltaY == deltaY);
    }

    public int DeltaX { get; }
    public int DeltaY { get; }
    public string Text { get; }

    private Direction(int deltaX, int deltaY, string text)
    {
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
}
