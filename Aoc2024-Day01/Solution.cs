using System.Text.RegularExpressions;

namespace Aoc2024_Day01;

internal partial class Solution
{
    public string Title => "Day 1: Historian Hysteria";

    public object PartOne()
    {
        var (left, right) = ReadInputLists();
        return left.Order()
                   .Zip(right.Order())
                   .Sum(x => Math.Abs(x.First - x.Second));
    }

    public object PartTwo()
    {
        var (left, right) = ReadInputLists();
        return left.Sum(x => x * right.Count(y => y == x));
    }

    private (List<int> Left, List<int> Right) ReadInputLists()
    {
        List<int> left = [], right = [];
        foreach (var line in InputFile.ReadAllLines())
        {
            var match = InputPattern.Match(line);
            if (!match.Success) continue;
            left.Add(Convert.ToInt32(match.Groups["Left"].Value));
            right.Add(Convert.ToInt32(match.Groups["Right"].Value));
        }
        return (left, right);
    }

    [GeneratedRegex(@"^(?<Left>\d+)\s+(?<Right>\d+)$", RegexOptions.ExplicitCapture)]
    private partial Regex InputPattern { get; }
}
