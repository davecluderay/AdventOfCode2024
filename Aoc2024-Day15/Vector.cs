namespace Aoc2024_Day15;

internal readonly record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new(tuple.X, tuple.Y);
    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
}
