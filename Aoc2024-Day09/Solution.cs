namespace Aoc2024_Day09;

internal class Solution
{
    public string Title => "Day 9: Disk Fragmenter";

    public object PartOne()
    {
        var diskMap = DiskMap.Read();
        DiskCompacter.CompactWithFragmentation(diskMap);
        return diskMap.CalculateChecksum();
    }

    public object PartTwo()
    {
        var diskMap = DiskMap.Read();
        DiskCompacter.CompactWithoutFragmentation(diskMap);
        return diskMap.CalculateChecksum();
    }
}
