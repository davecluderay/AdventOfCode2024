namespace Aoc2024_Day06;

internal readonly record struct Direction(int Dx, int Dy)
{
    public static readonly Direction Up = new(0, -1);
    public static readonly Direction Right = new(1, 0);
    public static readonly Direction Down = new(0, 1);
    public static readonly Direction Left = new(-1, 0);
    
    public Direction TurnClockwise()
    {
        if (this == Up) return Right;
        if (this == Right) return Down;
        if (this == Down) return Left;
        if (this == Left) return Up;
        throw new InvalidOperationException();
    }

    public static explicit operator Direction(char c)
        => c switch { '^' => Up, '>' => Right, 'v' => Down, '<' => Left, _ => throw new ArgumentOutOfRangeException() };
}
