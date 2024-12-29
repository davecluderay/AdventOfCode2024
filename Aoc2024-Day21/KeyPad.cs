namespace Aoc2024_Day21;

internal class KeyPad
{
    private readonly Dictionary<char, (int X, int Y)> _keys = new();

    private KeyPad(string[] rows)
    {
        for (var y = 0; y < rows.Length; y++)
        for (var x = 0; x < rows[y].Length; x++)
        {
            var key = rows[y][x];
            _keys[key] = (x, y);
        }
    }

    public IEnumerable<string> FindShortestSequences(char from, char to)
    {
        var (fx, fy) = _keys[from];
        var (tx, ty) = _keys[to];
        var dx = tx - fx;
        var dy = ty - fy;
        
        // Simplest case: not moving at all, just press A.
        if (dx == 0 && dy == 0) return ["A"];
        
        // There will be a maximum of two paths to consider (horizontal-then-vertical or vertical-then-horizontal).
        var vertical = new string(dy > 0 ? 'v' : '^', Math.Abs(dy));
        var horizontal = new string(dx > 0 ? '>' : '<', Math.Abs(dx));

        // Simple cases: move in a single direction, then press A.
        if (dx == 0) return [vertical + 'A'];
        if (dy == 0) return [horizontal + 'A'];

        // Special cases: avoid passing over the blank space (causing the robot to panic).
        var (sx, sy) = _keys[' '];
        if (tx == sx && fy == sy) return [vertical + horizontal + 'A'];
        if (fx == sx && ty == sy) return [horizontal + vertical + 'A'];

        // Return the two possible paths.
        return
        [
            vertical + horizontal + 'A',
            horizontal + vertical + 'A'
        ];
    }

    public static readonly KeyPad Numeric = new(["789", "456", "123", " 0A"]);
    public static readonly KeyPad Directional = new([" ^A", "<v>"]);
}