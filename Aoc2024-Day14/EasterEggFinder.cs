namespace Aoc2024_Day14;

internal static class EasterEggFinder
{
    public static bool IsEasterEgg(HashSet<Vector> positions)
    {
        return IsPotentialTree(positions) &&
               positions.Any(p => FindRegionOccupiedByRobots(positions, p) >= 200);
    }

    private static bool IsPotentialTree(HashSet<Vector> robotPositions)
    {
        return robotPositions.Any(p => IsSurroundedByRobots(robotPositions, p));

        static bool IsSurroundedByRobots(HashSet<Vector> positions, Vector position)
        {
            var (x, y) = position;
            return positions.Contains((x - 1, y - 1)) &&
                   positions.Contains((x, y - 1)) &&
                   positions.Contains((x + 1, y - 1)) &&
                   positions.Contains((x - 1, y)) &&
                   positions.Contains((x + 1, y)) &&
                   positions.Contains((x - 1, y + 1)) &&
                   positions.Contains((x, y + 1)) &&
                   positions.Contains((x + 1, y + 1));
        }
    }

    private static int FindRegionOccupiedByRobots(HashSet<Vector> robotPositions, Vector at)
    {
        var queue = new Queue<Vector>([at]);
        var visited = new HashSet<Vector>([at]);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var next in GetAdjacentPositions(current))
            {
                if (!robotPositions.Contains(next)) continue;
                if (!visited.Add(next)) continue;
                queue.Enqueue(next);
            }
        }

        static IEnumerable<Vector> GetAdjacentPositions(Vector position)
        {
            var (x, y) = position;
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x, y - 1);
            yield return (x, y + 1);
        }

        return visited.Count;
    }
}
