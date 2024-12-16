using System.Text;

namespace Aoc2024_Day15;

internal class Solution
{
    public string Title => "Day 15: Warehouse Woes";

    public object PartOne()
    {
        var (map, moves) = ReadInput(doubleWidth: false);
        map.SimulateMoves(moves);
        return map.CalculateBoxGpsTotal();
    }

    public object PartTwo()
    {
        var (map, moves) = ReadInput(doubleWidth: true);
        map.SimulateMoves(moves);
        return map.CalculateBoxGpsTotal();
    }

    private (WarehouseMap map, char[] moves) ReadInput(bool doubleWidth)
    {
        var sections = InputFile.ReadInSections().ToList();
        var map = WarehouseMap.Read(sections[0], doubleWidth);
        var moves = string.Join("", sections[1]).ToArray();
        return (map, moves);
    }
}
