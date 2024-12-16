using System.ComponentModel;

namespace Aoc2024_Day16;

internal enum Direction { North = 0, East , South, West }

static class DirectionExtensions
{
    public static Vector AsVector(this Direction direction)
        => direction switch
           {
               Direction.North => (0, -1),
               Direction.East  => (1, 0),
               Direction.South => (0, 1),
               Direction.West  => (-1, 0),
               _               => throw new InvalidEnumArgumentException(nameof(direction))
           };

    public static IEnumerable<(Direction Direction, int Cost)> GetRelativeDirectionCosts(this Direction initial)
    {
        yield return (initial, 0);
        yield return (initial.TurnLeft(), 1000);
        yield return (initial.TurnRight(), 1000);
        yield return (initial.TurnRight().TurnRight(), 2000);
    }

    private static Direction TurnLeft(this Direction direction)
        => (Direction)(((int)direction + 3) % 4);

    private static Direction TurnRight(this Direction direction)
        => (Direction)(((int)direction + 1) % 4);
}
