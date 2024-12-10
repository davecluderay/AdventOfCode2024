namespace Aoc2024_Day02;

internal class Solution
{
    public string Title => "Day 2: Red-Nosed Reports";

    public object PartOne()
    {
        var reports = Reports.ReadAll();
        var analysis = new ReportAnalysis(withDampening: false);
        return reports.Count(analysis.IsSafe);
    }

    public object PartTwo()
    {
        var reports = Reports.ReadAll();
        var analysis = new ReportAnalysis(withDampening: true);
        return reports.Count(analysis.IsSafe);
    }
}
