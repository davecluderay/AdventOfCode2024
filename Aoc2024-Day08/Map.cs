namespace Aoc2024_Day08;

internal record Map(IReadOnlySet<Position> AntinodePositions)
{
    public static Map Read(bool ignoreHarmonics)
    {
        var lines = InputFile.ReadAllLines();

        Bounds bounds = new(0, lines[0].Length - 1, 0, lines.Length - 1);
        Dictionary<char, List<Position>> antennaPositions = new();
        HashSet<Position> antinodePositions = new();

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            Position position = new(x, y);
            var symbol = lines[y][x];
            if (symbol == '.') continue;

            if (!antennaPositions.ContainsKey(symbol)) antennaPositions.Add(symbol, []);
            foreach (var other in antennaPositions[symbol])
            {
                foreach (var antinode in CalculateAntinodePositions(position, other, bounds, ignoreHarmonics))
                {
                    antinodePositions.Add(antinode);
                }
            }
            antennaPositions[symbol].Add(position);
        }

        return new Map(antinodePositions);
    }

    private static IEnumerable<Position> CalculateAntinodePositions(Position a, Position b, Bounds bounds, bool ignoreHarmonics)
    {
        if (ignoreHarmonics)
        {
            var diff = b - a;
            if (bounds.Contains(b + diff)) yield return b + diff;
            if (bounds.Contains(a - diff)) yield return a - diff;
        }
        else
        {
            var step = ShortestGridAlignedFactor(b - a);
            for (var p = a; bounds.Contains(p); p += step)
                yield return p;
            for (var p = a - step; bounds.Contains(p); p -= step)
                yield return p;
        }
    }

    private static Position ShortestGridAlignedFactor(Position p)
    {
        // Euclid algorithm (GCD)
        var (a, b) = p;
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return new Position(p.X / a, p.Y / a);
    }
}