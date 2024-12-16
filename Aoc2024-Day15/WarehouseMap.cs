namespace Aoc2024_Day15;

internal class WarehouseMap
{
    private readonly Dictionary<Vector, char> _data;
    private Vector _robotAt;

    private WarehouseMap(Dictionary<Vector, char> data, Vector robotAt)
        => (_data, _robotAt) = (data, robotAt);

    public void SimulateMoves(IEnumerable<char> moves)
    {
        foreach (var move in moves)
        {
            var vector = move switch
                    {
                        '<' => new Vector(-1, 0),
                        '>' => new Vector(1, 0),
                        '^' => new Vector(0, -1),
                        'v' => new Vector(0, 1),
                        _   => throw new InvalidOperationException($"Unexpected move: {move}")
                    };
            if (CanMove(_robotAt, vector))
            {
                Move(_robotAt, vector);
            }
        }

        bool CanMove(Vector from, Vector by)
        {
            var to = from + by;

            if (!_data.TryGetValue(to, out char value))
            {
                return true;
            }

            var isVertical = by.X == 0;
            switch (value)
            {
                case '#':
                case 'O' when !CanMove(to, by):
                case '[' or ']' when !isVertical && !CanMove(to, by):
                case '[' when isVertical && (!CanMove(to, by) || !CanMove(to with { X = to.X + 1 }, by)):
                case ']' when isVertical && (!CanMove(to, by) || !CanMove(to with { X = to.X - 1 }, by)):
                    return false;
                default:
                    return true;
            }
        }

        void Move(Vector from, Vector by)
        {
            if (!_data.ContainsKey(from)) return;

            var isVertical = by.X == 0;
            List<Vector> set = [from]; 
            if (isVertical && _data[from] == '[')
                set.Add((from.X + 1, from.Y));
            if (isVertical && _data[from] == ']')
                set.Add((from.X - 1, from.Y));

            foreach (var f in set)
            {
                var to = f + by;
                if (_data.ContainsKey(to))
                {
                    Move(to, by);
                }
                _data[to] = _data[f];
                _data.Remove(f);
                if (_robotAt == f)
                    _robotAt = to;
            }
        }
    }

    public int CalculateBoxGpsTotal()
    {
        return _data.Where(x => x.Value is 'O' or '[')
                    .Sum(x => x.Key.X + 100 * x.Key.Y);
    }

    public static WarehouseMap Read(string[] lines, bool doubleWidth)
    {
        Dictionary<Vector, char> data = new();
        Vector robotAt = default;

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            switch (lines[y][x])
            {
                case '#':
                    if (doubleWidth)
                    {
                        data[(x * 2, y)] = '#';
                        data[(x * 2 + 1, y)] = '#';
                    }
                    else
                    {
                        data[(x, y)] = '#';
                    }
                    break;
                case 'O':
                    if (doubleWidth)
                    {
                        data[(x * 2, y)] = '[';
                        data[(x * 2 + 1, y)] = ']';
                    }
                    else
                    {
                        data[(x, y)] = 'O';
                    }
                    break;
                case '@':
                    robotAt = (doubleWidth ? x * 2 : x, y);
                    data[robotAt] = '@';
                    break;
            }
        }

        var map = new WarehouseMap(data, robotAt);
        return map;
    }
}
