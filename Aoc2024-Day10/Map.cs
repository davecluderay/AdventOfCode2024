namespace Aoc2024_Day10;

internal sealed class Map
{
    private readonly Dictionary<Position, int> _heightData;
    private readonly Position[] _trailHeads;

    private Map(Dictionary<Position, int> heightData, IEnumerable<Position> trailHeads)
        => (_heightData, _trailHeads) = (heightData, trailHeads.ToArray());

    public int[] CalculateTrailScores()
        => _trailHeads.Select(h => CountRoutes(h, allRoutes: false))
                      .ToArray();

    public int[] CalculateTrailRatings()
        => _trailHeads.Select(h => CountRoutes(h, allRoutes: true))
                      .ToArray();

    private int CountRoutes(Position head, bool allRoutes)
    {
        // It is impossible to return to the same position in a given route (due to the increasing height requirement).
        // The visited set just controls whether multiple routes to the same summit are considered.
        ISet<Position>? visited = allRoutes ? null : new HashSet<Position>([head]);

        Stack<Position> stack = new([head]);
        var count = 0;
        while (stack.Count > 0)
        {
            var position = stack.Pop();
            var height = _heightData[position];
            if (height == 9) count++;
            foreach (var neighbor in GetOnwardPositions(position))
            {
                if (visited is null || visited.Add(neighbor))
                {
                    stack.Push(neighbor);
                }
            }
        }
        return count;
    }

    private IEnumerable<Position> GetOnwardPositions(Position position)
    {
        var height = _heightData[position];
        foreach (var adjacent in GetAdjacentPositions(position))
        {
            if (!_heightData.TryGetValue(adjacent, out var adjacentHeight)) continue;
            if (adjacentHeight - height != 1) continue;
            yield return adjacent;
        }
    }

    private IEnumerable<Position> GetAdjacentPositions(Position position)
    {
        var (x, y) = position;
        yield return new Position(x - 1, y);
        yield return new Position(x + 1, y);
        yield return new Position(x, y - 1);
        yield return new Position(x, y + 1);
    }

    public static Map Read()
    {
        var lines = InputFile.ReadAllLines();

        var heightData = new Dictionary<Position, int>();
        var trailHeads = new List<Position>();
        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            var position = new Position(x, y);
            var height = lines[y][x] - '0';
            heightData[position] = height;
            if (height == 0) trailHeads.Add(position);
        }

        return new Map(heightData, trailHeads);
    }

    private readonly record struct Position(int X, int Y);
}

