namespace Aoc2024_Day07;

internal class Solution
{
    public string Title => "Day 7: Bridge Repair";

    public object PartOne()
        => InputFile.ReadAllLines()
                    .Select(Calibration.Parse)
                    .Where(c => c.CanBeCorrect(includeCat: false))
                    .Sum(c => c.TestValue);

    public object PartTwo()
        => InputFile.ReadAllLines()
            .Select(Calibration.Parse)
            .Where(c => c.CanBeCorrect(includeCat: true))
            .Sum(c => c.TestValue);
}