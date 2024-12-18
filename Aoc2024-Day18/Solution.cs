namespace Aoc2024_Day18;

internal class Solution
{
    public string Title => "Day 18: RAM Run";

    public object PartOne()
    {
        var input = Parameters.Read();

        var corruptedLocations = SimulateCorruption(
            nanoseconds: input.DimensionLength == 7 ? 12 : 1024,
            corruptionSequence: input.CorruptionSequence);

        return FindShortestPath(input.Start,
                                input.End,
                                corruptedLocations,
                                input.DimensionLength);
    }

    public object PartTwo()
    {
        var input = Parameters.Read();

        int possibleAt = 0;
        int blockedAt = input.CorruptionSequence.Length;
        while (possibleAt < blockedAt)
        {
            var nanoseconds = (possibleAt + blockedAt) / 2;
            var corruptedLocations = SimulateCorruption(nanoseconds, input.CorruptionSequence);
            var shortestPath = FindShortestPath(input.Start,
                                                input.End,
                                                corruptedLocations,
                                                input.DimensionLength);
            if (shortestPath < 0) blockedAt = nanoseconds;
            else possibleAt = nanoseconds + 1;
        }

        var (x, y) = input.CorruptionSequence[blockedAt - 1];
        return $"{x},{y}";
    }

    private HashSet<Vector> SimulateCorruption(int nanoseconds, Vector[] corruptionSequence)
    {
        return corruptionSequence.Take(nanoseconds).ToHashSet();
    }

    private int FindShortestPath(Vector start, Vector end, HashSet<Vector> corruptedLocations, int dimensionLength)
    {
        var queue = new Queue<(Vector position, int distance)>();
        queue.Enqueue((start, 0));

        var visited = new HashSet<Vector> { start };

        while (queue.Count > 0)
        {
            var (location, distance) = queue.Dequeue();

            if (location == end)
            {
                return distance;
            }

            foreach (var adjacent in GetAdjacentLocations(location, dimensionLength))
            {
                if (visited.Contains(adjacent) || corruptedLocations.Contains(adjacent))
                {
                    continue;
                }

                visited.Add(adjacent);
                queue.Enqueue((adjacent, distance + 1));
            }
        }

        return -1;
    }

    private IEnumerable<Vector> GetAdjacentLocations(Vector location, int dimensionLength)
    {
        var (x, y) = location;
        if (x > 0) yield return (x - 1, y);
        if (x < dimensionLength - 1) yield return (x + 1, y);
        if (y > 0) yield return (x, y - 1);
        if (y < dimensionLength - 1) yield return (x, y + 1);
    }
}

internal readonly record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new(tuple.X, tuple.Y);
}
