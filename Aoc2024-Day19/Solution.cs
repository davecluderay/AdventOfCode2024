namespace Aoc2024_Day19;

internal class Solution
{
    public string Title => "Day 19: Linen Layout";

    public object PartOne()
    {
        var (patterns, designs) = ReadInput();
        var possibleDesignCount = designs.Count(design => IsPossibleDesign(design, patterns));
        return possibleDesignCount;
    }

    public object PartTwo()
    {
        var (patterns, designs) = ReadInput();
        designs = designs.Where(design => IsPossibleDesign(design, patterns))
                         .ToArray();
        var possibleCompositionCount = CountAllCompositions(designs, patterns);
        return possibleCompositionCount;
    }

    private bool IsPossibleDesign(string design, string[] patterns)
    {
        return CanBeMade(design, patterns);

        static bool CanBeMade(string design, string[] patterns)
        {
            if (design.Length == 0) return true;

            return patterns.Where(design.StartsWith)
                           .Any(pattern => CanBeMade(design.Substring(pattern.Length),
                                                     patterns));
        }
    }

    private static long CountAllCompositions(string[] designs, string[] patterns)
    {
        var cache = new Dictionary<string, long>();
        return designs.Sum(design => CountCompositions(design, patterns, cache));

        static long CountCompositions(string design, string[] patterns, Dictionary<string, long> cache)
        {
            if (cache.TryGetValue(design, out var cached))
                return cached;

            if (design.Length == 0)
                return 1;

            long count = patterns.Where(design.StartsWith)
                                 .Select(pattern => design.Substring(pattern.Length))
                                 .Select(remaining => CountCompositions(remaining, patterns, cache))
                                 .Sum();

            cache[design] = count;
            return count;
        }
    }

    private static (string[] Patterns, string[] Designs) ReadInput()
    {
        var sections = InputFile.ReadInSections()
                                .ToArray();
        var patterns = sections.First()
                               .Single()
                               .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var designs = sections.Last();
        return (patterns, designs);
    }
}
