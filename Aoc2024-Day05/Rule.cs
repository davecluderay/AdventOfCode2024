namespace Aoc2024_Day05;

internal readonly record struct Rule(int First, int Second)
{
    public static Rule Parse(string line)
    {
        var parts = line.Split('|');
        return new Rule(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
