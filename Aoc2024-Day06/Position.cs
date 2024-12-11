namespace Aoc2024_Day06;

internal readonly record struct Position(int X, int Y)
{
    public static Position operator +(Position from, Direction direction)
        => new(from.X + direction.Dx, from.Y + direction.Dy);
}
