namespace Aoc2024_Day12;

internal static class FencingCost
{
    public static int Calculate(HashSet<Position> region)
    {
        var area = region.Count;
        var perimeter = region.SelectMany(p => p.AdjacentPositions)
                              .Count(a => !region.Contains(a.Position));
        return area * perimeter;
    }

    public static int CalculateWithBulkDiscount(HashSet<Position> region)
    {
        static bool IsVertical(Edge edge) => edge is Edge.Left or Edge.Right;

        var boundaries = region.SelectMany(r => r.AdjacentPositions)
                               .Where(a => !region.Contains(a.Position))
                               .GroupBy(a => a.Edge, a => a.Position)
                               .ToDictionary(g => g.Key,
                                             g => g.OrderBy(p => IsVertical(g.Key) ? (p.X, p.Y) : (p.Y, p.X))
                                                   .ToList());

        // Positions are already in the correct order to search for contiguous edges.
        var contiguousEdgeCount = 0;
        foreach (var edge in boundaries.Keys)
        {
            Position? previous = null;
            foreach (var position in boundaries[edge])
            {
                if (previous is null)
                {
                    contiguousEdgeCount++;
                    previous = position;
                    continue;
                }

                var (x, y) = position;
                var (px, py) = previous.Value;
                var isContiguous = IsVertical(edge)
                                       ? px == x && py == y - 1
                                       : py == y && px == x - 1;

                if (!isContiguous) contiguousEdgeCount++;

                previous = position;
            }
        }

        return region.Count * contiguousEdgeCount;
    }
}
