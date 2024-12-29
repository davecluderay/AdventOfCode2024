namespace Aoc2024_Day22;

internal static class SecretNumberExtensions
{
    public static IEnumerable<(int D1, int D2, int D3, int D4, int Price)> BufferPricesWithPrecedingChanges(this IEnumerable<long> secretNumbers)
    {
        var buffer = new int[5];
        var i = 0;
        using var enumerator = secretNumbers.GetEnumerator();
        while (true)
        {
            if (!enumerator.MoveNext()) break;

            if (i < 5)
            {
                buffer[i] = (int)(enumerator.Current % 10);
            }
            else
            {
                buffer[0] = buffer[1];
                buffer[1] = buffer[2];
                buffer[2] = buffer[3];
                buffer[3] = buffer[4];
                buffer[4] = (int)(enumerator.Current % 10);
            }

            if (i >= 4)
            {
                yield return (buffer[1] - buffer[0],
                              buffer[2] - buffer[1],
                              buffer[3] - buffer[2],
                              buffer[4] - buffer[3],
                              buffer[4]);
            }

            i++;
        }
    }
}
