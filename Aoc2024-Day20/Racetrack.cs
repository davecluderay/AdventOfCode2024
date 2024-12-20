using System.Diagnostics;

namespace Aoc2024_Day20;

internal static class Racetrack
{
    public static Position[] ReadRoute()
    {
        var (trackAt, start, end) = ReadInput();
        var route = BuildRoute(trackAt, start, end);
        return route;
    }

    private static (HashSet<Position> TrackAt, Position start, Position end) ReadInput()
    {
        HashSet<Position> trackAt = new();
        Position start = default;
        Position end = default;

        var lines = InputFile.ReadAllLines();

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            Position position = new(x, y);
            var c = lines[y][x];
            switch (c)
            {
                case 'S':
                    trackAt.Add(position);
                    start = position;
                    break;
                case 'E':
                    trackAt.Add(position);
                    end = position;
                    break;
                case '.':
                    trackAt.Add(position);
                    break;
                case '#':
                    break;
                default:
                    throw new UnreachableException($"Unexpected character '{c}' at ({x},{y})");
            }
        }

        return (trackAt, start, end);
    }

    private static Position[] BuildRoute(HashSet<Position> trackAt, Position start, Position end)
    {
        List<Position> route = new(trackAt.Count);
        var pos = start;
        while (pos != end)
        {
            trackAt.Remove(pos);
            route.Add(pos);
            pos = GetAdjacentPositions(pos).Single(trackAt.Contains);
        }
        route.Add(end);
        return route.ToArray();

        static IEnumerable<Position> GetAdjacentPositions(Position pos)
        {
            yield return pos with { X = pos.X - 1 };
            yield return pos with { X = pos.X + 1 };
            yield return pos with { Y = pos.Y - 1 };
            yield return pos with { Y = pos.Y + 1 };
        }
    }
}
