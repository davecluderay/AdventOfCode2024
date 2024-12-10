using System.Text.RegularExpressions;

namespace Aoc2024_Day02;

internal static partial class Reports
{
    public static int[][] ReadAll()
    {
        List<int[]> reports = [];
        foreach (var line in InputFile.ReadAllLines())
        {
            var match = ReportPattern.Match(line);
            if (!match.Success) continue;
            var report = new int[match.Groups["Levels"].Captures.Count];

            for (var i = 0; i < report.Length; i++)
            {
                report[i] = int.Parse(match.Groups["Levels"].Captures[i].ValueSpan);
            }
            reports.Add(report);
        }
        return reports.ToArray();
    }

    [GeneratedRegex(@"^(?<Levels>\d+)(\s+(?<Levels>\d+))+$", RegexOptions.ExplicitCapture)]
    private static partial Regex ReportPattern { get; }
}
