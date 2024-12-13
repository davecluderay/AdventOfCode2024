namespace Aoc2024_Day12;

internal class Farm
{
    private readonly Dictionary<Position,char> _plots;

    private Farm(Dictionary<Position, char> plots)
        => _plots = plots;

    public HashSet<Position>[] FindRegions()
    {
        List<HashSet<Position>> regions = new();

        HashSet<Position> mapped = new();
        foreach (var position in _plots.Keys)
        {
            if (!mapped.Add(position)) continue;

            var region = MapRegion(position);
            mapped.UnionWith(region);

            regions.Add(region);
        }

        return regions.ToArray();
    }

    private HashSet<Position> MapRegion(Position from)
    {
        var crop = _plots[from];

        HashSet<Position> mapped = new([from]);
        var toExamine = new Stack<Position>([from]);
        while (toExamine.Any())
        {
            var pos = toExamine.Pop();
            foreach (var (_, adjacent) in pos.AdjacentPositions)
            {
                if (_plots.TryGetValue(adjacent, out var adjacentCrop) && adjacentCrop == crop)
                {
                    if (mapped.Add(adjacent))
                    {
                        toExamine.Push(adjacent);
                    }
                }
            }
        }

        return mapped;
    }

    public static Farm Read()
    {
        var lines = InputFile.ReadAllLines();

        Dictionary<Position, char> plots = new();

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            var crop = lines[y][x];
            plots[new Position(x, y)] = crop;
        }

        return new Farm(plots);
    }
}
