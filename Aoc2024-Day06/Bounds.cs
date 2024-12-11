namespace Aoc2024_Day06;

internal readonly record struct Bounds(int Left, int Right, int Top, int Bottom)
{
    public bool Contains(Position position)
        => position.X >= Left && position.X <= Right && position.Y >= Top && position.Y <= Bottom;
}
