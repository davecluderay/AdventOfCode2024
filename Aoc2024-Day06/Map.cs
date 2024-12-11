namespace Aoc2024_Day06;

internal class Map
{
    private Bounds Bounds { get; }
    private IReadOnlySet<Position> Obstacles { get; }
    private Position InitialGuardPosition { get; }
    private Direction InitialGuardOrientation { get; }

    private Map(HashSet<Position> obstacles, Position initialGuardPosition, Direction initialGuardOrientation, Bounds bounds)
        => (Bounds, Obstacles, InitialGuardPosition, InitialGuardOrientation) = (bounds, obstacles, initialGuardPosition, initialGuardOrientation);

    public int CalculateGuardCoverage()
    {
        return SimulatePatrol(out _);
    }

    public int CountPotentialObstaclePositions()
    {
        _ = SimulatePatrol(out HashSet<Position> visited);

        var count = 0;
        foreach (var position in visited)
        {
            if (position == InitialGuardPosition) continue;
            if (SimulatePatrol(out _, obstacleAt: position) < 0)
                count++;
        }

        return count;
    }

    private int SimulatePatrol(out HashSet<Position> visited, Position? obstacleAt = null)
    {
        visited = new HashSet<Position>();
        HashSet<(Position At, Direction Orientation)> turns = new();
        Position at = InitialGuardPosition;
        Direction orientation = InitialGuardOrientation;
        while (Bounds.Contains(at))
        {
            visited.Add(at);
            var next = at + orientation;
            while (Obstacles.Contains(next) || obstacleAt == next)
            {
                var turn = (at, orientation);
                if (!turns.Add(turn)) return -1; // Guard is in a loop.

                // Turn clockwise until the way is clear, then move.
                orientation = orientation.TurnClockwise();
                next = at + orientation;
            }
            at = next;
        }
        return visited.Count;
    }

    public static Map Read()
    {
        var lines = InputFile.ReadAllLines();
        Bounds bounds = new(0, lines[0].Length - 1, 0, lines.Length - 1);
        HashSet<Position> obstacles = new();
        Position? guardPosition = null;
        Direction? guardOrientation = null;
        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            Position position = new(x, y);
            switch (lines[y][x])
            {
                case '#':
                    obstacles.Add(position);
                    break;
                case '^':
                    guardPosition = position;
                    guardOrientation = Direction.Up;
                    break;
                case '>':
                    guardPosition = position;
                    guardOrientation = Direction.Right;
                    break;
                case 'v':
                    guardPosition = position;
                    guardOrientation = Direction.Down;
                    break;
                case '<':
                    guardPosition = position;
                    guardOrientation = Direction.Left;
                    break;
            }
        }
        return new Map(obstacles, guardPosition!.Value, guardOrientation!.Value, bounds);
    }
}
