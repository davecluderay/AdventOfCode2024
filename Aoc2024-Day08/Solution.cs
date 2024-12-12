namespace Aoc2024_Day08;

internal class Solution
{
    public string Title => "Day 8: Resonant Collinearity";

    public object PartOne()
    {
        var map = Map.Read(ignoreHarmonics: true);
        return map.AntinodePositions.Count;
    }

    public object PartTwo()
    {
        var map = Map.Read(ignoreHarmonics: false);
        return map.AntinodePositions.Count;
    }
}
