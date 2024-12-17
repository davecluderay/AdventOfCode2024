namespace Aoc2024_Day17;

internal class Solution
{
    public string Title => "Day 17: Chronospatial Computer";

    public object PartOne()
    {
        var computer = Computer.Read();
        var output = computer.RunProgram();
        return string.Join(",", output);
    }

    public object PartTwo()
    {
        var computer = Computer.Read();
        var value = computer.FindRegisterValueForProgramToOutputItself();
        return value;
    }
}
