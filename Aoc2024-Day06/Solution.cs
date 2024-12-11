namespace Aoc2024_Day06;

internal class Solution
{
    public string Title => "Day 6: Guard Gallivant";

    public object PartOne()
    {
        var map = Map.Read();
        return map.CalculateGuardCoverage();
    }

    public object PartTwo()
    {
        var map = Map.Read();
        return map.CountPotentialObstaclePositions();
    }
}
