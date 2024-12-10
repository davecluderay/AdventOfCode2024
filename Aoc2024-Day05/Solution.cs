namespace Aoc2024_Day05;

internal class Solution
{
    public string Title => "Day 5: Print Queue";

    public object PartOne()
    {
        var (rules, updates) = ReadInput();
        var result = updates.Where(u => u.SatisfiesAll(rules))
                            .Sum(u => u.MiddlePageNumber);
        return result;
    }

    public object PartTwo()
    {
        var (rules, updates) = ReadInput();
        var result = updates.Where(u => !u.SatisfiesAll(rules))
                            .Select(u => Update.Correct(u, rules))
                            .Sum(u => u.MiddlePageNumber);
        return result;
    }

    private static (Rule[] Rules, Update[] Updates) ReadInput()
    {
        var sections = InputFile.ReadInSections().ToArray();
        if (sections.Length != 2) throw new InvalidOperationException("Expected two sections");

        var rules = sections.First().Select(Rule.Parse).ToArray();
        var updates = sections.Last().Select(Update.Parse).ToArray();

        return (rules, updates);
    }
}
