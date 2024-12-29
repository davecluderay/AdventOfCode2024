namespace Aoc2024_Day22;

internal class Solution
{
    public string Title => "Day 22: Monkey Market";

    public object PartOne()
    {
        var initialNumbers = ReadInitialNumbers();
        return initialNumbers.Sum(initial => SecretNumberGenerator.Generate(initial, count: 2000).Last());
    }

    public object PartTwo()
    {
        var initialNumbers = ReadInitialNumbers();

        Dictionary<(int D1, int D2, int D3, int D4), int> bananaTotals = new();
        foreach (var initial in initialNumbers)
        {
            Dictionary<(int D1, int D2, int D3, int D4), int> monkeyPrices = new();
            var sequence = SecretNumberGenerator.Generate(initial, count: 2000).Prepend(initial);
            foreach (var (d1, d2, d3, d4, price) in sequence.BufferPricesWithPrecedingChanges())
            {
                if (monkeyPrices.ContainsKey((d1, d2, d3, d4))) continue;
                monkeyPrices[(d1, d2, d3, d4)] = price;
            }

            foreach (var price in monkeyPrices)
            {
                var currentTotal = bananaTotals.GetValueOrDefault(price.Key, 0);
                bananaTotals[price.Key] = currentTotal + price.Value;
            }
        }

        return bananaTotals.Values.Max();
    }

    private static int[] ReadInitialNumbers()
    {
        return InputFile.ReadAllLines()
                        .Select(int.Parse)
                        .ToArray();
    }
}
