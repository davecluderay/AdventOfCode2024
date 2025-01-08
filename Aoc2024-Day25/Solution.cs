namespace Aoc2024_Day25;

internal class Solution
{
    public string Title => "Day 25: Code Chronicle";

    public object PartOne()
    {
        var (locks, keys) = ReadSchematics();

        var count = 0;
        foreach (var key in keys)
        foreach (var @lock in locks)
        {
            if (IsPotentialFit(key, @lock))
            {
                count++;
            }
        }
        return count;
    }

    public object PartTwo()
    {
        return "MERRY CHRISTMAS!";
    }

    private static bool IsPotentialFit(int[] key, int[] @lock)
    {
        for (var i = 0; i < 5; i++)
        {
            if (key[i] + @lock[i] > 5)
            {
                return false;
            }
        }
        return true;
    }

    private static (int[][] Locks, int[][] Keys) ReadSchematics()
    {
        List<int[]> locks = new();
        List<int[]> keys = new();
        foreach (var schematic in InputFile.ReadInSections())
        {
            var data = new int[5];
            for (var l = 1; l < 6; l++)
            for (var i = 0; i < 5; i++)
            {
                data[i] += schematic[l][i] == '#' ? 1 : 0;
            }
            var addTo = schematic[0] == "#####" ? locks : keys;
            addTo.Add(data);
        }

        return (locks.ToArray(), keys.ToArray());
    }
}
