namespace Aoc2024_Day13;

internal class Solution
{
    public string Title => "Day 13: Claw Contraption";

    public object PartOne()
    {
        var machines = ClawMachine.ReadAll(fixConversionErrors: false)
                                  .ToList();
        return machines.Sum(machine => machine.FindMinimumCostToWinFast() ?? 0);
    }

    public object PartTwo()
    {
        var machines = ClawMachine.ReadAll(fixConversionErrors: true)
                                  .ToList();
        return machines.Sum(machine => machine.FindMinimumCostToWinFast() ?? 0);
    }
}
