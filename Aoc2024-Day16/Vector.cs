namespace Aoc2024_Day16;

internal readonly record struct Vector(int X, int Y)
{
    public override string ToString() => $"({X}, {Y})";
    public static implicit operator Vector((int X, int Y) tuple) => new(tuple.X, tuple.Y);
    public static Vector operator +(Vector a, Vector b) => (a.X + b.X, a.Y + b.Y);
    public static Vector operator *(int n, Vector v) => (v.X * n, v.Y * n);
}
