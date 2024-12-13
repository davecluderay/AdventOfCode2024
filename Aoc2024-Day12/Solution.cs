namespace Aoc2024_Day12;

internal class Solution
{
    public string Title => "Day 12: Garden Groups";

    public object PartOne()
    {
        var farm = Farm.Read();
        var regions = farm.FindRegions();
        return regions.Sum(FencingCost.Calculate);
    }

    public object PartTwo()
    {
        var farm = Farm.Read();
        var regions = farm.FindRegions();
        return regions.Sum(FencingCost.CalculateWithBulkDiscount);
    }
}
