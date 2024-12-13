namespace Aoc2024_Day11;

internal class Solution
{
    public string Title => "Day 11: Plutonian Pebbles";

    public object PartOne()
    {
        var stones = ReadStones();
        return CountAfter(stones, blinks: 25);
    }

    public object PartTwo()
    {
        var stones = ReadStones();
        return CountAfter(stones, blinks: 75);
    }

    private static long CountAfter(IEnumerable<long> initialStones, int blinks)
    {
        var stoneCounts = initialStones.GroupBy(s => s)
                                       .ToDictionary(g => g.Key, g => g.LongCount());
        while (blinks-- > 0)
        {
            var dic = new Dictionary<long, long>();
            foreach (var stone in stoneCounts.Keys)
            {
                var count = stoneCounts[stone];
                if (stone == 0)
                {
                    dic[1] = dic.GetValueOrDefault(1, defaultValue: 0) + count;
                    continue;
                }

                var digits = stone.ToString();
                if (digits.Length % 2 == 0)
                {
                    var split1 = long.Parse(digits.Substring(0, digits.Length / 2));
                    var split2 = long.Parse(digits.Substring(digits.Length / 2));

                    dic[split1] = dic.GetValueOrDefault(split1, defaultValue: 0) + count;
                    dic[split2] = dic.GetValueOrDefault(split2, defaultValue: 0) + count;

                    continue;
                }

                var mul = stone * 2024;
                dic[mul] = dic.GetValueOrDefault(mul, 0) + count;
            }
            stoneCounts = dic;
        }

        return stoneCounts.Sum(x => x.Value);
    }

    private static long[] ReadStones()
        => InputFile.ReadAllLines()
                    .Single()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();
}
