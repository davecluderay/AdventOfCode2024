namespace Aoc2024_Day12;

internal readonly record struct Position(int X, int Y)
{
    public IEnumerable<AdjacentPosition> AdjacentPositions =>
    [
        new(Edge.Left, new Position(X - 1, Y)),
        new(Edge.Right, new Position(X + 1, Y)),
        new(Edge.Top, new Position(X, Y - 1)),
        new(Edge.Bottom, new Position(X, Y + 1))
    ];
}

internal readonly record struct AdjacentPosition(Edge Edge, Position Position);

internal enum Edge { Top, Bottom, Left, Right }
