namespace Aoc2024_Day22;

internal static class SecretNumberGenerator
{
    public static IEnumerable<long> Generate(int initial, int count)
    {
        var current = initial;
        for (var i = 0; i < count; i++)
        {
            current = ((current << 6) ^ current) & 0xFFFFFF;
            current = ((current >> 5) ^ current) & 0xFFFFFF;
            current = ((current << 11) ^ current) & 0xFFFFFF;
            yield return current;
        }
    }
}
