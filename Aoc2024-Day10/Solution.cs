namespace Aoc2024_Day10;

internal class Solution
{
    public string Title => "Day 10: Hoof It";

    public object PartOne()
        => Map.Read()
              .CalculateTrailScores()
              .Sum();

    public object PartTwo()
        => Map.Read()
              .CalculateTrailRatings()
              .Sum();
}
