namespace Aoc2024_Day20;

internal readonly record struct Position(int X, int Y)
{
    public int ManhattanDistance(Position other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
}
