namespace Aoc2024_Day21;

internal static class KeyPadSolver
{
    private static readonly Dictionary<(char, char, int), long> ResultCache = new ();

    public static long CalculateMinimumKeyPresses(string code, KeyPad[] keyPads)
    {
        var sequenceLength = 0L;
        var from = 'A';
        foreach (var next in code)
        {
            sequenceLength += CalculateMinimumKeyPresses(from, next, keyPads.AsSpan());
            from = next;
        }
        return sequenceLength;
    }
    

    private static long CalculateMinimumKeyPresses(char from, char to, ReadOnlySpan<KeyPad> keyPads)
    {
        if (keyPads.IsEmpty) return 1L; // Count key-presses on the outermost keypad.

        if (ResultCache.TryGetValue((from, to, keyPads.Length), out var cached))
        {
            return cached;
        }

        // Recursive call(s) to the next outermost keypad, tracking the best result.
        var best = long.MaxValue;
        foreach (var sequence in keyPads[0].FindShortestSequences(from, to))
        {
            var l = 0L;
            var f = 'A';
            foreach (var n in sequence)
            {
                l += CalculateMinimumKeyPresses(f, n, keyPads.Slice(1));
                f = n;
            }
            best = Math.Min(best, l);
        }

        return ResultCache[(from, to, keyPads.Length)] = best;
    }
}